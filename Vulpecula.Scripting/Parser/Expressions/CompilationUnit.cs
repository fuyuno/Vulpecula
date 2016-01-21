using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    public class CompilationUnit : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            throw new NotImplementedException();
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        public Expression<Func<T, bool>> AsExpressionTree<T>()
        {
            return w => true;
        }

        #endregion
    }
}