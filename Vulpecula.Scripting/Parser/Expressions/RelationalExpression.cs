using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // AdditiveExpression
    // RelationalExpression "<" AdditiveExpression
    // RelationalExpression "<=" AdditiveExpression
    // RelationalExpression ">" AdditiveExpression
    // RelationalExpression ">=" AdditiveExpression
    internal class RelationalExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            if (!VerifyAheadKeywords(reader, "<", ">", "<=", ">="))
            {
                // Additive
                var expr1 = new AdditiveExpression();
                expr1.Parse(reader);
                Children.Add(expr1);
            }
            else
            {
                // -> RelationalTail
                var expr2 = new RelationalExpressionTail();
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

    // "<" AdditiveExpression RelationalExpressionTail
    // "<=" AdditiveExpression RelationalExpressionTail
    // ">" AdditiveExpression RelationalExpressionTail
    // ">=" AdditiveExpression RelationalExpressionTail
    internal class RelationalExpressionTail : ExpressionBase
    {
        private int _type = 0;

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = AssertKeywordOr(reader, "<", ">", "<=", ">=");
            _type = token.TokenString == "<" ? 0 : (token.TokenString == ">" ? 1 : token.TokenString == "<=" ? 2 : 3);

            // Additive
            var expr1 = new AdditiveExpression();
            expr1.Parse(reader);
            Children.Add(expr1);

            // RelationalTail
            var expr2 = new RelationalExpressionTail();
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