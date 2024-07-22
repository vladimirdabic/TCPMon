using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.BinarySchema.Parse
{
    public abstract class Statement
    {
        public int Line;
        public Dictionary<string, object> Properties;

        public interface IVisitor
        {
            void Visit(Definitions definitions);
            void Visit(Struct structStatement);
        }

        public abstract void Accept(IVisitor visitor);


        public class Definitions : Statement
        {
            public List<Statement> Statements;

            public Definitions(List<Statement> statements)
            {
                Statements = statements;
            }

            public override void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public class Struct : Statement
        {
            public List<Member> Members;
            public Token Name { get; private set; }

            public Struct(Token name, List<Member> members)
            {
                Members = members;
                Name = name;
            }

            public override void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
    }
}
