// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Lexers;
using Xunit;

namespace FunQL.Core.Tests.Fields.Parsers.Functions;

public class FieldFunctionParserTests
{
    [Theory]
    [MemberData(nameof(FieldFunctionTestData.AllFunctions), MemberType = typeof(FieldFunctionTestData))]
    public void ParseFieldFunction_ShouldReturnFieldFunction_WhenTextIsFieldFunction(
        string text,
        FieldFunction expected
    )
    {
        /* Arrange */
        var lexer = new StringLexer(text);
        var parserState = new ParserState(lexer);
        var parser = new FieldFunctionParser(new FieldPathParser());

        /* Act */
        var result = parser.ParseFieldFunction(parserState);

        /* Assert */
        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void ParseFieldFunction_ShouldThrowSyntaxException_WhenFieldFunctionUnknown()
    {
        /* Arrange */
        var lexer = new StringLexer("nonExistent(field)");
        var parserState = new ParserState(lexer);
        var parser = new FieldFunctionParser(new FieldPathParser());

        /* Act */
        var action = () => parser.ParseFieldFunction(parserState);

        /* Assert */
        Assert.Throws<SyntaxException>(action);
    }
}