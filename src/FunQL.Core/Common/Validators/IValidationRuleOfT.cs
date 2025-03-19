// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>Validation rule for validation of a <see cref="QueryNode"/> of type <typeparamref name="T"/>.</summary>
/// <typeparam name="T">Type of the <see cref="QueryNode"/> this rule can validate.</typeparam>
public interface IValidationRule<in T> : IValidationRule where T : QueryNode
{
    /// <inheritdoc cref="IValidationRule.ValidateOnEnter"/>
    public Task ValidateOnEnter(T node, IValidatorState state, CancellationToken cancellationToken);

    /// <inheritdoc cref="IValidationRule.ValidateOnEnter"/>
    public Task ValidateOnExit(T node, IValidatorState state, CancellationToken cancellationToken);
}