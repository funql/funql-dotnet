// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Exceptions;

namespace FunQL.Core.Common.Validators.Exceptions;

/// <summary>Represents an exception that occurs on validation errors.</summary>
public class ValidationException : FunQLException
{
    /// <summary>Validation errors.</summary>
    public IEnumerable<ValidationError> Errors { get; }

    /// <summary>Initializes properties.</summary>
    /// <param name="message">Message for this exception.</param>
    /// <param name="errors">Validation errors.</param>
    public ValidationException(string message, IEnumerable<ValidationError> errors) : base(message)
    {
        Errors = errors;
    }

    /// <summary>Initializes properties.</summary>
    /// <param name="errors">Validation errors.</param>
    public ValidationException(IEnumerable<ValidationError> errors) : this("The input was not valid.", errors)
    {
    }
}