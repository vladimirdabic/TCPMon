using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Blaze.Interpreter;
using VD.Blaze.Interpreter.Types;

namespace TCPMon.Blaze
{
    public class PacketBuilderValue : IValue, IValueProperties
    {
        public BinaryWriter Writer { get; private set; }
        public MemoryStream Stream { get; private set; }

        private Dictionary<string, IValue> _properties;

        public PacketBuilderValue()
        {
            Stream = new MemoryStream();
            Writer = new BinaryWriter(Stream);
            
            _properties = new Dictionary<string, IValue>
            {
                ["uint8"] = new BuiltinFunctionValue("packetbuilder.uint8", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.uint8");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToByte(value));

                    return this;
                }),

                ["uint16"] = new BuiltinFunctionValue("packetbuilder.uint16", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.uint16");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToUInt16(value));

                    return this;
                }),

                ["uint32"] = new BuiltinFunctionValue("packetbuilder.uint32", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.uint32");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToUInt32(value));

                    return this;
                }),

                ["uint64"] = new BuiltinFunctionValue("packetbuilder.uint64", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.uint64");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToUInt64(value));

                    return this;
                }),

                ["int8"] = new BuiltinFunctionValue("packetbuilder.int8", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.int8");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToSByte(value));

                    return this;
                }),

                ["int16"] = new BuiltinFunctionValue("packetbuilder.int16", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.int16");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToInt16(value));

                    return this;
                }),

                ["int32"] = new BuiltinFunctionValue("packetbuilder.int32", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.int32");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToInt32(value));

                    return this;
                }),

                ["int64"] = new BuiltinFunctionValue("packetbuilder.int64", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is NumberValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.int64");

                    double value = ((NumberValue)args[0]).Value;
                    Writer.Write(Convert.ToInt64(value));

                    return this;
                }),

                ["bool"] = new BuiltinFunctionValue("packetbuilder.bool", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is BooleanValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.bool");

                    Writer.Write(((BooleanValue)args[0]).Value);
                    return this;
                }),

                ["char"] = new BuiltinFunctionValue("packetbuilder.char", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is StringValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.char");

                    string value = ((StringValue)args[0]).Value;

                    if(value.Length < 1)
                        throw new InterpreterInternalException("Expected value for packetbuilder.char");

                    Writer.Write(value[0]);
                    
                    return this;
                }),

                ["string"] = new BuiltinFunctionValue("packetbuilder.string", (VM vm, List<IValue> args) =>
                {
                    if (args.Count == 0 || !(args[0] is StringValue))
                        throw new InterpreterInternalException("Expected value for packetbuilder.string");

                    string value = ((StringValue)args[0]).Value;
                    Writer.Write((ushort)value.Length);

                    foreach(char c in value)
                        Writer.Write(c);

                    return this;
                })
            };
        }

        public bool AsBoolean()
        {
            return true;
        }

        public string AsString()
        {
            return "<packetbuilder>";
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
            return "packetbuilder";
        }

        public IValue GetProperty(string name)
        {
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
