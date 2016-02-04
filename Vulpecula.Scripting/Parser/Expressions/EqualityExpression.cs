using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // RelationalExpression
    // EqualityExpression "==" RelationalExpression
    // EqualityExpression "!=" RelationalExpression
    internal class EqualityExpression : ExpressionBase
    {
        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            // Relational

            // EqualityTail
            if (reader.Peek().TokenString == "==" || reader.Peek().TokenString == "!=")
                return;
            var expr2 = new EqualityExpressionTail();
            expr2.Parse(reader);
            Children.Add(expr2);
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    // "==" RelationalExpression EqualityExpressionTail
    // "!=" RelationalExpression EqualityExpressionTail
    internal class EqualityExpressionTail : ExpressionBase
    {
        private int _type = 0;

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = AssertKeywordOr(reader, "==", "!=");
            _type = token.TokenString == "==" ? 0 : 1;

            // Relational

            // EqualityTail
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}