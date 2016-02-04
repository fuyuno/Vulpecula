using System;
using System.Linq;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // [<DataSourceExpression>] <FilterExpression>
    public class CompilationUnit : ExpressionBase
    {
        private readonly ScriptParser _parser;

        public CompilationUnit(ScriptParser parser)
        {
            _parser = parser;
        }

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            var token = reader.Peek();
            if (token.TokenString == "from")
            {
                var expr1 = new DataSourceExpression();
                expr1.Parse(reader);
                _parser.DataSource = expr1.AsDataSource();

                var expr2 = new FilterExpression();
                expr2.Parse(reader);
                Children.Add(expr2);
            }
            else if (token.TokenString == "where")
            {
                var expr = new FilterExpression();
                expr.Parse(reader);
                Children.Add(expr);
            }
            else
                throw new Exception("Script must start 'from' or 'where' keyword.");
        }

        public override Expression AsExpressionTree()
        {
            // DataSource is not contains to Expression Tree(DS is collection source, not filtering)
            return Children.First().AsExpressionTree();
        }

        // .Where(AsExpressionTree.Compile());
        public Expression<Func<T, bool>> AsExpressionTree<T>()
        {
            return w => true;
        }

        #endregion
    }
}