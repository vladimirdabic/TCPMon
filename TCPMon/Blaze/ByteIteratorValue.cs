using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Blaze.Interpreter.Types;
using VD.Blaze.Interpreter;

namespace TCPMon.Blaze
{
    public class ByteIteratorValue : IteratorValue
    {
        public byte[] Bytes;
        private int _index;

        public ByteIteratorValue(byte[] bytes)
        {
            Bytes = bytes;
            _index = 0;
        }

        public override IValue Next()
        {
            if (!Available()) return VM.NullInstance;
            return new NumberValue(Bytes[_index++]);
        }

        public override bool Available()
        {
            return _index < Bytes.Length;
        }
    }
}
