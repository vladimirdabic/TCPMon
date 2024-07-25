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
        public string Name => IntSize.ToString().ToLower();
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
        public string Name => "bool";

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            return reader.ReadBoolean();
        }
    }

    public class CharType : IType
    {
        public string Name => "char";

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            return reader.ReadChar();
        }
    }

    public class StringType : IType
    {
        public string Name => "string";

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            int len = reader.ReadUInt16();
            return new string(reader.ReadChars(len));
        }
    }

    public class StructType : IType
    {
        public string Name => (string)Struct.Name.Value;
        public Definition.Struct Struct { get; private set; }

        public StructType(Definition.Struct structStmt)
        {
            Struct = structStmt;
        }

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            return decoder.Evaluate(Struct);
        }
    }

    public class EnumType : IType
    {
        public string Name => (string)Enum.Name.Value;

        public Definition.Enum Enum { get; private set; }
        public IType BaseType { get => Enum.BaseType; }

        public EnumType(Definition.Enum enumDef)
        {
            Enum = enumDef;
        }

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            object value = BaseType.Decode(reader, decoder);

            if (!ObjectExtensions.IsNumber(value)) decoder.Error($"Enum base value can only be an integer type (enum {Name} :: {BaseType.Name})");
            long num = Convert.ToInt64(value);

            foreach(var pair in  Enum.Values)
            {
                if (pair.value == num) return pair.name;                                
            }

            return $"UNKNOWN ({num})";
        }
    }

    public class ArrayType : IType
    {
        public IType BaseType { get; private set; }
        public int Size { get; private set; }
        public string MemberName { get; private set; }


        public string Name => $"{BaseType.Name}[]";

        private ArrayType(IType baseType, int size, string memberName)
        {
            BaseType = baseType;
            Size = size;
            MemberName = memberName;
        }

        public ArrayType(IType baseType, int size) : this(baseType, size, null)
        {
        }

        public ArrayType(IType baseType, string memberName) : this(baseType, 0, memberName)
        {
        }

        public object Decode(BinaryReader reader, SchemaDecoder decoder)
        {
            int size;

            if(MemberName != null)
            {
                if (!decoder._currentObject.ContainsKey(MemberName))
                    decoder.Error($"Member '{MemberName}' that specifies the array length was not found");

                DecodedValue value = decoder._currentObject[MemberName];

                if (!(value.Type is IntegerType))
                    decoder.Error($"Member '{MemberName}' that specifies the array length must be an integer type");

                size = Convert.ToInt32(value.Value);
            }
            else
                size = Size;

            DecodedValue[] array = new DecodedValue[size];

            for(int i = 0; i < size; ++i)
                array[i] = new DecodedValue(BaseType, BaseType.Decode(reader, decoder));

            return array;
        }
    }
}
