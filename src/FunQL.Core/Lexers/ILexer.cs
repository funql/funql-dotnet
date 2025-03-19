// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Lexers;

/// <summary>Interface for a lexer used to tokenize an input stream.</summary>
public interface ILexer
{
    /// <summary>Current token.</summary>
    public Token CurrentToken { get; }

    /// <summary>Peeks at the next token in the input stream without advancing the lexer position.</summary>
    /// <returns>The next token in the input stream.</returns>
    public Token PeekNextToken();

    /// <summary>Advances the lexer to the next token in the input stream and returns the new current token.</summary>
    /// <returns>The next token in the input stream.</returns>
    public Token NextToken();

    /// <summary>
    /// Inform the lexer that an <see cref="TokenType.OpenBracket"/> must be handled as an
    /// <see cref="TokenType.Array"/>. This must be explicitly called because a lexer can't determine whether token is a
    /// bracket or an array.
    /// </summary>
    /// <returns>Current <see cref="TokenType.OpenBracket"/> token as an <see cref="TokenType.Array"/> token.</returns>
    public Token CurrentTokenAsArray();
}