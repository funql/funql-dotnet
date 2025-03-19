// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Visitors;

/// <summary>State of a visitor.</summary>
public interface IVisitorState : IProcessorState<IVisitContext>
{
    /// <summary>
    /// Callback when entering given <paramref name="node"/>.
    ///
    /// Note that this should only be called for the sealed/final node type and not for e.g. an abstract type! This is
    /// to avoid entering the same node multiple times.
    /// </summary>
    /// <param name="node">Node being entered.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles callback.</returns>
    public Task OnEnter(QueryNode node, CancellationToken cancellationToken);

    /// <summary>Callback when exiting given <paramref name="node"/>.</summary>
    /// <param name="node">Node being exited.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles callback.</returns>
    public Task OnExit(QueryNode node, CancellationToken cancellationToken);
}