using VD.BinarySchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using VD.BinarySchema.Parse;
using System.Xml.Linq;


namespace VD.BinarySchema
{
    public class SchemaDecoder : Statement.IVisitor, Member.IVisitor
    {
        private BinaryReader _reader;
        internal SchemaObject _currentObject;
        internal Statement.Struct _currentStruct;

        private string _source;
        private int _line;

        public SchemaObject Decode(BinaryReader reader, Statement definitions, string source)
        {
            _reader = reader;
            _source = source;
            return Evaluate(definitions);
        }

        public SchemaObject Evaluate(Statement statement)
        {
            if (statement.Line != 0)
            {
                _line = statement.Line;
            }

            return statement.Accept(this);
        }

        public void Evaluate(SchemaObject obj, Member member)
        {
            if (member.Line != 0)
            {
                _line = member.Line;
            }

            member.Accept(obj, this);
        }

        public SchemaObject Visit(Statement.Definitions definitions)
        {
            Statement.Struct entry = null;

            // Define all structs
            foreach (Statement def in definitions.Statements)
            {
                if (def is Statement.Struct structStmt)
                {
                    if (structStmt.Properties != null && structStmt.Properties.ContainsKey("entry")) entry = structStmt;
                }
            }

            if(entry is null)
                Error("Entry struct not found, please define one usind '@entry'");

            return Evaluate(entry);
        }

        public SchemaObject Visit(Statement.Struct structStatement)
        {
            SchemaObject obj = new SchemaObject();

            var oldObj = _currentObject;
            var oldStruct = _currentStruct;
            _currentObject = obj;
            _currentStruct = structStatement;

            foreach (Member member in structStatement.Members)
                Evaluate(obj, member);

            _currentStruct = oldStruct;
            _currentObject = oldObj;
            return obj;
        }

        public void Visit(SchemaObject obj, Member.Simple simpleMember)
        {
            string name = (string)simpleMember.Name.Value;
            object value = simpleMember.Type.Decode(_reader, this);

            if(value is null)
                Error($"Failed to decode member '{name}' in struct '{_currentStruct.Name.Value}'");

            obj[name] = new DecodedValue(simpleMember.Type, value);
        }

        public void Error(string message)
        {
            throw new DecoderException(_source, _line, message);
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
