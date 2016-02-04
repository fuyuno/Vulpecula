using System;
using System.Collections.Generic;
using System.Linq;
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

        protected void AssertKeyword(TokenReader reader, string expect)
        {
            var token = reader.Read();
            if (token.TokenType != TokenType.Keyword || token.TokenString != expect)
                throw new Exception($"Assertion Error: Expected Value: {expect}, Actual Value: {token.TokenString}");
        }

        protected Token AssertKeywordOr(TokenReader reader, params string[] expect)
        {
            var token = reader.Read();
            if (token.TokenType != TokenType.Keyword || expect.All(w => w != token.TokenString))
                throw new Exception($"Assertion Error: Expected Values: {expect}, Actual Value: {token.TokenString}");
            return token;
        }
    }
}