﻿using System;

using Vulpecula.Scripting.Lexer;
using Vulpecula.Scripting.Objects;
using Vulpecula.Scripting.Parser.Expressions;

namespace Vulpecula.Scripting.Parser
{
    public class ScriptParser
    {
        private readonly CompilationUnit _compilationUnit;
        private readonly TokenReader _tokenReader;

        public DataSource DataSource { get; internal set; }

        public ScriptParser(TokenReader tokenReader)
        {
            _tokenReader = tokenReader;
            _compilationUnit = new CompilationUnit(this);
        }

        public void Parse()
        {
            _compilationUnit.Parse(_tokenReader);
        }

        public Func<T, bool> Compile<T>()
        {
            return _compilationUnit.AsExpressionTree<T>().Compile();
        }
    }
}