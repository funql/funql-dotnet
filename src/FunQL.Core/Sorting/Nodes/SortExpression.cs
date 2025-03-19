// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Sorting.Nodes;

/// <summary>Expression specifying how data should be sorted.</summary>
/// <param name="Name">Function name.</param>
/// <param name="FieldArgument">Field to sort on.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public abstract record SortExpression(
    string Name,
    FieldArgument FieldArgument,
    Metadata? Metadata
) : QueryNode(Metadata), IFunctionNode;