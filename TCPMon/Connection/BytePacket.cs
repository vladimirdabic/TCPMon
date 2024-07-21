using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPMon.Connection
{
    internal class BytePacket : IPacket
    {
        public DateTime ReceivedAt => _time;
        public byte[] Data => _data;

        private readonly byte[] _data;
        private readonly DateTime _time;

        public BytePacket(byte[] data, DateTime time)
        {
            _data = data;
            _time = time;
        }
    }
}
