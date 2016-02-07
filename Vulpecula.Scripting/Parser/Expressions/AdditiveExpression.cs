using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // MultiplicativeExpression
    // AdditiveExpression "+" MultiplicativeExpression
    // AdditiveExpression "-" MultiplicativeExpression
    internal class AdditiveExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            if (VerifyAheadKeywords(reader, "+", "-"))
            {
                // Multiplicative
            }
            else
            {
                // -> AdditiveTail
                var expr2 = new AdditiveExpressionTail();
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

    // "+" MultiplicativeExpression AdditiveExpressionTail
    // "-" MultiplicativeExpression AdditiveExpressionTail
    internal class AdditiveExpressionTail : ExpressionBase
    {
        private int _type = 0;

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = AssertKeywordOr(reader, "+", "-");
            _type = token.TokenString == "+" ? 0 : 1;

            // Multiplicative

            // Additive
            var expr2 = new AdditiveExpressionTail();
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