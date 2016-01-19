using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vulpecula.Scripting.Lexer
{
    internal class Tokenizer
    {
        private readonly string[] _keywords = { "from", "where" };
        private readonly string[] _operators = { "+", "-", "*", "/", "<", ">" };
        private readonly string[] _operatorWords = { "contains", "cast", "or", "and" };
        private readonly string _text;

        public string Message { get; private set; }
        public List<Token> Tokens { get; }

        public Tokenizer(string text)
        {
            _text = text.Replace(Environment.NewLine, "");
            Message = "";
            Tokens = new List<Token>();
        }

        public void Run()
        {
            using (var sr = new StringReader(_text))
            {
                try
                {
                    while (sr.Peek() != -1)
                    {
                        var c = (char) sr.Read();
                        if (c == ' ')
                            continue;

                        if (ParseString(c, sr))
                            continue;

                        if (ParseNumeric(c, sr))
                            continue;

                        if (ParseIdentifier(c, sr))
                            continue;

                        if (ParseHash(c, sr))
                            continue;

                        if (ParseOperator(c, sr))
                            continue;

                        if (ParseSeparator(c, sr))
                            continue;

                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    Message = e.Message;
                }
            }
        }

        private bool ParseString(char c, StringReader sr)
        {
            if (c != '"')
                return false;

            var sb = new StringBuilder();
            while (true)
            {
                if (sr.Peek() == -1)
                    throw new Exception("Unexpected token.");
                if (sr.Peek() == '"')
                    break;
                sb.Append((char) sr.Read());
            }
            Tokens.Add(new Token(sb.ToString(), TokenType.String));
            sr.Read();
            return true;
        }

        private bool ParseNumeric(char c, StringReader sr)
        {
            if (c < '0' || c > '9')
                return false;

            var sb = new StringBuilder();
            sb.Append(c);
            while ('0' <= sr.Peek() && sr.Peek() <= '9')
                sb.Append((char) sr.Read());
            Tokens.Add(new Token(sb.ToString(), TokenType.Numeric));
            return true;
        }

        private bool ParseIdentifier(char c, StringReader sr)
        {
            if (c < 'a' || c > 'z')
                return false;

            var sb = new StringBuilder();
            sb.Append(c);
            while (('a' <= sr.Peek() && sr.Peek() <= 'z') || sr.Peek() == '_')
                sb.Append((char) sr.Read());

            var str = sb.ToString();
            if (_keywords.Contains(str))
                Tokens.Add(new Token(str, TokenType.Keyword));
            else if (_operatorWords.Contains(str))
                Tokens.Add(new Token(str, TokenType.Operator));
            else
                Tokens.Add(new Token(str, TokenType.Variable));
            return true;
        }

        private bool ParseHash(char c, StringReader sr)
        {
            if (c != ':')
                return false;

            var sb = new StringBuilder();
            sb.Append(c);
            while (('a' <= sr.Peek() && sr.Peek() <= 'z') || sr.Peek() == '_')
                sb.Append((char) sr.Read());
            Tokens.Add(new Token(sb.ToString(), TokenType.Hash));
            return true;
        }

        private bool ParseOperator(char c, StringReader sr)
        {
            if ((c == '=' || c == '!' || c == '<' || c == '>') && sr.Peek() == '=')
                Tokens.Add(new Token(new string(new[] { c, (char) sr.Read() }), TokenType.Operator));
            else if ((c == '|' && sr.Peek() == '|') || (c == '&' && sr.Peek() == '&'))
                Tokens.Add(new Token(new string(new[] { c, (char) sr.Read() }), TokenType.Operator));
            else if (_operators.Contains(c.ToString()))
                Tokens.Add(new Token(c.ToString(), TokenType.Operator));
            else
                return false;
            return true;
        }

        private bool ParseSeparator(char c, StringReader sr)
        {
            if (c == '.' || c == ')' || c == '(')
            {
                Tokens.Add(new Token(c.ToString(), TokenType.Sepatator));
                return true;
            }
            return false;
        }
    }
}