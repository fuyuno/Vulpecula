using System;
using System.Linq.Expressions;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Objects;

namespace Vulpecula.Scripting.Parser.Expressions
{
    // Parsing Script
    // "from" "bucket"
    // "from" ("user"|"voice"|"favorite"|"conv") ":" <LiteralExpression>
    public class DataSourceExpression : ExpressionBase
    {
        private DataSource _dataSource;

        public DataSource AsDataSource()
        {
            return _dataSource;
        }

        private void ParseDataSource(SourceType type, TokenReader reader)
        {
            if (reader.Read().TokenString != ":")
                throw new Exception("Invalid format. Valid format is \"from conv:12345\".");

            var tag = reader.Read();
            if (type == SourceType.Conv)
            {
                if (tag.TokenType != TokenType.Numeric)
                    throw new Exception("DataSource \"Conv\" must have NUMERIC parameter.");
            }
            else
            {
                if (tag.TokenType != TokenType.String)
                    throw new Exception($"DataSource \"{type}\" must have STRING parameter.");
            }
            _dataSource = new DataSource(type, tag.TokenString);
        }

        #region Overrides of ExpressionBase

        public override void Parse(TokenReader reader)
        {
            reader.Read(); // from

            var token = reader.Read();
            if (token.TokenString == "bucket")
                _dataSource = new DataSource(SourceType.Bucket);
            else if (token.TokenString == "user")
                ParseDataSource(SourceType.User, reader);
            else if (token.TokenString == "voice")
                ParseDataSource(SourceType.Voice, reader);
            else if (token.TokenString == "favorite")
                ParseDataSource(SourceType.Favorite, reader);
            else if (token.TokenString == "conv")
                ParseDataSource(SourceType.Conv, reader);
            else
                throw new Exception($"Invalid datasource : {token.TokenString}");
        }

        public override Expression AsExpressionTree()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}