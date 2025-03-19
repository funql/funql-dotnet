// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Fields.Nodes.Fields;

/// <summary>
/// Field referencing an item in a list, e.g. <c>$it</c> in field path <c>items.$it.child</c>. The <c>$it</c> field in
/// this case references any item in the <c>items</c> list, which has no explicit name.
/// </summary>
/// <param name="ListField">Field path to the list field that this field is an item of.</param>
/// <param name="Metadata"><inheritdoc cref="Field"/></param>
public sealed record ListItemField(
    FieldPath ListField,
    Metadata? Metadata = null
) : Field(Metadata);