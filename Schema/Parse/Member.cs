using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.BinarySchema.Parse
{
    public abstract class Member
    {
        public int Line;
        public Token Name { get; private set; }
        public IType Type { get; private set; }

        public interface IVisitor
        {
            void Visit(SchemaObject obj, Simple simpleMember);
            void Visit(SchemaObject obj, If ifStmt);
            void Visit(SchemaObject obj, Group group);
        }

        public abstract void Accept(SchemaObject obj, IVisitor visitor);

        public class Simple : Member
        {
            public Simple(Token name, IType type)
            {
                Name = name;
                Type = type;
            }

            public override void Accept(SchemaObject obj, IVisitor visitor)
            {
                visitor.Visit(obj, this);
            }
        }

        public class If : Member
        {
            public Expression Condition { get; private set; }
            public Member Member { get; private set; }
            public Member ElseMember { get; private set; }

            public If(Expression condition, Member trueMember, Member falseMember)
            {
                Condition = condition;
                Member = trueMember;
                ElseMember = falseMember;
            }

            public override void Accept(SchemaObject obj, IVisitor visitor)
            {
                visitor.Visit(obj, this);
            }
        }

        public class Group : Member
        {
            public List<Member> Members;

            public Group(List<Member> members)
            {
                Members = members;
            }

            public override void Accept(SchemaObject obj, IVisitor visitor)
            {
                visitor.Visit(obj, this);
            }
        }
    }
}
