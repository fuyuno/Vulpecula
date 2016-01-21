using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Objects;
using Vulpecula.Scripting.Parser.Expressions;

namespace Vulpecula.Scripting.Test.Parser.Expressions
{
    [TestClass]
    public class DataSourceExpressionTest
    {
        #region TestCases

        [TestMethod]
        public void TestCase01()
        {
            var query = "from bucket";
            Assertion(query, SourceType.Bucket, null);
        }

        [TestMethod]
        public void TestCase02()
        {
            var query = "from user:\"Mikazuki\"";
            Assertion(query, SourceType.User, "Mikazuki");
        }

        [TestMethod]
        public void TestCase03()
        {
            var query = "from conv:12345";
            Assertion(query, SourceType.Conv, "12345");
        }

        [TestMethod]
        public void TestCase04()
        {
            var query = "from tweet:12345";
            AssertionException(query, "Invalid datasource : tweet");
        }

        [TestMethod]
        public void TestCase05()
        {
            var query = "from conv:\"12345\"";
            AssertionException(query, "DataSource \"Conv\" must have NUMERIC parameter.");
        }

        [TestMethod]
        public void TestCase06()
        {
            var query = "from user:12345";
            AssertionException(query, "DataSource \"User\" must have STRING parameter.");
        }

        [TestMethod]
        public void TestCase07()
        {
            var query = "from voice:\"Mikazuki\" where source.name == \"Croudia\"";
            Assertion(query, SourceType.Voice, "Mikazuki");
        }

        [TestMethod]
        public void TestCase08()
        {
            var query = "from user:12345 where source.name == \"Croudia\"";
            AssertionException(query, "DataSource \"User\" must have STRING parameter.");
        }

        #endregion

        #region Utilities

        private void Assertion(string query, SourceType expectedSource, string expectedTag)
        {
            var tokenizer = new Tokenizer(query);
            tokenizer.Run();

            var expr = new DataSourceExpression();
            expr.Parse(new TokenReader(tokenizer.Tokens));

            Assert.AreEqual(expectedSource, expr.AsDataSource().SourceType);
            Assert.AreEqual(expectedTag, expr.AsDataSource().Tag);
        }

        private void AssertionException(string query, string expectedMessage)
        {
            try
            {
                var tokenizer = new Tokenizer(query);
                tokenizer.Run();

                var expr = new DataSourceExpression();
                expr.Parse(new TokenReader(tokenizer.Tokens));
                Assert.Fail("Does not occured Exception.");
            }
            catch (Exception e)
            {
                Assert.AreEqual(expectedMessage, e.Message);
            }
        }

        #endregion
    }
}