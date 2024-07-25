using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.BinarySchema.Parse
{
    public abstract class Expression
    {
        public interface IVisitor
        {
            object Visit(Number number);
            object Visit(String str);
            object Visit(Boolean boolean);
            object Visit(Variable variable);
            object Visit(BinaryOperation binaryOperation);
        }

        public abstract object Accept(IVisitor visitor);

        public class Number : Expression
        {
            public int Value;

            public Number(int value)
            {
                Value = value;
            }

            public override object Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }

        public class String : Expression
        {
            public string Value;

            public String(string value)
            {
                Value = value;
            }

            public override object Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }

        public class Boolean : Expression
        {
            public bool Value;

            public Boolean(bool value)
            {
                Value = value;
            }

            public override object Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }

        public class Variable : Expression
        {
            public Token Data;

            public Variable(Token data)
            {
                Data = data;
            }

            public override object Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }

        public class BinaryOperation : Expression
        {
            public Expression Left;
            public Expression Right;
            public TokenType Operator;

            public BinaryOperation(Expression left, Expression right, TokenType op)
            {
                Left = left;
                Right = right;
                Operator = op;
            }

            public override object Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }
    }
}
