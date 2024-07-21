using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPMon.Blaze;
using VD.Blaze.Interpreter;
using VD.Blaze.Interpreter.Environment;
using VD.Blaze.Module;

namespace TCPMon.Connection
{
    internal class TCPScriptedConnection : IConnection
    {
        public string Name => _name;
        public string Address => _client.Client.RemoteEndPoint.ToString();
        public ConnectionType Type => ConnectionType.TCP;
        public IPacket[] Packets => _packets.ToArray();

        public event PacketEventHandler PacketReceived;
        public event ConnectionEventHandler ConnectionClosed;

        private string _name;
        private bool _connected = false;
        private List<BytePacket> _packets;
        private TcpClient _client;
        private NetworkStream _stream;

        private VM _blazeVM;
        private ConnectionValue _blazeConnection;
        private string _blazeModulePath;
        private ModuleEnv _blazeConnectionModule;

        public TCPScriptedConnection(string connectionName, string blazeModulePath)
        {
            _name = connectionName;
            _packets = new List<BytePacket>();
            _client = new TcpClient();
            _blazeModulePath = blazeModulePath;
        }

        public void Connect(string server, int port)
        {
            try
            {
                _client.Connect(server, port);

                try
                {
                    // Load module
                    Module module = new Module();

                    MemoryStream stream = new MemoryStream(File.ReadAllBytes(_blazeModulePath));
                    BinaryReader reader = new BinaryReader(stream);

                    module.FromBinary(reader);

                    // Setup vm and connection module
                    _blazeVM = new VM();
                    _blazeConnectionModule = new ModuleEnv();

                    _blazeConnection = new ConnectionValue(this, _blazeVM);
                    _blazeConnectionModule.DefineVariable("connection", VariableType.PUBLIC, _blazeConnection);
                    _blazeConnectionModule.SetParent(Program.InternalModule);

                    // Load user module
                    ModuleEnv env = _blazeVM.LoadModule(module, _blazeConnectionModule);

                    // Run main
                    string moduleName = env.Module.Name;
                    var func = env.GetFunction("main");

                    if (func is null)
                    {
                        MainForm.PrintLine($"[{moduleName}] Entry point 'main' not found", Color.Orange);
                        return;
                    }

                    _blazeVM.RunFunction(func, null);
                }
                catch (VMException e)
                {
                    if (e.Location.line == 0)
                        MainForm.PrintLine($"[{e.Location.filename}] {e.Value.AsString()}", Color.Orange);
                    else
                        MainForm.PrintLine($"[{e.Location.filename}:{e.Location.line}] {e.Value.AsString()}", Color.Orange);
                }
                catch (FileLoadException e)
                {
                    MainForm.PrintLine(e.Message);
                }

                Thread thread = new Thread(Listener)
                {
                    IsBackground = true,
                };

                thread.Start();
            }
            catch (SocketException ex)
            {
                switch (ex.SocketErrorCode)
                {
                    case SocketError.HostDown:
                    case SocketError.HostNotFound:
                    case SocketError.HostUnreachable:
                        throw new ConnectionException("Host not found or is unreachable");

                    case SocketError.ConnectionRefused:
                        throw new ConnectionException("Connection refused by host");

                    default:
                        throw new ConnectionException($"Error: {ex.Message} ({ex.SocketErrorCode})");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ConnectionException("Specified port is out of range");
            }
        }

        public void ClearPackets()
        {
            _packets.Clear();
        }

        public void Close()
        {
            if (!_connected) return;

            _connected = false;
        }

        private void Listener()
        {
            _stream = _client.GetStream();
            _connected = true;

            while (_connected)
            {
                if (_stream.DataAvailable)
                {
                    byte[] data = new byte[_client.Available];
                    _stream.Read(data, 0, data.Length);

                    BytePacket packet = new BytePacket(data, DateTime.Now);
                    _packets.Add(packet);
                    PacketReceived?.Invoke(this, packet);

                    // TODO: Pass list of bytes or a custom object
                    _blazeConnection.PacketsEvent.Raise(null);
                }

                if (!_connected || !IsSocketConnected(_client.Client)) _connected = false;
            }


            _blazeConnection.ClosedEvent.Raise(null);
            ConnectionClosed?.Invoke(this);
            _client.Close();

            Program.InternalModule.Children.Remove(_blazeConnectionModule);
        }

        static bool IsSocketConnected(Socket s)
        {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }

        public void Send(byte[] data)
        {
            if (!_connected) return;
            _stream.Write(data, 0, data.Length);
        }
    }
}
