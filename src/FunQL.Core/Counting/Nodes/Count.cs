// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Counting.Nodes;

/// <summary>The count parameter specifies whether to provide a count of the total number of results.</summary>
/// <param name="Constant">The constant for the value.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Count(Constant Constant, Metadata? Metadata = null) : Parameter(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "count";
}