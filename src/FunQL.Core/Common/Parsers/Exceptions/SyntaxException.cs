// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Parsers.Exceptions;

/// <summary>Represents an exception that occurs on FunQL syntax errors.</summary>
public class SyntaxException : ParseException
{
    /// <inheritdoc/>
    public SyntaxException(string message, int position, string text) : base(message)
    {
        Position = position;
        Text = text;
    }

    /// <inheritdoc/>
    public SyntaxException(string message, Exception innerException, int position, string text)
        : base(message, innerException)
    {
        Position = position;
        Text = text;
    }
}