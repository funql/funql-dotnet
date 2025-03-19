// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Lexers.Extensions;

/// <summary>Extensions for <see cref="TokenType"/>.</summary>
public static class TokenTypeExtensions
{
    /// <summary>Returns the description of <paramref name="tokenType"/>, e.g. to be used in an error message.</summary>
    /// <param name="tokenType">Type to get description for.</param>
    /// <returns>Description of the <paramref name="tokenType"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="tokenType"/> unknown.</exception>
    public static string GetDescription(this TokenType tokenType) => tokenType switch
    {
        TokenType.None => "",
        TokenType.Eof => "EOF",
        TokenType.Identifier => "identifier",
        TokenType.OpenParen => "(",
        TokenType.CloseParen => ")",
        TokenType.Comma => ",",
        TokenType.Dot => ".",
        TokenType.Dollar => "$",
        TokenType.OpenBracket => "[",
        TokenType.CloseBracket => "]",
        TokenType.String => "string",
        TokenType.Number => "number",
        TokenType.Boolean => "boolean",
        TokenType.Null => "null",
        TokenType.Object => "object",
        TokenType.Array => "array",
        _ => throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null)
    };
}