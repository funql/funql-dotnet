// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>Translator for <see cref="Guid"/> types.</summary>
public class GuidBinaryExpressionLinqTranslator : BinaryExpressionLinqTranslator
{
    /// <inheritdoc/>
    public override Expression? Translate(
        BooleanExpression node,
        Expression left,
        Expression right,
        ILinqVisitorState state
    )
    {
        var leftType = left.Type.UnwrapNullableType();
        var rightType = right.Type.UnwrapNullableType();

        // Early return if we can't translate expressions
        if (leftType != typeof(Guid) && rightType != typeof(Guid))
            return null;

        switch (node)
        {
            // left.CompareTo(right) > 0
            case GreaterThan:
            // left.CompareTo(right) >= 0
            case GreaterThanOrEqual:
            // left.CompareTo(right) < 0
            case LessThan:
            // left.CompareTo(right) <= 0
            case LessThanOrEqual:
                // For comparisons, use 'left.CompareTo(right)', returning an integer (-1, 0, 1)
                left = LinqExpressionUtil.CreateFunctionCall(
                    GuidMethodUtil.CompareTo, state.HandleNullPropagation, left, right
                );
                // Compare with '0'
                right = LinqExpressionUtil.ZeroConstant;
                break;
        }

        return base.Translate(node, left, right, state);
    }
}