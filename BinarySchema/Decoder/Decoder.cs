using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VD.BinarySchema.Parse;


namespace VD.BinarySchema
{
    public class SchemaDecoder : Statement.IVisitor, Member.IVisitor
    {
        private BinaryReader _reader;
        private Dictionary<string, object> _decodedObject;
        private Dictionary<string, Statement.Struct> _structs;

        private string _source;
        private int _line;

        public static Dictionary<string, object> Decode(byte[] data, string schema, string source = "Decoder.Decode")
        {
            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            SchemaDecoder decoder = new SchemaDecoder();

            var tokens = lexer.Lex(schema, source);
            var defs = parser.Parse(tokens);

            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            return decoder.Decode(reader, defs, source);
        }

        public Dictionary<string, object> Decode(BinaryReader reader, Statement definitions, string source)
        {
            _reader = reader;
            _source = source;
            _decodedObject = new Dictionary<string, object>();
            _structs = new Dictionary<string, Statement.Struct>();
            Evaluate(definitions);
            return _decodedObject;
        }

        public void Evaluate(Statement statement)
        {
            if (statement.Line != 0)
            {
                _line = statement.Line;
                // statement.CurrentLine = _line;
            }

            statement.Accept(this);
        }

        public void Evaluate(Member member)
        {
            if (member.Line != 0)
            {
                _line = member.Line;
            }

            member.Accept(this);
        }

        public void Visit(Statement.Definitions definitions)
        {
            Statement.Struct entry = null;

            // Define all structs
            foreach (Statement def in definitions.Statements)
            {
                if (def is Statement.Struct structStmt)
                {
                    _structs[(string)structStmt.Name.Value] = structStmt;
                    if (structStmt.Properties != null && structStmt.Properties.ContainsKey("entry")) entry = structStmt;
                }
            }

            if(entry is null)
                throw new DecoderException(_source, _line, "Entry struct not found, please define one usind '@entry'");

            Evaluate(entry);
        }

        public void Visit(Statement.Struct structStatement)
        {
            foreach (Member member in structStatement.Members)
                Evaluate(member);
        }

        public void Visit(Member.Simple simpleMember)
        {
            string name = (string)simpleMember.Name.Value;

            if (simpleMember.Type is StructType)
            {
                var originalStruct = _decodedObject;
                _decodedObject = new Dictionary<string, object>();
                
                simpleMember.Type.Decode(_reader, this);

                originalStruct[name] = _decodedObject;
                _decodedObject = originalStruct;
                
                return;
            }

            object value = simpleMember.Type.Decode(_reader, this);

            if(value is null)
                throw new DecoderException(_source, _line, $"Failed to decode member '{name}'");

            _decodedObject[name] = value;
        }
    }

    public class DecoderException : Exception
    {
        public new string Source;
        public int Line;

        public DecoderException(string source, int line, string message) : base(message)
        {
            Source = source;
            Line = line;
        }
    }
}
