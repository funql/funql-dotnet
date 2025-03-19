// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Nodes.Functions;

/// <summary>
/// The ceiling function rounds the <paramref name="FieldPath"/> numeric value up to the nearest numeric value with no
/// decimal component.
/// </summary>
/// <param name="FieldPath">Field path to apply function on.</param>
/// <param name="Metadata"><inheritdoc cref="FieldArgument"/></param>
public sealed record Ceiling(
    FieldPath FieldPath,
    Metadata? Metadata = null
) : FieldFunction(FunctionName, FieldPath, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "ceiling";
}