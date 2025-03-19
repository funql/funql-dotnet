// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers.Exceptions;

namespace FunQL.Core.Lexers.Extensions;

/// <summary>Extensions for <see cref="ILexer"/>.</summary>
public static class LexerExtensions
{
    /// <summary>
    /// Expects the current token to be of given <paramref name="tokenType"/> and advances the lexer to the next token.
    /// </summary>
    /// <param name="lexer">The lexer instance.</param>
    /// <param name="tokenType">The expected type of the token.</param>
    /// <returns>The expected token.</returns>
    /// <exception cref="Exception">Thrown if the expected token type is not found.</exception>
    public static Token ExpectToken(this ILexer lexer, TokenType tokenType)
    {
        var token = lexer.CurrentToken;
        if (token.Type == tokenType)
        {
            lexer.NextToken();
            return token;
        }

        throw SyntaxException(
            lexer,
            SyntaxErrors.TokenExpected(tokenType.GetDescription(), token.Position, token.Text)
        );
    }

    /// <summary>
    /// Expects an optional token of given <paramref name="tokenType"/>. If found, advances the lexer to the next token.
    /// </summary>
    /// <param name="lexer">The lexer instance.</param>
    /// <param name="tokenType">The expected type of the token.</param>
    /// <returns>True if the optional token is found, false otherwise.</returns>
    public static bool ExpectOptionalToken(this ILexer lexer, TokenType tokenType)
    {
        if (lexer.CurrentToken.Type != tokenType)
            return false;

        lexer.NextToken();
        return true;
    }

    /// <summary>
    /// Expects the current token to be given <paramref name="identifier"/> and advances the lexer to the next token.
    /// </summary>
    /// <param name="lexer">The lexer instance.</param>
    /// <param name="identifier">The expected identifier.</param>
    /// <exception cref="Exception">Thrown if the expected identifier is not found.</exception>
    public static void ExpectIdentifier(this ILexer lexer, string identifier)
    {
        var token = lexer.CurrentToken;
        if (token.Type == TokenType.Identifier && token.Text == identifier)
        {
            lexer.NextToken();
        }
        else
        {
            throw SyntaxException(lexer, SyntaxErrors.IdentifierExpected(identifier, token.Position, token.Text));
        }
    }

    /// <summary>
    /// Creates a <see cref="SyntaxException"/> for given <paramref name="message"/> and current <paramref name="lexer"/>
    /// state.
    /// </summary>
    /// <param name="lexer">Lexer for which to create exception.</param>
    /// <param name="message">Message for the exception.</param>
    /// <returns>The <see cref="SyntaxException"/>.</returns>
    public static SyntaxException SyntaxException(this ILexer lexer, string message)
    {
        var token = lexer.CurrentToken;
        return new SyntaxException(message, token.Position, token.Text);
    }
}