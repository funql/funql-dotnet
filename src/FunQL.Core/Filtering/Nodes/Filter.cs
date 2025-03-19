// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The filter parameter specifies the expression to filter the data with, where only the items are included for which
/// <paramref name="Expression"/> evaluates to <c>true</c>.
/// </summary>
/// <param name="Expression">The expression to filter data with.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Filter(
    BooleanExpression Expression,
    Metadata? Metadata = null
) : Parameter(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "filter";
}