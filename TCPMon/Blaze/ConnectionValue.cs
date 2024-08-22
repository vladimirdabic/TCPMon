using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TCPMon.Connection;
using VD.Blaze.Interpreter;
using VD.Blaze.Interpreter.Types;

namespace TCPMon.Blaze
{
    public class ConnectionValue : IValue, IValueProperties
    {
        public IConnection Connection { get; private set; }
        public EventValue PacketsEvent { get; private set; }
        public EventValue ClosedEvent { get; private set; }

        private Dictionary<string, IValue> _properties;

        public ConnectionValue(IConnection connection, VM vm)
        {
            Connection = connection;
            PacketsEvent = new EventValue(vm);
            ClosedEvent = new EventValue(vm);

            _properties = new Dictionary<string, IValue>()
            {
                ["send"] = new BuiltinFunctionValue("connection.send", (VM _vm, List<IValue> args) => {

                    if (args.Count == 0 || !(args[0] is PacketBuilderValue))
                        throw new InterpreterInternalException("Expected packetbuilder for connection.send");

                    PacketBuilderValue builder = (PacketBuilderValue)args[0];
                    Connection.Send(builder.Stream.ToArray());

                    return null;
                })
            };
        }

        public bool AsBoolean()
        {
            return true;
        }

        public string AsString()
        {
            return $"<connection name={Connection.Name}, address={Connection.Address}>";
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
            return "connection";
        }

        public IValue GetProperty(string name)
        {
            switch(name)
            {
                case "received":
                    return PacketsEvent;

                case "closed":
                    return ClosedEvent;

                //case "send":
                //    break;

                case "name":
                    return new StringValue(Connection.Name);

                case "address":
                    return new StringValue(Connection.Address);

                // TODO
                case "packets":
                    break;
            }

            if(_properties.ContainsKey(name))
                return _properties[name];

            throw new PropertyNotFound();
        }

        public void SetProperty(string name, IValue value)
        {
            throw new PropertyNotFound();
        }
    }
}
