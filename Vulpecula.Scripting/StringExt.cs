using System.Text;

namespace Vulpecula.Scripting
{
    internal static class StringExt
    {
        // path_to_floor -> PathToFloor
        public static string ToUpperCamelcase(this string str)
        {
            var sb = new StringBuilder();
            var offset = 0;
            for (var i = 0; i + offset < str.Length; i++)
            {
                if (i == 0)
                    sb.Append((char) (str[0] - (char) 0x20));
                else if (str[i] == '_' && i + 1 < str.Length)
                {
                    if (char.IsUpper(str[i + 1]))
                        sb.Append(str[i + 1]);
                    else
                        sb.Append((char) (str[i + 1] - (char) 0x20));
                    i++;
                }
                else
                    sb.Append(str[i]);
            }
            return sb.ToString();
        }
    }
}