// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Nodes.Functions;

/// <summary>Function to apply to given <paramref name="FieldPath"/>.</summary>
/// <param name="Name">Name of the function.</param>
/// <param name="FieldPath">Field path to apply function on.</param>
/// <param name="Metadata"><inheritdoc cref="FieldArgument"/></param>
public abstract record FieldFunction(
    string Name,
    FieldPath FieldPath,
    Metadata? Metadata
) : FieldArgument(Metadata), IFunctionNode;