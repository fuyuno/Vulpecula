using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // UnaryExpression
    // MultiplicativeExpression "*" PrimaryNoNewArray
    // MultiplicativeExpression "/" PrimaryNoNewArray
    // MultiplicativeExpression "%" PrimaryNoNewArray
    internal class MultiplicativeExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            if (!VerifyAheadKeywords(reader, "*", "/", "%"))
            {
                // PrimaryNoNewArray
                var expr1 = new PrimaryNoNewArray();
                expr1.Parse(reader);
                Children.Add(expr1);
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
    // "*" PrimaryNoNewArray  MultiplicativeExpressionTail
    // "/" PrimaryNoNewArray  MultiplicativeExpressionTail
    // "%" PrimaryNoNewArray  MultiplicativeExpressionTail
    internal class MultiplicativeExpressionTail : ExpressionBase
    {
        private int _type = 0;

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = AssertKeywordOr(reader, "*", "/", "%");
            _type = token.TokenString == "*" ? 0 : (token.TokenString == "/" ? 1 : 2);

            // PrimaryNoNewArray
            var expr1 = new PrimaryNoNewArray();
            expr1.Parse(reader);
            Children.Add(expr1);

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