// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The 'less than or equal' (lte) expression filters data to only include items where <paramref name="FieldArgument"/>
/// is less than or equal to <paramref name="Constant"/>.
/// </summary>
/// <param name="FieldArgument">The field whose value is being evaluated.</param>
/// <param name="Constant">Constant that <paramref name="FieldArgument"/> is compared with.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record LessThanOrEqual(
    FieldArgument FieldArgument,
    Constant Constant,
    Metadata? Metadata = null
) : BooleanExpression(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "lte";
}