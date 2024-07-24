using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.BinarySchema.Parse;
using VD.Blaze.Interpreter.Types;

namespace TCPMon.Blaze
{
    public class SchemaValue : IValue, IValueProperties
    {
        public Definition Schema { get; private set; }
        public string Name { get; private set; }

        public SchemaValue(Definition definition, string name)
        {
            Schema = definition;
            Name = name;
        }

        public bool AsBoolean()
        {
            return true;
        }

        public string AsString()
        {
            return $"<schema '{Name}'>";
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
            return "schema";
        }

        public IValue GetProperty(string name)
        {
            switch(name)
            {
                case "name":
                    return new StringValue(Name);

                default:
                    throw new PropertyNotFound();
            }
        }

        public void SetProperty(string name, IValue value)
        {
            throw new PropertyNotFound();
        }
    }
}
