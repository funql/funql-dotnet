// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Inputting.Nodes;

/// <summary>
/// The input parameter specifies the input value for a specific request, e.g. the data required to create a new
/// resource.
/// </summary>
/// <param name="Constant">The constant for the value.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public record Input(
    Constant Constant,
    Metadata? Metadata = null
) : Parameter(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "input";
}