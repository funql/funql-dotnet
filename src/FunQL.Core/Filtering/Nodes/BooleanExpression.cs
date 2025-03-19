// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// Base class for boolean expressions, which are expressions that evaluate to <c>true</c> or <c>false</c>.
/// </summary>
/// <param name="Name">Name of the expression.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public abstract record BooleanExpression(
    string Name,
    Metadata? Metadata
) : QueryNode(Metadata), IFunctionNode;