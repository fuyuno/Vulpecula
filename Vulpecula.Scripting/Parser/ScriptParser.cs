using System;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Objects;

namespace Vulpecula.Scripting.Parser
{
    internal class ScriptParser
    {
        private readonly TokenReader _tokenReader;

        public DataSource DataSource { get; private set; }

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