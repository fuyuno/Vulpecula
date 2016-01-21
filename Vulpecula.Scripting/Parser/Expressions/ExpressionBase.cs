using System.Collections.Generic;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    public abstract class ExpressionBase
    {
        protected List<Expression> Children { get; private set; }

        protected ExpressionBase()
        {
            Children = new List<Expression>();
        }

        public abstract void Parse(TokenReader reader);

        public abstract Expression AsExpressionTree();
    }
}