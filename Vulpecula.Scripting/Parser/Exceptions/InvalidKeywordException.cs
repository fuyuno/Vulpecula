using System;

namespace Vulpecula.Scripting.Parser.Exceptions
{
    internal class InvalidKeywordException : Exception
    {
        public InvalidKeywordException(string actual, string expect) : base($"Assertion Error: Expected Value: {expect}, Actual Value: {actual}") {}
    }
}