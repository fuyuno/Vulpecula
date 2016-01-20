using System;

using Vulpecula.Scripting.Lexer;

namespace Vulpecula.Scripting.Parser
{
    internal class ScriptParser
    {
        private readonly TokenReader _tokenReader;

        public ScriptParser(TokenReader tokenReader)
        {
            _tokenReader = tokenReader;
        }

        public void Parse() {}

        public Func<T, bool> Compile<T>()
        {
            return null;
        }
    }
}