// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Fields.Nodes.Fields;

/// <summary>Field referenced by its name, e.g. <c>child</c> in field path <c>parent.child</c>.</summary>
/// <param name="Name">Name of the field.</param>
/// <param name="Metadata"><inheritdoc cref="Field"/></param>
public sealed record NamedField(
    string Name,
    Metadata? Metadata = null
) : Field(Metadata);