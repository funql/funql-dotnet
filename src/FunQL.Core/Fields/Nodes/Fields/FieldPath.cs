// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Fields.Nodes.Fields;

/// <summary>
/// Full path to a field, e.g. the fields <c>parent</c> and <c>child</c> that make up the field path
/// <c>parent.child</c>.
/// </summary>
/// <param name="Fields">All fields that make up the path to a specific field.</param>
/// <param name="Metadata"><inheritdoc cref="FieldArgument"/></param>
public sealed record FieldPath(
    IReadOnlyList<Field> Fields,
    Metadata? Metadata = null
) : FieldArgument(Metadata);