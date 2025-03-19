// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Filtering.Visitors.Translators;

/// <summary>Translator for enum types, ensuring left and right enum values can be compared.</summary>
public sealed class EnumBinaryExpressionLinqTranslator : BinaryExpressionLinqTranslator
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
        if (!leftType.IsEnum && !rightType.IsEnum)
            return null;

        var enumType = leftType.IsEnum ? leftType : rightType;
        var enumUnderlyingType = Enum.GetUnderlyingType(enumType);
        left = left.ConvertToType(enumUnderlyingType);
        right = right.ConvertToType(enumUnderlyingType);

        return base.Translate(node, left, right, state);
    }
}