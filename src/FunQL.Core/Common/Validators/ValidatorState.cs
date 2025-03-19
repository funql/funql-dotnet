// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Validators;

/// <summary>Implementation of <see cref="IValidatorState"/>.</summary>
/// <param name="rules">Validation rules.</param>
public class ValidatorState(params IValidationRule[] rules) : ProcessorState<IValidateContext>, IValidatorState
{
    /// <summary>Rule to delegate validation to.</summary>
    private readonly CompositeValidationRule _rootRule = new(rules);

    /// <summary>Current validation errors.</summary>
    private readonly List<ValidationError> _errors = [];

    /// <inheritdoc/>
    public bool HasErrors => _errors.Count > 0;

    /// <inheritdoc/>
    public IEnumerable<ValidationError> Errors => _errors;

    /// <inheritdoc/>
    public void AddError(ValidationError error)
    {
        _errors.Add(error);
    }

    /// <inheritdoc/>
    public Task ValidateOnEnter(QueryNode node, CancellationToken cancellationToken) =>
        _rootRule.ValidateOnEnter(node, this, cancellationToken);

    /// <inheritdoc/>
    public Task ValidateOnExit(QueryNode node, CancellationToken cancellationToken) =>
        _rootRule.ValidateOnExit(node, this, cancellationToken);
}