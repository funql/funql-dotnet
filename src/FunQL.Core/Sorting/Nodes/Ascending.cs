// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Sorting.Nodes;

/// <summary>The ascending (asc) sort expression sorts data in ascending order (smallest to largest).</summary>
/// <param name="FieldArgument">Field to sort on.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Ascending(
    FieldArgument FieldArgument,
    Metadata? Metadata = null
) : SortExpression(FunctionName, FieldArgument, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "asc";
}