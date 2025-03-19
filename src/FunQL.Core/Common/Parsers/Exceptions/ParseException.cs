// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Exceptions;

namespace FunQL.Core.Common.Parsers.Exceptions;

/// <summary>Represents an exception that occurs when something goes wrong during parsing of a query.</summary>
public class ParseException : FunQLException
{
    /// <summary>Position in query that is being parsed.</summary>
    public int? Position { get; set; }

    /// <summary>The text for which an error occured.</summary>
    public string? Text { get; set; }

    /// <inheritdoc/>
    public ParseException()
    {
    }

    /// <inheritdoc/>
    public ParseException(string? message) : base(message)
    {
    }

    /// <inheritdoc/>
    public ParseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}