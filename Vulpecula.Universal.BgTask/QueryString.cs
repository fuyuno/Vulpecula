using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpecula.Universal.BgTask
{
    public static class QueryString
    {
        // 簡易 JSON エンコード, そのうち JSON.NET で書き直す。
        public static string Query(params KeyValuePair<string, object>[] props)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            foreach (var prop in props)
            {
                if (sb.Length != 1)
                {
                    sb.Append(",");
                }
                sb.Append($"\"{prop.Key}\":\"{prop.Value}\"");
            }
            sb.Append("}");
            return sb.ToString();
        }

        public static IEnumerable<KeyValuePair<string, string>> Parse(string arguments)
        {
            arguments = arguments.Substring(1, arguments.Length - 2);
            var list = arguments.Split(',').Select(s => new KeyValuePair<string, string>(s.Split(':')[0].Replace("\"", ""), s.Split(':')[1].Replace("\"", ""))).ToList();
            return list.AsReadOnly();
        }
    }
}