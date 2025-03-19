// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Exceptions;

/// <summary>Exception type representing exceptions in the FunQL library.</summary>
// ReSharper disable once InconsistentNaming
public class FunQLException : Exception
{
    /// <summary>Initializes properties.</summary>
    public FunQLException()
    {
    }

    /// <summary>Initializes properties.</summary>
    /// <param name="message">Message for this exception.</param>
    public FunQLException(string? message) : base(message)
    {
    }

    /// <summary>Initializes properties.</summary>
    /// <param name="message">Message for this exception.</param>
    /// <param name="innerException">Inner exception that is the cause of this exception.</param>
    public FunQLException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}