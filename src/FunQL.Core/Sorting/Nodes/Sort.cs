// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Sorting.Nodes;

/// <summary>The sort parameter specifies one or more expressions to sort the data with.</summary>
/// <param name="Expressions">Expressions to sort data with.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Sort(
    IReadOnlyList<SortExpression> Expressions,
    Metadata? Metadata = null
) : Parameter(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "sort";
}