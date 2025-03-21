// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Lexers;
using Xunit;

namespace FunQL.Core.Tests.Lexers;

public class StringLexerTests
{
    [Fact]
    public void NextToken_ShouldNotThrow()
    {
        /* Arrange */
        var lexer = new StringLexer("""filter(eq(lower(name),"mathijs"))""");

        /* Act */
        var exception = Record.Exception(() =>
        {
            while (true)
            {
                var token = lexer.NextToken();
                if (token.Type == TokenType.Eof)
                    break;
            }
        });

        /* Assert */
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("", TokenType.Eof)]
    [InlineData("filter", TokenType.Identifier)]
    [InlineData("(", TokenType.OpenParen)]
    [InlineData(")", TokenType.CloseParen)]
    [InlineData(",", TokenType.Comma)]
    [InlineData(".", TokenType.Dot)]
    [InlineData("$", TokenType.Dollar)]
    [InlineData("[", TokenType.OpenBracket)]
    [InlineData("]", TokenType.CloseBracket)]
    [InlineData("\"\"", TokenType.String)]
    [InlineData("\"value\"", TokenType.String)]
    [InlineData("0", TokenType.Number)]
    [InlineData("1", TokenType.Number)]
    [InlineData("-1", TokenType.Number)]
    [InlineData("0.0", TokenType.Number)]
    [InlineData("1.0", TokenType.Number)]
    [InlineData("-1.0", TokenType.Number)]
    [InlineData("true", TokenType.Boolean)]
    [InlineData("false", TokenType.Boolean)]
    [InlineData("null", TokenType.Null)]
    [InlineData("{ }", TokenType.Object)]
    // TokenType.Array requires call to lexer.CurrentTokenAsArray(), so not tested here
    public void CurrentToken_ShouldReturnExpectedTokenType(string text, TokenType tokenType)
    {
        /* Arrange */
        var lexer = new StringLexer(text);

        /* Act */
        var result = lexer.CurrentToken;

        /* Assert */
        Assert.Equal(tokenType, result.Type);
    }

    [Fact]
    public void CurrentTokenAsArray_ShouldReturnTokenTypeArray_WhenTokenIsArray()
    {
        /* Arrange */
        var lexer = new StringLexer("[1, 2, 3]");

        /* Act */
        var result = lexer.CurrentTokenAsArray();

        /* Assert */
        Assert.Equal(TokenType.Array, result.Type);
    }
}