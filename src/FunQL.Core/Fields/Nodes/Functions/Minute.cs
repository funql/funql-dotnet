// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Nodes.Functions;

/// <summary>
/// The minute function returns the minute component of the DateTime <paramref name="FieldPath"/> value.
/// </summary>
/// <param name="FieldPath">Field path to apply function on.</param>
/// <param name="Metadata"><inheritdoc cref="FieldArgument"/></param>
public sealed record Minute(
    FieldPath FieldPath,
    Metadata? Metadata = null
) : FieldFunction(FunctionName, FieldPath, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "minute";
}