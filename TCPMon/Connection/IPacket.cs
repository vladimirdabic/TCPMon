using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPMon.Connection
{
    public interface IPacket
    {
        DateTime ReceivedAt { get; }
        byte[] Data { get; }
    }
}
