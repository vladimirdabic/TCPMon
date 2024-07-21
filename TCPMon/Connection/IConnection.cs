using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPMon.Connection
{
    public interface IConnection
    {
        string Name { get; }
        string Address { get; }
        ConnectionType Type { get; }
        IPacket[] Packets { get; }

        event PacketEventHandler PacketReceived;
        event ConnectionEventHandler ConnectionClosed;

        void Send(byte[] data);
        void ClearPackets();
        void Close();
    }

    public delegate void PacketEventHandler(IConnection sender, IPacket packet);
    public delegate void ConnectionEventHandler(IConnection sender);


    public enum ConnectionType
    {
        TCP
    }

    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message)
        {
        }
    }
}
