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
    public class SchemaDecoder : Definition.IVisitor, Member.IVisitor, Expression.IVisitor
    {
        private BinaryReader _reader;
        internal SchemaObject _currentObject;
        internal Definition.Struct _currentStruct;

        private string _source;
        private int _line;

        public SchemaObject Decode(BinaryReader reader, Definition definitions, string source)
        {
            _reader = reader;
            _source = source;
            return Evaluate(definitions);
        }

        public SchemaObject Evaluate(Definition statement)
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

        public object Evaluate(Expression expr)
        {
            return expr.Accept(this);
        }

        public SchemaObject Visit(Definition.Collection definitions)
        {
            Definition.Struct entry = null;

            // Define all structs
            foreach (Definition def in definitions.Definitions)
            {
                if (def is Definition.Struct structStmt)
                {
                    if (structStmt.Properties != null && structStmt.Properties.ContainsKey("entry")) entry = structStmt;
                }
            }

            if(entry is null)
                Error("Entry struct not found, please define one usind '@entry'");

            return Evaluate(entry);
        }

        public SchemaObject Visit(Definition.Struct structStatement)
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

        public void Visit(SchemaObject obj, Member.If ifStmt)
        {
            object res = Evaluate(ifStmt.Condition);

            if (Convert.ToBoolean(res))
                Evaluate(obj, ifStmt.Member);
            else if(ifStmt.ElseMember != null)
                Evaluate(obj, ifStmt.ElseMember);
        }

        public void Visit(SchemaObject obj, Member.Group group)
        {
            foreach (Member member in group.Members)
                Evaluate(obj, member);
        }

        public object Visit(Expression.Number number)
        {
            return number.Value;
        }

        public object Visit(Expression.Boolean boolean)
        {
            return boolean.Value;
        }

        public object Visit(Expression.Variable variable)
        {
            string name = (string)variable.Data.Value;

            if (!_currentObject.ContainsKey(name)) Error($"Member '{name}' not found");
            DecodedValue value = _currentObject[name];

            return value.Value;
        }

        public object Visit(Expression.BinaryOperation binaryOperation)
        {
            object left = Evaluate(binaryOperation.Left);
            object right = Evaluate(binaryOperation.Right);

            switch(binaryOperation.Operator)
            {
                case TokenType.DOUBLE_EQUALS:
                    if(left.IsNumber() && right.IsNumber())
                        return Convert.ToInt64(left) == Convert.ToInt64(right);
                    return left == right;
                case TokenType.NOT_EQUALS:
                    if (left.IsNumber() && right.IsNumber())
                        return Convert.ToInt64(left) != Convert.ToInt64(right);
                    return left != right;
                case TokenType.GREATER: return (int)left > (int)right;
                case TokenType.GREATER_EQUALS: return (int)left >= (int)right;
                case TokenType.LESS: return (int)left < (int)right;
                case TokenType.LESS_EQUALS: return (int)left <= (int)right;
                case TokenType.AND: return (bool)left && (bool)right;
                case TokenType.OR: return (bool)left || (bool)right;
            }

            throw new DecoderException(_source, _line, $"Binary operation '{binaryOperation.Operator}' not implemented");
        }

        public SchemaObject Visit(Definition.Enum enumStatement)
        {
            // No implementation needed
            throw new NotImplementedException();
        }

        public object Visit(Expression.String str)
        {
            return str.Value;
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

    public static class ObjectExtensions
    {
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}
