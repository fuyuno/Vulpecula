using System.Collections.Generic;

namespace Vulpecula.Scripting
{
    /// <summary>
    /// Extension of IEnumerable&lt;object&gt;
    /// </summary>
    public static class EnumerableExt
    {
        public static IEnumerable<T> RunScript<T>(this IEnumerable<T> enumerable, string script)
        {
            var runner = new ScriptCore<T>(enumerable);
            return runner.RunScript(script);
        }
    }
}