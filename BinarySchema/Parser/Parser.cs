using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VD.BinarySchema.Parse;

namespace VD.BinarySchema
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _current;
        private Dictionary<string, IType> _types;

        public Parser()
        {
            _types = new Dictionary<string, IType>
            {
                {"int8", new IntegerType(IntegerType.Size.INT8) },
                {"int16", new IntegerType(IntegerType.Size.INT16) },
                {"int32", new IntegerType(IntegerType.Size.INT32) },
                {"int64", new IntegerType(IntegerType.Size.INT64) },
                {"uint8", new IntegerType(IntegerType.Size.UINT8) },
                {"uint16", new IntegerType(IntegerType.Size.UINT16) },
                {"uint32", new IntegerType(IntegerType.Size.UINT32) },
                {"uint64", new IntegerType(IntegerType.Size.UINT64) },

                {"bool", new BoolType() },
            };
        }

        /// <summary>
        /// Generates an abstract syntax tree from a list of tokens
        /// </summary>
        /// <param name="tokens">The list of tokens to use</param>
        /// <returns>An abstract syntax tree</returns>
        public Statement Parse(List<Token> tokens)
        {
            _tokens = tokens;
            _current = 0;

            List<Statement> definitions = new List<Statement>();

            while (Available())
            {
                int line = Peek().Location.Line;
                Statement topDef = ParseTopDef();
                topDef.Line = line;
                definitions.Add(topDef);
            }

            return new Statement.Definitions(definitions);
        }

        private Statement ParseTopDef()
        {
            var props = new Dictionary<string, object>();

            // Parse properties
            while (Match(TokenType.AT))
            {
                Token propName = Consume(TokenType.IDENTIFIER, "Expected property name after '@'");
                props[(string)propName.Value] = true; // for now
            }

            if (Match(TokenType.STRUCT))
                return ParseStruct(props);
       

            if(Match(TokenType.ENUM))
            {

            }

            throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected a declaration");
        }

        private Statement.Struct ParseStruct(Dictionary<string, object> properties = null)
        {
            Token name = Consume(TokenType.IDENTIFIER, "Expected struct name after 'struct'");
            Consume(TokenType.OPEN_BRACE, "Expected struct body");

            List<Member> members = new List<Member>();

            while(Available() && !Check(TokenType.CLOSE_BRACE))
            {
                int line = Peek().Location.Line;
                Token member_name = Consume(TokenType.IDENTIFIER, "Expected struct member name");

                Consume(TokenType.DOUBLE_COLON, "Expected struct member type");
                IType type = ParseType();
                Consume(TokenType.SEMICOLON, "Expected ';' after struct member");

                Member member = new Member.Simple(member_name, type)
                {
                    Line = line
                };

                members.Add(member);
            }

            Consume(TokenType.CLOSE_BRACE, "Expected '}' to close struct body");

            Statement.Struct stmt = new Statement.Struct(name, members);
            stmt.Properties = properties;
            _types[(string)name.Value] = new StructType(stmt);

            return stmt;
        }
        
        private IType ParseType()
        {
            Token typeName = Consume(TokenType.IDENTIFIER, "Expected type");

            string name = (string)typeName.Value;
            if(!_types.ContainsKey(name))
                throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected type");

            return _types[name];
        }

        // Helper functions
        private Token Consume(TokenType type, string message)
        {
            Token token = Advance();

            if (token.Type != type)
                throw new ParserException(token.Location.Source, token.Location.Line, message);

            return token;
        }

        private bool Match(params TokenType[] types)
        {
            foreach (TokenType t in types)
            {
                if (Peek().Type == t)
                {
                    _current++;
                    return true;
                }
            }

            return false;
        }

        private bool Check(TokenType type)
        {
            return Peek().Type == type;
        }

        private Token Advance()
        {
            return _tokens[_current++];
        }

        private Token Prev()
        {
            return _tokens[_current - 1];
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private Token Peek(int offset)
        {
            return _tokens[_current + offset];
        }

        private bool Available()
        {
            return Peek().Type != TokenType.EOF;
        }
    }

    public class ParserException : Exception
    {
        public new string Source;
        public int Line;

        public ParserException(string source, int line, string message) : base(message)
        {
            Source = source;
            Line = line;
        }
    }
}
