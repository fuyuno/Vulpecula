using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // Variable is IDENTIFIER(not numeric, string, opetators and separators)
    // Convert to Camelcase str (favorited_count => FavoritedCount)
    public class VariableExpression : ExpressionBase
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

        #endregion
    }
}