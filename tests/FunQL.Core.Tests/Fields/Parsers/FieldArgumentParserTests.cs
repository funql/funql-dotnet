// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Lexers;
using Xunit;

namespace FunQL.Core.Tests.Fields.Parsers;

public class FieldArgumentParserTests
{
    [Theory]
    [MemberData(nameof(FieldPathTestData.NamedFieldPaths), MemberType = typeof(FieldPathTestData))]
    [MemberData(nameof(FieldPathTestData.MapFieldPaths), MemberType = typeof(FieldPathTestData))]
    public void ParseFieldArgument_ShouldReturnFieldPath_WhenTextIsFieldPath(string input, FieldPath _, string _1)
    {
        /* Arrange */
        var lexer = new StringLexer(input);
        var parserState = new ParserState(lexer);
        var parser = new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser()));

        /* Act */
        var result = parser.ParseFieldArgument(parserState);

        /* Assert */
        Assert.IsType<FieldPath>(result, exactMatch: false);
    }

    [Theory]
    [MemberData(nameof(FieldFunctionTestData.AllFunctions), MemberType = typeof(FieldFunctionTestData))]
    public void ParseFieldArgument_ShouldReturnFieldFunction_WhenTextIsFieldFunction(string text, FieldFunction _)
    {
        /* Arrange */
        var lexer = new StringLexer(text);
        var parserState = new ParserState(lexer);
        var parser = new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser()));

        /* Act */
        var result = parser.ParseFieldArgument(parserState);

        /* Assert */
        Assert.IsType<FieldFunction>(result, exactMatch: false);
    }
}