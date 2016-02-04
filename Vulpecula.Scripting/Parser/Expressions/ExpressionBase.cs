using System;
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

        public void AssertKeyword(TokenReader reader, string expect)
        {
            var token = reader.Read();
            if (token.TokenType != TokenType.Keyword || token.TokenString != expect)
                throw new Exception($"Assertion Error: Expected Value: {expect}, Actual Value: {token.TokenString}");
        }
    }
}