// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Lexers;

/// <summary>The types of tokens that can be produced by a lexer.</summary>
public enum TokenType
{
    /// <summary>None token used for the initial token.</summary>
    None,

    /// <summary>End-of-file token indicating the end of the input stream.</summary>
    Eof,

    /// <summary>Token representing an identifier (e.g., field or function name).</summary>
    Identifier,

    /// <summary>Token representing an opening parenthesis '('.</summary>
    OpenParen,

    /// <summary>Token representing a closing parenthesis ')'.</summary>
    CloseParen,

    /// <summary>Token representing a comma ',' used in e.g. lists or function arguments.</summary>
    Comma,

    /// <summary>Token representing a dot '.' used for e.g. field paths.</summary>
    Dot,

    /// <summary>Token representing a dollar sign '$'.</summary>
    Dollar,

    /// <summary>Token representing an opening square bracket '['.</summary>
    OpenBracket,

    /// <summary>Token representing a closing square bracket ']'.</summary>
    CloseBracket,

    /// <summary>Token representing a string literal.</summary>
    String,

    /// <summary> Token representing a numeric literal.</summary>
    Number,

    /// <summary>Token representing a boolean value (true or false).</summary>
    Boolean,

    /// <summary>Token representing a null value.</summary>
    Null,

    /// <summary>Token representing an object.</summary>
    Object,

    /// <summary>Token representing an array.</summary>
    Array,
}