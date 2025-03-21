// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Lexers;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Requests.Parsers;
using Moq;
using Xunit;

namespace FunQL.Core.Tests.Requests.Parsers;

public class RequestParserTests
{
    private sealed record EmptyParameter() : Parameter("empty", null);
    
    /// <summary>
    /// Creates the <see cref="RequestParser"/>, mocking <see cref="IParameterParser"/> to always return
    /// <see cref="EmptyParameter"/> to simplify tests.
    /// </summary>
    private static RequestParser CreateRequestParser()
    {
        var mock = new Mock<IParameterParser>();
        mock.Setup(it => it.ParseParameter(It.IsAny<IParserState>()))
            .Returns<IParserState>(it => new EmptyParameter());

        return new RequestParser(mock.Object);
    }
    
    [Fact]
    public void ParseRequest_ShouldReturnRequestNode_WhenTextIsValidRequest()
    {
        /* Arrange */
        var lexer = new StringLexer("test()");
        var parserState = new ParserState(lexer);
        var parser = CreateRequestParser();

        /* Act */
        var result = parser.ParseRequest(parserState);

        /* Assert */
        Assert.Equal("test", result.Name);
        Assert.Empty(result.Parameters);
    }

    [Fact]
    public void ParseRequest_ShouldThrowSyntaxException_WhenTextContainsMoreTokens()
    {
        /* Arrange */
        var lexer = new StringLexer("test()invalid");
        var parserState = new ParserState(lexer);
        var parser = CreateRequestParser();

        /* Act */
        var action = () => parser.ParseRequest(parserState);

        /* Assert */
        var exception = Assert.Throws<SyntaxException>(action);
        Assert.Equal("invalid", exception.Text);
    }
}