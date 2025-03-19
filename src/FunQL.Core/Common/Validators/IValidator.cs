// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>
/// Interface for a validator that can validate a <see cref="Request"/> for a given <see cref="IValidatorState"/>.
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validates the <paramref name="node"/> for given <paramref name="state"/>.
    ///
    /// Validation errors are added to <see cref="IValidatorState.Errors"/>.
    /// </summary>
    /// <param name="node">Node to validate.</param>
    /// <param name="state">State of the validator.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task to await validation.</returns>
    public Task Validate(Request node, IValidatorState state, CancellationToken cancellationToken);
}