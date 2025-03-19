// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Fields.Nodes;

/// <summary>Base class for a field argument: either a field or the function to apply to the field.</summary>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public abstract record FieldArgument(Metadata? Metadata) : QueryNode(Metadata);