using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.BinarySchema.Parse;

namespace VD.BinarySchema
{
    public struct DecodedValue
    {
        public IType Type { get; private set; }
        public object Value { get; private set; }

        public DecodedValue(IType type, object value)
        {
            Type = type;
            Value = value;
        }
    }
}
