using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TCPMon.Connection;
using VD.BinarySchema;
using VD.BinarySchema.Parse;
using VD.Blaze.Interpreter;
using VD.Blaze.Interpreter.Types;

namespace TCPMon.Blaze
{
    public class PacketValue : IValue, IValueProperties, IValueIndexable, IValueIterable
    {
        public IPacket Packet { get; private set; }
        public Dictionary<string, IValue> Properties;

        private static SchemaDecoder _decoder = new SchemaDecoder();

        public PacketValue(IPacket packet)
        {
            Packet = packet;
            Properties = new Dictionary<string, IValue>();

            DefineProperties();
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
            if(Properties.ContainsKey(name)) return Properties[name];

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


        private void DefineProperties()
        {
            Properties["decode"] = new BuiltinFunctionValue("packet.decode", (VM itp, List<IValue> args) =>
            {
                if (args.Count == 0 || !(args[0] is SchemaValue))
                    throw new InterpreterInternalException("Expected schema object for function packet.decode");

                SchemaValue schema = (SchemaValue)args[0];
                return Decode(schema.Schema, schema.Name);
            });
        }

        // Schema stuff
        public DictionaryValue Decode(Definition schema, string schemaName)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(Packet.Data));
            SchemaObject obj = _decoder.Decode(reader, schema, schemaName);

            return Translate(obj);
        }

        private DictionaryValue Translate(SchemaObject obj)
        {
            DictionaryValue dict = new DictionaryValue();

            foreach(var pair in obj)
                dict.Entries[new StringValue(pair.Key)] = TranslateValue(pair.Value);

            return dict;
        }

        private IValue TranslateValue(DecodedValue value)
        {
            if (value.Type is IntegerType) return new NumberValue(Convert.ToDouble(value.Value));
            else if (value.Value is string stringValue) return new StringValue(stringValue);
            else if (value.Value is bool boolValue) return new BooleanValue(boolValue);
            else if (value.Value is char charValue) return new StringValue(charValue.ToString());
            else if (value.Value is DecodedValue[] values)
            {
                ListValue list = new ListValue();
                foreach (DecodedValue subValue in values)
                    list.Values.Add(TranslateValue(subValue));
                return list;
            }
            else if (value.Value is SchemaObject schemaObject) return Translate(schemaObject);

            throw new InterpreterInternalException($"Cannot translate schema type '{value.Type.Name}' to a blaze value");
        }
    }
}
