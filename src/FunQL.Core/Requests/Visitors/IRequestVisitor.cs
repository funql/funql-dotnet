// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Visitor for the <see cref="Request"/> nodes.</summary>
/// <typeparam name="TState">Type of the <see cref="IVisitorState"/> for this visitor.</typeparam>
public interface IRequestVisitor<in TState> : IParameterVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Request node, TState state, CancellationToken cancellationToken);
}