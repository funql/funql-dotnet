// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>Translator for <see cref="string"/> types.</summary>
public class StringBinaryExpressionLinqTranslator : BinaryExpressionLinqTranslator
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
        if (leftType != typeof(string) && rightType != typeof(string))
            return null;

        // Convert nulls of type 'object' to nulls of type 'string' to make the string.Compare call work
        left = left.EnsureTypedNullConstant(typeof(string));
        right = right.EnsureTypedNullConstant(typeof(string));

        var handleNullPropagation = state.HandleNullPropagation;

        switch (node)
        {
            // string.Compare(left, right) > 0
            case GreaterThan:
            // string.Compare(left, right) >= 0
            case GreaterThanOrEqual:
            // string.Compare(left, right) < 0
            case LessThan:
            // string.Compare(left, right) <= 0
            case LessThanOrEqual:
                // For comparisons, use 'string.Compare(left, right)', returning an integer (-1, 0, 1)
                left = LinqExpressionUtil.CreateFunctionCall(
                    StringMethodUtil.StringCompare, handleNullPropagation, left, right
                );
                // Compare with '0'
                right = LinqExpressionUtil.ZeroConstant;
                break;
        }

        return node switch
        {
            Has =>
                LinqExpressionUtil.CreateFunctionCall(StringMethodUtil.Contains, handleNullPropagation, left, right),
            StartsWith =>
                LinqExpressionUtil.CreateFunctionCall(StringMethodUtil.StartsWith, handleNullPropagation, left, right),
            EndsWith =>
                LinqExpressionUtil.CreateFunctionCall(StringMethodUtil.EndsWith, handleNullPropagation, left, right),
            RegexMatch =>
                LinqExpressionUtil.CreateFunctionCall(
                    StringMethodUtil.RegexIsMatch, handleNullPropagation, left, right
                ),
            _ => base.Translate(node, left, right, state)
        };
    }
}