namespace Vulpecula.Scripting.Lexer
{
    internal class Token
    {
        public string TokenString { get; }

        public TokenType TokenType { get; }

        public Token(string str, TokenType type)
        {
            TokenString = str;
            TokenType = type;
        }
    }
}