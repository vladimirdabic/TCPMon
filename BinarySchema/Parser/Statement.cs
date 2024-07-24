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
            SchemaObject Visit(Definitions definitions);
            SchemaObject Visit(Struct structStatement);
        }

        public abstract SchemaObject Accept(IVisitor visitor);


        public class Definitions : Statement
        {
            public List<Statement> Statements;

            public Definitions(List<Statement> statements)
            {
                Statements = statements;
            }

            public override SchemaObject Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
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

            public override SchemaObject Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }
    }
}
