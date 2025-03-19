// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Visitors;

/// <summary>Implementation of <see cref="IVisitorState"/>.</summary>
/// <param name="onEnter">Callback to call when a node is entered.</param>
/// <param name="onExit">Callback to call when a node is exited.</param>
public class VisitorState(
    VisitorState.VisitDelegate? onEnter = null,
    VisitorState.VisitDelegate? onExit = null
) : ProcessorState<IVisitContext>, IVisitorState
{
    /// <summary>Delegate for handling a visit.</summary>
    public delegate Task VisitDelegate(QueryNode node, IVisitorState state, CancellationToken cancellationToken);

    /// <summary>Callback to call when a node is entered.</summary>
    private readonly VisitDelegate? _onEnter = onEnter;

    /// <summary>Callback to call when a node is exited.</summary>
    private readonly VisitDelegate? _onExit = onExit;

    /// <inheritdoc/>
    public Task OnEnter(QueryNode node, CancellationToken cancellationToken) =>
        _onEnter?.Invoke(node, this, cancellationToken) ?? Task.CompletedTask;

    /// <inheritdoc/>
    public Task OnExit(QueryNode node, CancellationToken cancellationToken) =>
        _onExit?.Invoke(node, this, cancellationToken) ?? Task.CompletedTask;
}