// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>
/// Default translator that translates binary <see cref="BooleanExpression"/> to its corresponding binary
/// <see cref="Expression"/> (AND operation, OR operation, equality comparison or numerical comparison). 
/// </summary>
public abstract class BinaryExpressionLinqTranslator : IBinaryExpressionLinqTranslator
{
    /// <inheritdoc/>
    public virtual int Order => 0;

    /// <inheritdoc/>
    public virtual Expression? Translate(
        BooleanExpression node,
        Expression left,
        Expression right,
        ILinqVisitorState state
    )
    {
        // If types are not the same, one of the types could be Nullable and the other might not be, so make sure they
        // both are Nullable for binary expressions to work
        if (left.Type != right.Type)
        {
            left = left.EnsureNullableType();
            right = right.EnsureNullableType();
            // Ensure null Constants match the type of the other expression to make gt, gte, lt and lte work
            left = left.EnsureTypedNullConstant(right.Type);
            right = right.EnsureTypedNullConstant(left.Type);
        }

        return node switch
        {
            And =>
                Expression.AndAlso(left, right),
            Or =>
                Expression.OrElse(left, right),
            Equal =>
                Expression.Equal(left, right),
            NotEqual =>
                Expression.NotEqual(left, right),
            GreaterThan =>
                Expression.GreaterThan(left, right),
            GreaterThanOrEqual =>
                Expression.GreaterThanOrEqual(left, right),
            LessThan =>
                Expression.LessThan(left, right),
            LessThanOrEqual =>
                Expression.LessThanOrEqual(left, right),
            _ => null
        };
    }
}