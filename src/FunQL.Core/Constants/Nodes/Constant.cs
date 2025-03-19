// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Constants.Nodes;

/// <summary>Node representing a constant.</summary>
/// <param name="Value">Value of the constant.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Constant(
    object? Value,
    Metadata? Metadata = null
) : QueryNode(Metadata);