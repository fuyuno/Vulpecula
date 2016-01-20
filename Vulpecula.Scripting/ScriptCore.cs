using System;
using System.Collections.Generic;
using System.Linq;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Parser;

namespace Vulpecula.Scripting
{
    public class ScriptCore<T>
    {
        private readonly IEnumerable<T> _enumerable;

        public ScriptCore(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public IEnumerable<T> RunScript(string script)
        {
            var tokenizer = new Tokenizer(script);
            if (!string.IsNullOrEmpty(tokenizer.Message))
                throw new Exception(tokenizer.Message);

            var expr = new ScriptParser(new TokenReader(tokenizer.Tokens));
            expr.Parse();
            var func = expr.Compile<T>();
            return _enumerable.Where(func);
        }
    }
}