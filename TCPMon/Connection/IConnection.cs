using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPMon.Connection
{
    public interface IConnection
    {
        string ConnectionName { get; }
        string ConnectionAddress { get; }
        ConnectionType ConnectionType { get; }
        IPacket[] Packets { get; }

        event PacketEventHandler PacketReceived;
        event ConnectionEventHandler ConnectionClosed;

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
