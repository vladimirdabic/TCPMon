using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TCPMon.Connection
{
    internal class TCPConnection : IConnection
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

        public TCPConnection(string connectionName)
        {
            _name = connectionName;
            _packets = new List<BytePacket>();
            _client = new TcpClient();
        }

        public void Connect(string server, int port)
        {
            try
            {
                _client.Connect(server, port);

                Thread thread = new Thread(Listener)
                {
                    IsBackground = true,
                };

                thread.Start();
            } 
            catch (SocketException ex)
            {
                switch(ex.SocketErrorCode)
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
            catch(ArgumentOutOfRangeException ex)
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

            while(_connected)
            {
                if(_stream.DataAvailable)
                {
                    byte[] data = new byte[_client.Available];
                    _stream.Read(data, 0, data.Length);

                    BytePacket packet = new BytePacket(data, DateTime.Now);
                    _packets.Add(packet);
                    PacketReceived?.Invoke(this, packet);
                }

                if (!_connected || !IsSocketConnected(_client.Client)) _connected = false;
            }


            ConnectionClosed?.Invoke(this);
            _client.Close();
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
