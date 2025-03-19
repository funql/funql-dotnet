// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Fields.Nodes.Fields;

/// <summary>Single field in a field path, e.g. <c>child</c> in field path <c>parent.child</c>.</summary>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public abstract record Field(Metadata? Metadata) : QueryNode(Metadata);