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
        private readonly Dictionary<string, IType> _types;

        private static readonly Dictionary<TokenType, PrecedenceInfo> _operatorPrecedence = new Dictionary<TokenType, PrecedenceInfo>()
        {
            { TokenType.OR, new PrecedenceInfo(0, PrecAssoc.LEFT) },
            { TokenType.AND, new PrecedenceInfo(5, PrecAssoc.LEFT) },

            { TokenType.DOUBLE_EQUALS, new PrecedenceInfo(10, PrecAssoc.LEFT) },
            { TokenType.NOT_EQUALS, new PrecedenceInfo(10, PrecAssoc.LEFT) },

            { TokenType.GREATER, new PrecedenceInfo(20, PrecAssoc.LEFT) },
            { TokenType.LESS, new PrecedenceInfo(20, PrecAssoc.LEFT) },
            { TokenType.GREATER_EQUALS, new PrecedenceInfo(20, PrecAssoc.LEFT) },
            { TokenType.LESS_EQUALS, new PrecedenceInfo(20, PrecAssoc.LEFT) },
        };

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
                {"char", new CharType() },

                {"string", new StringType() },
            };
        }

        /// <summary>
        /// Generates an abstract syntax tree from a list of tokens
        /// </summary>
        /// <param name="tokens">The list of tokens to use</param>
        /// <returns>An abstract syntax tree</returns>
        public Definition Parse(List<Token> tokens)
        {
            _tokens = tokens;
            _current = 0;

            List<Definition> definitions = new List<Definition>();

            while (Available())
            {
                int line = Peek().Location.Line;
                Definition topDef = ParseTopDef();
                topDef.Line = line;
                definitions.Add(topDef);
            }

            return new Definition.Collection(definitions);
        }

        private Definition ParseTopDef()
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
                return ParseEnum();
            }

            throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected a declaration");
        }

        private Definition.Enum ParseEnum()
        {
            Token name = Consume(TokenType.IDENTIFIER, "Expected enum name after 'enum'");
            Consume(TokenType.DOUBLE_COLON, "Expected type");
            IType baseType = ParseType();
            Consume(TokenType.OPEN_BRACE, "Expected enum body");

            List<(long, string)> definitions = new List<(long, string)>();

            while (Available() && !Check(TokenType.CLOSE_BRACE))
            {
                // TODO: default value

                Token numberValue = Consume(TokenType.NUMBER, "Expected enum number value");
                Consume(TokenType.COLON, "Expected enum value name");
                Token stringValue = Consume(TokenType.IDENTIFIER, "Expected enum value name");
                Consume(TokenType.SEMICOLON, "Expected ';' after enum value");
                definitions.Add((Convert.ToInt64(numberValue.Value), (string)stringValue.Value));
            }

            Consume(TokenType.CLOSE_BRACE, "Expected '}' to close enum definition");

            var def = new Definition.Enum(name, definitions, baseType);
            _types[(string)name.Value] = new EnumType(def);

            return def;
        }

        private Definition.Struct ParseStruct(Dictionary<string, object> properties = null)
        {
            Token name = Consume(TokenType.IDENTIFIER, "Expected struct name after 'struct'");
            Consume(TokenType.OPEN_BRACE, "Expected struct body");

            List<Member> members = ParseMembers();

            Definition.Struct stmt = new Definition.Struct(name, members);
            stmt.Properties = properties;
            _types[(string)name.Value] = new StructType(stmt);

            return stmt;
        }

        private List<Member> ParseMembers()
        {
            List<Member> members = new List<Member>();

            while (Available() && !Check(TokenType.CLOSE_BRACE))
                members.Add(ParseMember());

            Consume(TokenType.CLOSE_BRACE, "Expected '}' to close member definitions");
            return members;
        }

        private Member ParseMember()
        {
            if (Match(TokenType.IF))
            {
                Consume(TokenType.OPEN_PAREN, "Expected condition");
                Expression cond = ParseExpression();
                Consume(TokenType.CLOSE_PAREN, "Expected ')' to close condition");

                Member trueMember = ParseMember();
                Member falseMember = Match(TokenType.ELSE) ? ParseMember() : null;

                return new Member.If(cond, trueMember, falseMember);
            }
            else if (Match(TokenType.OPEN_BRACE))
            {
                return new Member.Group(ParseMembers());
            }
            else
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

                return member;
            }
        }
        
        private IType ParseType()
        {
            Token typeName = Consume(TokenType.IDENTIFIER, "Expected type");

            string name = (string)typeName.Value;
            if(!_types.ContainsKey(name))
                throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected type");

            IType type = _types[name];

            // Array type
            if(Match(TokenType.OPEN_SQUARE))
            {
                if(Match(TokenType.IDENTIFIER))
                {
                    Token member = Prev();
                    type = new ArrayType(type, (string)member.Value);
                }
                else if(Match(TokenType.NUMBER))
                {
                    Token len = Prev();
                    type = new ArrayType(type, (int)len.Value);
                }
                else
                {
                    throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected array size");
                }

                Consume(TokenType.CLOSE_SQUARE, "Expected ']' after array length");
            }

            return type;
        }

        // Expressions
        private Expression ParseExpression()
        {
            return ParseBinaryOperation(0);
        }

        private Expression ParseBinaryOperation(int precedence)
        {
            Expression left = ParsePrimary();

            while (true)
            {
                if (!Available()) break;
                Token op = Peek();
                if (!_operatorPrecedence.ContainsKey(op.Type)) break; // Is not an operator so break

                var precData = _operatorPrecedence[op.Type];
                if (precData.Level < precedence) break;  // Break if level is smaller than current level

                Advance(); // Consume the operator token finally
                int next_prec = precData.Assoc == PrecAssoc.LEFT ? precData.Level + 1 : precData.Level;

                Expression right = ParseBinaryOperation(next_prec);
                left = new Expression.BinaryOperation(left, right, op.Type);
            }

            return left;
        }

        private Expression ParsePrimary()
        {
            if (Match(TokenType.NUMBER))
                return new Expression.Number((int)Prev().Value);

            // if (Match(TokenType.STRING))
            //return new Expression.String((string)Prev().Value);

            if (Match(TokenType.IDENTIFIER))
            {
                Token name = Prev();

                if (Match(TokenType.DOT))
                {
                    string namestr = (string)name.Value;

                    if(!_types.ContainsKey(namestr) || _types[namestr] is not EnumType)
                        throw new ParserException(Peek().Location.Source, Peek().Location.Line, $"Expected an enum before '.'");

                    Token valueName = Consume(TokenType.IDENTIFIER, "Expected enum value after '.'");
                    var values = ((EnumType)_types[namestr]).Enum.Values;

                    foreach (var v in values)
                        if (v.name == (string)valueName.Value)
                            return new Expression.Number((int)v.value);

                    throw new ParserException(Peek().Location.Source, Peek().Location.Line, $"Unknown enum value '{valueName.Value}' in enum '{namestr}'");
                }

                return new Expression.Variable(name);
            }

            if (Match(TokenType.TRUE, TokenType.FALSE))
                return new Expression.Boolean(Prev().Type == TokenType.TRUE);

            if (Match(TokenType.OPEN_PAREN))
            {
                Expression expr = ParseExpression();
                Consume(TokenType.CLOSE_PAREN, "Expected ')'");
                return expr;
            }

            throw new ParserException(Peek().Location.Source, Peek().Location.Line, "Expected an expression");
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

    // Precedence Associativity
    public enum PrecAssoc
    {
        LEFT, RIGHT
    }

    public record struct PrecedenceInfo(int Level, PrecAssoc Assoc);
}
