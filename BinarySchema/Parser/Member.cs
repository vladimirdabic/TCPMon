﻿using System;
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
            void Visit(Simple simpleMember);
        }

        public abstract void Accept(IVisitor visitor);

        public class Simple : Member
        {
            public Simple(Token name, IType type)
            {
                Name = name;
                Type = type;
            }

            public override void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
    }
}
