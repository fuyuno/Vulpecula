using System.Collections.Generic;

namespace Vulpecula.Scripting.Lexer
{
    internal class TokenReader
    {
        private readonly List<Token> _tokens;
        private int _index;

        public TokenReader(List<Token> tokens)
        {
            _tokens = tokens;
            _index = 0;
        }

        public Token Peek()
        {
            if (_tokens.Count <= _index + 1)
                return null;
            return _tokens[_index + 1];
        }

        public Token Read()
        {
            return _tokens[++_index];
        }
    }
}