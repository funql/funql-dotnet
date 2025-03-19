// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Default implementation of <see cref="IRequestVisitor{TState}"/>.</summary>
/// <param name="parameterVisitor">
/// Visitor to delegate <see cref="IParameterVisitor{TState}"/> implementation to.
/// </param>
/// <inheritdoc/>
public class RequestVisitor<TState>(
    IParameterVisitor<TState> parameterVisitor
) : IRequestVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IParameterVisitor{TState}"/> implementation to.</summary>
    private readonly IParameterVisitor<TState> _parameterVisitor = parameterVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(Request node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            foreach (var parameter in node.Parameters)
                await Visit(parameter, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Parameter node, TState state, CancellationToken cancellationToken) =>
        _parameterVisitor.Visit(node, state, cancellationToken);
}