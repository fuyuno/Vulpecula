using System.Collections.Generic;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    public abstract class ExpressionBase
    {
        protected List<ExpressionBase> Children { get; private set; }

        protected ExpressionBase()
        {
            Children = new List<ExpressionBase>();
        }

        public abstract void Parse(TokenReader reader);

        public abstract Expression AsExpressionTree();

        // public virtual AstNode AsAstTree() { }
    }
}