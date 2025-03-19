// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Visitors;

/// <summary>Visitor for the <see cref="Limit"/> nodes.</summary>
/// <typeparam name="TState">Type of the <see cref="IVisitorState"/> for this visitor.</typeparam>
public interface ILimitVisitor<in TState> : IConstantVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Limit node, TState state, CancellationToken cancellationToken);
}