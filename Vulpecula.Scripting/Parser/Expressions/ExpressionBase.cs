using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Parser.Exceptions;

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
                throw new InvalidKeywordException(token.TokenString, expect);
        }

        protected Token AssertKeywordOr(TokenReader reader, params string[] expect)
        {
            var token = reader.Read();
            if (token.TokenType != TokenType.Keyword || expect.All(w => w != token.TokenString))
                throw new InvalidKeywordException(token.TokenString, string.Join(",", expect));
            return token;
        }

        protected bool VerifyKeywords(TokenReader reader, params string[] expects)
        {
            var token = reader.Peek();
            if (token.TokenType != TokenType.Keyword || expects.All(w => w != token.TokenString))
                return false;
            return true;
        }

        protected bool VerifyAheadKeywords(TokenReader reader, params string[] expects)
        {
            var token = reader.Ahead();
            if (token.TokenType != TokenType.Keyword || expects.All(w => w != token.TokenString))
                return false;
            return true;
        }
    }
}