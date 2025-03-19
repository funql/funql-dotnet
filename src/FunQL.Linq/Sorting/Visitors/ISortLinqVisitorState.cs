// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Sorting.Nodes;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Sorting.Visitors;

/// <summary>State of a LINQ visitor specific to <see cref="Sort"/> nodes.</summary>
public interface ISortLinqVisitorState : ILinqVisitorState
{
    /// <summary>The list with <see cref="SortLinqExpression"/> that got translated.</summary>
    public IReadOnlyList<SortLinqExpression>? SortExpressions { get; set; }

    /// <summary>Direction for current sort expression.</summary>
    public SortDirection? SortDirection { get; set; }
}