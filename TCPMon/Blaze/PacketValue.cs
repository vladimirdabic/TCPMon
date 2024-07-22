using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TCPMon.Connection;
using VD.Blaze.Interpreter.Types;

namespace TCPMon.Blaze
{
    public class PacketValue : IValue, IValueProperties, IValueIndexable, IValueIterable
    {
        public IPacket Packet { get; private set; }

        public PacketValue(IPacket packet)
        {
            Packet = packet;
        }

        public bool AsBoolean()
        {
            return true;
        }

        public string AsString()
        {
            return $"<packet length={Packet.Data.Length}, received_at={Packet.ReceivedAt}>";
        }

        public IValue Copy()
        {
            return this;
        }

        public bool Equals(IValue other)
        {
            return other == this;
        }

        public string GetName()
        {
            return "packet";
        }

        public IValue GetProperty(string name)
        {
            switch(name)
            {
                case "length":
                    return new NumberValue(Packet.Data.Length);

                case "received_at":
                    return new StringValue(Packet.ReceivedAt.ToString());

                default:
                    throw new PropertyNotFound();
            }
        }

        public IValue GetAtIndex(IValue index)
        {
            if(index is NumberValue numberValue)
            {
                int idx = Convert.ToInt32(numberValue.Value);

                if (idx < 0 || idx > Packet.Data.Length - 1) throw new IndexOutOfBounds();

                return new NumberValue(Packet.Data[idx]);
            }

            throw new IndexNotFound();
        }

        public void SetAtIndex(IValue index, IValue value)
        {
            throw new IndexNotFound();
        }

        public void SetProperty(string name, IValue value)
        {
            throw new PropertyNotFound();
        }

        public IteratorValue GetIterator()
        {
            return new ByteIteratorValue(Packet.Data);
        }
    }
}
