using System.Collections.Generic;

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
            return _enumerable;
        }
    }
}