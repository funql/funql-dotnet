// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors.Extensions;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;

namespace FunQL.Core.Common.Parsers.Extensions;

/// <summary>Extensions for <see cref="IParserState"/>.</summary>
public static class ParserStateExtensions
{
    /// <summary>
    /// Expects the current token to be of given <paramref name="tokenType"/> and advances the lexer to the next token.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <param name="tokenType">The expected type of the token.</param>
    /// <returns>The expected token.</returns>
    /// <exception cref="Exception">Thrown if the expected token type is not found.</exception>
    public static Token ExpectToken(this IParserState state, TokenType tokenType) =>
        state.Lexer.ExpectToken(tokenType);

    /// <summary>
    /// Expects an optional token of given <paramref name="tokenType"/>. If found, advances the lexer to the next token.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <param name="tokenType">The expected type of the token.</param>
    /// <returns>True if the optional token is found, false otherwise.</returns>
    public static bool ExpectOptionalToken(this IParserState state, TokenType tokenType) =>
        state.Lexer.ExpectOptionalToken(tokenType);

    /// <summary>
    /// Expects the current token to be given <paramref name="identifier"/> and advances the lexer to the next token.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <param name="identifier">The expected identifier.</param>
    /// <exception cref="Exception">Thrown if the expected identifier is not found.</exception>
    public static void ExpectIdentifier(this IParserState state, string identifier) =>
        state.Lexer.ExpectIdentifier(identifier);

    /// <summary>Current token.</summary>
    /// <param name="state">The state instance.</param>
    /// <returns>Current token.</returns>
    public static Token CurrentToken(this IParserState state) => state.Lexer.CurrentToken;

    /// <summary>Peeks at the next token in the input stream without advancing the lexer position.</summary>
    /// <param name="state">The state instance.</param>
    /// <returns>The next token in the input stream.</returns>
    public static Token PeekNextToken(this IParserState state) => state.Lexer.PeekNextToken();

    /// <summary>Advances the lexer to the next token in the input stream and returns the new current token.</summary>
    /// <param name="state">The state instance.</param>
    /// <returns>The next token in the input stream.</returns>
    public static Token NextToken(this IParserState state) => state.Lexer.NextToken();

    /// <summary>
    /// Inform the lexer that an <see cref="TokenType.OpenBracket"/> must be handled as an
    /// <see cref="TokenType.Array"/>. This must be explicitly called because a lexer can't determine whether token is a
    /// bracket or an array.
    /// </summary>
    /// <param name="state">The state instance.</param>
    /// <returns>Current <see cref="TokenType.OpenBracket"/> token as an <see cref="TokenType.Array"/> token.</returns>
    public static Token CurrentTokenAsArray(this IParserState state) => state.Lexer.CurrentTokenAsArray();


    /// <summary>Find context of type <typeparamref name="T"/>.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context or <c>null</c> if not found.</returns>
    public static T? FindContext<T>(this IParserState state) where T : IParseContext =>
        state.FindContext<IParseContext, T>();

    /// <summary>Require context for given <paramref name="type"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <param name="type">Type of the context to find.</param>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static IParseContext RequireContext(this IParserState state, Type type) =>
        state.RequireContext<IParseContext>(type);

    /// <summary>Require context of type <typeparamref name="T"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static T RequireContext<T>(this IParserState state) where T : IParseContext =>
        state.RequireContext<IParseContext, T>();

    /// <summary>Creates <see cref="Metadata"/> for current token.</summary>
    /// <param name="state">State to create <see cref="Metadata"/> for.</param>
    /// <returns>The <see cref="Metadata"/>.</returns>
    public static Metadata CreateMetadata(this IParserState state)
    {
        var token = state.CurrentToken();
        return new Metadata(token.Position);
    }
}