﻿// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The 'ends with' (enw) expression filters data to only include items where the <paramref name="FieldArgument"/>
/// string value ends with the <paramref name="Constant"/> string value.
/// </summary>
/// <param name="FieldArgument">The field whose value is being evaluated.</param>
/// <param name="Constant">Constant that <paramref name="FieldArgument"/> should end with.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record EndsWith(
    FieldArgument FieldArgument,
    Constant Constant,
    Metadata? Metadata = null
) : BooleanExpression(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "enw";
}