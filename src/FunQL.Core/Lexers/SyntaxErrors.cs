// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Lexers;

/// <summary>Syntax error messages.</summary>
internal static class SyntaxErrors
{
    /// <summary>Invalid character '{character}' at position {position}.</summary>
    public static string InvalidCharacter(string character, int position) =>
        $"Invalid character '{character}' at position {position}.";

    /// <summary>Expression '{value}' at position {position} not closed properly.</summary>
    public static string UnbalancedExpression(string value, int position) =>
        $"Expression '{value}' at position {position} not closed properly.";

    /// <summary>String '{value}' at position {position} not closed properly.</summary>
    public static string UnbalancedString(string value, int position) =>
        $"String '{value}' at position {position} not closed properly.";

    /// <summary>Expected {tokenType} at position {position}, but found '{actual}'.</summary>
    public static string TokenExpected(string tokenType, int position, string actual) =>
        $"Expected {tokenType} at position {position}, but found '{actual}'.";

    /// <summary>Expected '{identifier}' at position {position}, but found '{actual}'.</summary>
    public static string IdentifierExpected(string identifier, int position, string actual) =>
        $"Expected '{identifier}' at position {position}, but found '{actual}'.";

    /// <summary>Expected digit at position {position}, but found '{actual}'.</summary>
    public static string DigitExpected(int position, string actual) =>
        $"Expected digit at position {position}, but found '{actual}'.";

    /// <summary>Unknown identifier '{identifier}' at position {position}.</summary>
    public static string UnknownIdentifier(string identifier, int position) =>
        $"Unknown identifier '{identifier}' at position {position}.";
}