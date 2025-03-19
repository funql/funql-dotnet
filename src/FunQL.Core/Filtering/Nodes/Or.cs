// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The 'or' expression filters data to only include items where either <paramref name="Left"/> or
/// <paramref name="Right"/> evaluates to <c>true</c>.
/// </summary>
/// <param name="Left">Left side of the expression.</param>
/// <param name="Right">Right side of the expression.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Or(
    BooleanExpression Left,
    BooleanExpression Right,
    Metadata? Metadata = null
) : BooleanExpression(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "or";
}