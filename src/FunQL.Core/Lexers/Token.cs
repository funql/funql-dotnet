// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Lexers;

/// <summary>Represents a lexical token produced by a lexer.</summary>
/// <param name="Type">Type of the token.</param>
/// <param name="Text">Textual representation of the token.</param>
/// <param name="Position">Position of the token in the input stream.</param>
public sealed record Token(
    TokenType Type,
    string Text,
    int Position
);