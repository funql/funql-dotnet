// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Exceptions;

namespace FunQL.Linq.Common.Exceptions;

/// <summary>Represents an exception that occurs when something goes wrong during LINQ translation/execution.</summary>
public class LinqException : FunQLException
{
    /// <inheritdoc/>
    public LinqException(string message) : base(message)
    {
    }

    /// <inheritdoc/>
    public LinqException(string message, Exception innerException) : base(message, innerException)
    {
    }
}