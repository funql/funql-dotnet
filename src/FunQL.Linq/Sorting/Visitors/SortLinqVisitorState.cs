// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Sorting.Visitors;

/// <summary>Implementation of <see cref="ISortLinqVisitorState"/>.</summary>
/// <inheritdoc cref="LinqVisitorState"/>
public class SortLinqVisitorState(
    Expression source,
    bool handleNullPropagation = false,
    VisitorState.VisitDelegate? onEnter = null,
    VisitorState.VisitDelegate? onExit = null
) : LinqVisitorState(source, handleNullPropagation, onEnter, onExit), ISortLinqVisitorState
{
    /// <inheritdoc/>
    public IReadOnlyList<SortLinqExpression>? SortExpressions { get; set; }

    /// <inheritdoc/>
    public SortDirection? SortDirection { get; set; }
}