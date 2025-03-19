// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;

namespace FunQL.Core.Constants.Parsers;

/// <summary>
/// Implementation of <see cref="IConstantParser"/> that uses <see cref="JsonSerializer"/> to parse the
/// <see cref="Constant"/> node.
/// </summary>
/// <param name="jsonSerializerOptions">Options for the <see cref="JsonSerializer"/>.</param>
/// <inheritdoc/>
public class JsonConstantParser(
    JsonSerializerOptions jsonSerializerOptions
) : IConstantParser
{
    /// <summary>Options for the <see cref="JsonSerializer"/>.</summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        state.IncreaseDepth();

        var expectedType = state.RequireContext<ConstantParseContext>().ExpectedType;
        // If Type is a primitive/struct (int, double, DateTime, etc.), we should make it Nullable as 'null' is a valid
        // constant, but JsonSerializer can only read 'null' if Type can be null
        expectedType = expectedType.ToNullableType();

        var token = state.CurrentToken();
        switch (token.Type)
        {
            case TokenType.String:
            case TokenType.Number:
            case TokenType.Boolean:
            case TokenType.Null:
            case TokenType.Object:
            case TokenType.Array:
                // Valid token for constants
                break;
            case TokenType.OpenBracket:
                // Handle OpenBracket token as Array 
                token = state.CurrentTokenAsArray();
                break;
            case TokenType.None:
            case TokenType.Eof:
            case TokenType.Identifier:
            case TokenType.OpenParen:
            case TokenType.CloseParen:
            case TokenType.Comma:
            case TokenType.Dot:
            case TokenType.Dollar:
            case TokenType.CloseBracket:
            default:
                // Invalid token for constants
                throw state.Lexer.SyntaxException(SyntaxErrors.TokenExpected("constant", token.Position, token.Text));
        }

        var metadata = state.CreateMetadata();

        try
        {
            var value = JsonSerializer.Deserialize(token.Text, expectedType, _jsonSerializerOptions);
            // Successfully parsed, so go to next token
            state.NextToken();

            state.DecreaseDepth();
            return new Constant(value, metadata);
        }
        catch (Exception e) when (e is JsonException or NotSupportedException)
        {
            throw new ParseException(ConstantParseErrors.FailedToParse(token.Text, token.Position), e);
        }
    }
}