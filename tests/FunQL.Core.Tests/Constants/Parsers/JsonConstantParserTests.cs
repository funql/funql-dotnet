// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Tests.TestData;
using FunQL.Core.Tests.TestData.Models;
using Xunit;

namespace FunQL.Core.Tests.Constants.Parsers;

public class JsonConstantParserTests
{
    public static TheoryData<string, Type, object?> GetTestData() => new()
    {
        { "\"value\"", typeof(string), "value" },
        { "123", typeof(int), 123 },
        { "null", typeof(int?), null },
        {
            "{ \"id\": \"9535bfa7-341e-4608-93a4-0cd8c5055acd\", \"name\": \"Hans Burkhard Schlömer\" }",
            typeof(Designer),
            new Designer
            {
                Id = Guid.Parse("9535bfa7-341e-4608-93a4-0cd8c5055acd"),
                Name = "Hans Burkhard Schlömer"
            }
        },
        { "\"STAR_WARS\"", typeof(Theme), Theme.StarWars }
    };

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void ParseConstant_ShouldParseInputAsExpectedValueForExpectedType(
        string input,
        Type expectedType,
        object? expectedValue
    )
    {
        /* Arrange */
        var parser = new JsonConstantParser(JsonDefaults.JsonSerializerOptions);
        var parserState = new ParserState(new StringLexer(input));
        parserState.EnterContext(new ConstantParseContext(expectedType));

        /* Act */
        var result = parser.ParseConstant(parserState);

        /* Assert */
        Assert.Equal(expectedValue, result.Value);
    }

    [Fact]
    public void ParseConstant_ShouldThrowInvalidOperationException_WhenConstantParseContextMissing()
    {
        /* Arrange */
        var parser = new JsonConstantParser(new JsonSerializerOptions());
        var parserState = new ParserState(new StringLexer(""));

        /* Act */
        var action = () => parser.ParseConstant(parserState);

        /* Assert */
        Assert.Throws<InvalidOperationException>(action);
    }

    [Theory]
    [InlineData("")] // TokenType.Eof
    [InlineData("filter")] // TokenType.Identifier
    [InlineData("(")] // TokenType.OpenParen
    [InlineData(")")] // TokenType.CloseParen
    [InlineData(",")] // TokenType.Comma
    [InlineData(".")] // TokenType.Dot
    [InlineData("$")] // TokenType.Dollar
    [InlineData("]")] // TokenType.CloseBracket
    public void ParseConstant_ShouldThrowSyntaxException_WhenTokenInvalid(string text)
    {
        /* Arrange */
        var parser = new JsonConstantParser(new JsonSerializerOptions());
        var parserState = new ParserState(new StringLexer(text));
        // Type does not matter, should throw before parsing
        parserState.EnterContext(new ConstantParseContext(typeof(string)));

        /* Act */
        var action = () => parser.ParseConstant(parserState);

        /* Assert */
        Assert.Throws<SyntaxException>(action);
    }

    [Fact]
    public void ParseConstant_ShouldThrowParseException_WhenConstantParsingFails()
    {
        /* Arrange */
        var parser = new JsonConstantParser(new JsonSerializerOptions());
        var parserState = new ParserState(new StringLexer("{ }"));
        parserState.EnterContext(new ConstantParseContext(typeof(string)));

        /* Act */
        var action = () => parser.ParseConstant(parserState);

        /* Assert */
        Assert.Throws<ParseException>(action);
    }
}