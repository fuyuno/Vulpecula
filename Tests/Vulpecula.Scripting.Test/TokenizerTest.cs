using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Test
{
    [TestClass]
    public class TokenizerTest
    {
        [TestMethod]
        public void RunningTest1()
        {
            var query = "from bucket where user.screen == \"Mikazuki\"";
            var queryTokens = new List<Token>
                              {
                              new Token("from", TokenType.Keyword),
                              new Token("bucket", TokenType.Variable),
                              new Token("where", TokenType.Keyword),
                              new Token("user", TokenType.Variable),
                              new Token(".", TokenType.Sepatator),
                              new Token("screen", TokenType.Variable),
                              new Token("==", TokenType.Operator),
                              new Token("Mikazuki", TokenType.String)
                              };
            Asserion(query, queryTokens);
        }

        [TestMethod]
        public void RunningTest2()
        {
            var query = "from user:\"Mikazuki\" where favorite_count > 10";
            var queryTokens = new List<Token>
                              {
                              new Token("from", TokenType.Keyword),
                              new Token("user", TokenType.Variable),
                              new Token(":", TokenType.Sepatator),
                              new Token("Mikazuki", TokenType.String),
                              new Token("where", TokenType.Keyword),
                              new Token("favorite_count", TokenType.Variable),
                              new Token(">", TokenType.Operator),
                              new Token("10", TokenType.Numeric)
                              };
            Asserion(query, queryTokens);
        }

        [TestMethod]
        public void RunningTest3()
        {
            var query = "from bucket where (user.name == \"みかづき\" && favorite_count == 10) || source.name == \"Croudia\"";
            var queryTokens = new List<Token>
                              {
                              new Token("from", TokenType.Keyword),
                              new Token("bucket", TokenType.Variable),
                              new Token("where", TokenType.Keyword),
                              new Token("(", TokenType.Sepatator),
                              new Token("user", TokenType.Variable),
                              new Token(".", TokenType.Sepatator),
                              new Token("name", TokenType.Variable),
                              new Token("==", TokenType.Operator),
                              new Token("みかづき", TokenType.String),
                              new Token("&&", TokenType.Operator),
                              new Token("favorite_count", TokenType.Variable),
                              new Token("==", TokenType.Operator),
                              new Token("10", TokenType.Numeric),
                              new Token(")", TokenType.Sepatator),
                              new Token("||", TokenType.Operator),
                              new Token("source", TokenType.Variable),
                              new Token(".", TokenType.Sepatator),
                              new Token("name", TokenType.Variable),
                              new Token("==", TokenType.Operator),
                              new Token("Croudia", TokenType.String)
                              };
            Asserion(query, queryTokens);
        }

        private void Asserion(string query, List<Token> queryTokens)
        {
            var tokenizer = new Tokenizer(query);
            tokenizer.Run();
            Assert.AreEqual("", tokenizer.Message);
            Assert.AreEqual(queryTokens.Count, tokenizer.Tokens.Count);
            for (int i = 0; i < queryTokens.Count; i++)
            {
                Assert.AreEqual(queryTokens[i].TokenString, tokenizer.Tokens[i].TokenString);
                Assert.AreEqual(queryTokens[i].TokenType, tokenizer.Tokens[i].TokenType);
            }
        }
    }
}