// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Visitors;

/// <summary>Visitor for the <see cref="Filter"/> nodes.</summary>
/// <typeparam name="TState">Type of the <see cref="IVisitorState"/> for this visitor.</typeparam>
public interface IFilterVisitor<in TState> : IFieldArgumentVisitor<TState>, IConstantVisitor<TState>
    where TState : IVisitorState
{
    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Filter node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(BooleanExpression node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(And node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Or node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Not node, TState state, CancellationToken cancellationToken);

    /* Collection */
    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(All node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Any node, TState state, CancellationToken cancellationToken);

    /* Boolean expressions */
    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Equal node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(NotEqual node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(GreaterThan node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(GreaterThanOrEqual node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(LessThan node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(LessThanOrEqual node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(Has node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(StartsWith node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(EndsWith node, TState state, CancellationToken cancellationToken);

    /// <summary>Visits <paramref name="node"/> for given <paramref name="state"/>.</summary>
    /// <param name="node">Node being visited.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    public Task Visit(RegexMatch node, TState state, CancellationToken cancellationToken);
}