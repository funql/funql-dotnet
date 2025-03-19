// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The 'not' expression filters data to only include items where <paramref name="BooleanExpression"/> does not evaluate
/// to <c>true</c>. In other words, the expression evaluates to <c>true</c> if <paramref name="BooleanExpression"/>
/// evaluates to <c>false</c>, and vice versa.
/// </summary>
/// <param name="BooleanExpression">The expression to be negated.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Not(
    BooleanExpression BooleanExpression,
    Metadata? Metadata = null
) : BooleanExpression(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "not";
}