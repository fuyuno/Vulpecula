using System;

namespace Vulpecula.Mobile.Extensions
{
    public static class StringEx
    {
        public static string ToSingleLine(this string str)
        {
            return str?.Trim().Replace(Environment.NewLine, "");
        }
    }
}