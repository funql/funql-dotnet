// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Filtering.Nodes;

/// <summary>
/// The 'any' expression filters data to only include items where at least one element in the collection specified by 
/// <paramref name="FieldPath"/> evaluates to <c>true</c> for given <paramref name="Predicate"/>.
/// </summary>
/// <param name="FieldPath">The path to the collection field to be evaluated.</param>
/// <param name="Predicate">The predicate to evaluate against each element in the collection.</param>
/// <param name="Metadata"><inheritdoc cref="QueryNode"/></param>
public sealed record Any(
    FieldPath FieldPath,
    BooleanExpression Predicate,
    Metadata? Metadata = null
) : BooleanExpression(FunctionName, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "any";
}