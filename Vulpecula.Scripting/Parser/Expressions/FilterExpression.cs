using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // "where" EqualityExpression
    public class FilterExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            AssertKeyword(reader, "where");

            var expr = new EqualityExpression();
            expr.Parse(reader);
            Children.Add(expr);
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}