using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.BinarySchema.Parse
{
    public abstract class Definition
    {
        public int Line;
        public Dictionary<string, object> Properties;

        public interface IVisitor
        {
            SchemaObject Visit(Collection definitions);
            SchemaObject Visit(Struct structStatement);
            SchemaObject Visit(Enum enumStatement);
        }

        public abstract SchemaObject Accept(IVisitor visitor);


        public class Collection : Definition
        {
            public List<Definition> Definitions;

            public Collection(List<Definition> statements)
            {
                Definitions = statements;
            }

            public override SchemaObject Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }

        public class Struct : Definition
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

        public class Enum : Definition
        {
            public Token Name { get; private set; }
            public IType BaseType { get; private set; }
            public List<(long value, string name)> Values { get; private set; }

            public Enum(Token name, List<(long, string)> values, IType baseType)
            {
                Name = name;
                Values = values;
                BaseType = baseType;
            }

            public override SchemaObject Accept(IVisitor visitor)
            {
                return visitor.Visit(this);
            }
        }
    }
}
