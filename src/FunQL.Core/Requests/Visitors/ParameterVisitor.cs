// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Counting.Visitors;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Visitors;
using FunQL.Core.Inputting.Nodes;
using FunQL.Core.Inputting.Visitors;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Limiting.Visitors;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Skipping.Nodes;
using FunQL.Core.Skipping.Visitors;
using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Visitors;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Default implementation of <see cref="IParameterVisitor{TState}"/>.</summary>
/// <param name="inputVisitor">Visitor to delegate <see cref="IInputVisitor{TState}"/> implementation to.</param>
/// <param name="filterVisitor">Visitor to delegate <see cref="IFilterVisitor{TState}"/> implementation to.</param>
/// <param name="sortVisitor">Visitor to delegate <see cref="ISortVisitor{TState}"/> implementation to.</param>
/// <param name="skipVisitor">Visitor to delegate <see cref="ISkipVisitor{TState}"/> implementation to.</param>
/// <param name="limitVisitor">Visitor to delegate <see cref="ILimitVisitor{TState}"/> implementation to.</param>
/// <param name="countVisitor">Visitor to delegate <see cref="ICountVisitor{TState}"/> implementation to.</param>
/// <inheritdoc/>
public class ParameterVisitor<TState>(
    IInputVisitor<TState> inputVisitor,
    IFilterVisitor<TState> filterVisitor,
    ISortVisitor<TState> sortVisitor,
    ISkipVisitor<TState> skipVisitor,
    ILimitVisitor<TState> limitVisitor,
    ICountVisitor<TState> countVisitor
) : IParameterVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IInputVisitor{TState}"/> implementation to.</summary>
    private readonly IInputVisitor<TState> _inputVisitor = inputVisitor;

    /// <summary>Visitor to delegate <see cref="IFilterVisitor{TState}"/> implementation to.</summary>
    private readonly IFilterVisitor<TState> _filterVisitor = filterVisitor;

    /// <summary>Visitor to delegate <see cref="ISortVisitor{TState}"/> implementation to.</summary>
    private readonly ISortVisitor<TState> _sortVisitor = sortVisitor;

    /// <summary>Visitor to delegate <see cref="ISkipVisitor{TState}"/> implementation to.</summary>
    private readonly ISkipVisitor<TState> _skipVisitor = skipVisitor;

    /// <summary>Visitor to delegate <see cref="ILimitVisitor{TState}"/> implementation to.</summary>
    private readonly ILimitVisitor<TState> _limitVisitor = limitVisitor;

    /// <summary>Visitor to delegate <see cref="ICountVisitor{TState}"/> implementation to.</summary>
    private readonly ICountVisitor<TState> _countVisitor = countVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(Parameter node, TState state, CancellationToken cancellationToken) => node switch
    {
        Input input => _inputVisitor.Visit(input, state, cancellationToken),
        Filter filter => _filterVisitor.Visit(filter, state, cancellationToken),
        Sort sort => _sortVisitor.Visit(sort, state, cancellationToken),
        Skip skip => _skipVisitor.Visit(skip, state, cancellationToken),
        Limit limit => _limitVisitor.Visit(limit, state, cancellationToken),
        Count count => _countVisitor.Visit(count, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };
}