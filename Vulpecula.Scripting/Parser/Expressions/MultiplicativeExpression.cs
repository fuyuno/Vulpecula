using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // UnaryExpression
    // MultiplicativeExpression "*" UnaryExpression
    // MultiplicativeExpression "/" UnaryExpression
    // MultiplicativeExpression "%" UnaryExpression
    internal class MultiplicativeExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            if (!VerifyAheadKeywords(reader, "*", "/", "%"))
            {
                // UnaryExpression
            }
            else
            {
                // -> MultiplicativeTail
                var expr2 = new MultiplicativeExpressionTail();
                expr2.Parse(reader);
                Children.Add(expr2);
            }
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    // Parsing Script
    // "*" UnaryExpression MultiplicativeExpressionTail
    // "/" UnaryExpression MultiplicativeExpressionTail
    // "%" UnaryExpression MultiplicativeExpressionTail
    internal class MultiplicativeExpressionTail : ExpressionBase
    {
        private int _type = 0;

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = AssertKeywordOr(reader, "*", "/", "%");
            _type = token.TokenString == "*" ? 0 : (token.TokenString == "/" ? 1 : 2);

            // Unary

            // MultiplicativeTail
            var expr2 = new MultiplicativeExpressionTail();
            expr2.Parse(reader);
            Children.Add(expr2);
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}