using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.BinarySchema.Parse
{
    public interface IType
    {
        string Name { get; }
     
        object Decode(BinaryReader reader, SchemaDecoder decoder);
    }

    public class IntegerType : IType
    {
        public string Name => IntSize.ToString();
        public Size IntSize { get; private set; }


        public IntegerType(Size size)
        {
            IntSize = size;
        }

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            switch(IntSize)
            {
                case Size.UINT8: return reader.ReadByte();
                case Size.UINT16: return reader.ReadUInt16();
                case Size.UINT32: return reader.ReadUInt32();
                case Size.UINT64: return reader.ReadUInt64();

                case Size.INT8: return reader.ReadSByte();
                case Size.INT16: return reader.ReadInt16();
                case Size.INT32: return reader.ReadInt32();
                case Size.INT64: return reader.ReadInt64();

                default:
                    throw new NotImplementedException();
            }
        }

        public enum Size
        {
            UINT8, UINT16, UINT32, UINT64,
            INT8, INT16, INT32, INT64
        }
    }

    public class BoolType : IType
    {
        public string Name => "Bool";

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            return reader.ReadBoolean();
        }
    }

    public class StructType : IType
    {
        public string Name => (string)Struct.Name.Value;
        public Statement.Struct Struct { get; private set; }

        public StructType(Statement.Struct structStmt)
        {
            Struct = structStmt;
        }

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            decoder.Evaluate(Struct);
            return null;
        }
    }
}
