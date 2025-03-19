// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Diagnostics;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;

namespace FunQL.Core.Fields.Parsers.Fields;

/// <summary>Default implementation of <see cref="IFieldPathParser"/>.</summary>
public class FieldPathParser : IFieldPathParser
{
    /// <inheritdoc/>
    public FieldPath ParseFieldPath(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        var fields = new List<Field>
        {
            ParseField(state)
        };
        var hasNext = HasNextField();
        while (hasNext)
        {
            if (state.CurrentToken().Type == TokenType.Dot)
                state.NextToken();
            fields.Add(ParseField(state));
            hasNext = HasNextField();
        }

        state.DecreaseDepth();
        return new FieldPath(fields, metadata);

        bool HasNextField() => state.CurrentToken().Type is TokenType.Dot or TokenType.OpenBracket;
    }

    /// <inheritdoc/>
    public Field ParseField(IParserState state)
    {
        var token = state.CurrentToken();
        switch (token.Type)
        {
            case TokenType.Identifier:
            case TokenType.OpenBracket:
                return ParseNamedField(state);
            case TokenType.Dollar:
                return ParseListItemField(state);
            case TokenType.None:
            case TokenType.Eof:
            case TokenType.OpenParen:
            case TokenType.CloseParen:
            case TokenType.Comma:
            case TokenType.Dot:
            case TokenType.CloseBracket:
            case TokenType.String:
            case TokenType.Number:
            case TokenType.Boolean:
            case TokenType.Null:
            case TokenType.Object:
            case TokenType.Array:
            default:
                throw state.Lexer.SyntaxException(SyntaxErrors.TokenExpected("field", token.Position, token.Text));
        }
    }

    /// <inheritdoc/>
    public NamedField ParseNamedField(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        var name = state.CurrentToken().Type == TokenType.Identifier
            ? ParseDotNotationField(state)
            : ParseBracketNotationField(state);

        state.DecreaseDepth();
        return new NamedField(name, metadata);
    }

    /// <inheritdoc/>
    public ListItemField ParseListItemField(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectToken(TokenType.Dollar);
        state.ExpectIdentifier("it");
        var parent = state.RequireContext<ListItemParseContext>().Parent;

        state.DecreaseDepth();
        return new ListItemField(parent, metadata);
    }

    /// <summary>Parses <paramref name="text"/> as string.</summary>
    /// <param name="text">Text to parse.</param>
    /// <returns>The parsed string.</returns>
    private static string ParseString(string text)
    {
        Debug.Assert(text.StartsWith('"') && text.EndsWith('"'), "String must start and end with double quotes");

        // Remove start and end quotes
        return text.Substring(1, text.Length - 2)
            // Unescape any escaped quotes
            .Replace("\\\"", "\"");
    }

    /// <summary>Tries to parse named field that uses dot notation ('parent.child').</summary>
    private static string ParseDotNotationField(IParserState state) => state.ExpectToken(TokenType.Identifier).Text;

    /// <summary>Tries to parse named field that uses bracket notation ('parent["child"]').</summary>
    private static string ParseBracketNotationField(IParserState state)
    {
        state.ExpectToken(TokenType.OpenBracket);
        var keyToken = state.ExpectToken(TokenType.String);
        state.ExpectToken(TokenType.CloseBracket);

        return ParseString(keyToken.Text);
    }
}