// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>Validation rule for validation of a <see cref="QueryNode"/>.</summary>
public interface IValidationRule
{
    /// <summary>Type of the <see cref="QueryNode"/> that this rule can validate.</summary>
    public Type NodeType { get; }

    /// <summary>
    /// Validates the <paramref name="node"/> for given <paramref name="state"/> upon entering the
    /// <paramref name="node"/>.
    ///
    /// Validation errors are added to <see cref="IValidatorState.Errors"/>.
    /// </summary>
    /// <param name="node">Node to validate.</param>
    /// <param name="state">State of the validator.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task to await validation.</returns>
    public Task ValidateOnEnter(QueryNode node, IValidatorState state, CancellationToken cancellationToken);

    /// <summary>
    /// Validates the <paramref name="node"/> for given <paramref name="state"/> upon exiting the
    /// <paramref name="node"/>.
    ///
    /// Validation errors are added to <see cref="IValidatorState.Errors"/>.
    /// </summary>
    /// <param name="node">Node to validate.</param>
    /// <param name="state">State of the validator.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task to await validation.</returns>
    public Task ValidateOnExit(QueryNode node, IValidatorState state, CancellationToken cancellationToken);
}