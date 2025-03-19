// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>
/// Abstract implementation of <see cref="IValidationRule{T}"/> to simplify implementation, only having to override
/// <see cref="ValidateOnEnter"/> and/or <see cref="ValidateOnExit"/>.
/// </summary>
/// <inheritdoc/>
public abstract class AbstractValidationRule<T> : IValidationRule<T> where T : QueryNode
{
    /// <inheritdoc/>
    public Type NodeType { get; } = typeof(T);

    /// <inheritdoc/>
    public virtual Task ValidateOnEnter(T node, IValidatorState state, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    /// <inheritdoc/>
    public virtual Task ValidateOnExit(T node, IValidatorState state, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    /// <inheritdoc/>
    Task IValidationRule.ValidateOnEnter(QueryNode node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node is not T queryNode)
            throw new ArgumentException($"Node must be of type {typeof(T)}", nameof(node));
        return ValidateOnEnter(queryNode, state, cancellationToken);
    }

    /// <inheritdoc/>
    Task IValidationRule.ValidateOnExit(QueryNode node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node is not T queryNode)
            throw new ArgumentException($"Node must be of type {typeof(T)}", nameof(node));
        return ValidateOnExit(queryNode, state, cancellationToken);
    }
}