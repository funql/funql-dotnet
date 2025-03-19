// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Translator for <see cref="decimal"/> functions.</summary>
public sealed class DecimalFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        if (source.Type.UnwrapNullableType() != typeof(decimal))
            return null;

        return node.Name switch
        {
            Floor.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DecimalMethodUtil.Floor,
                state.HandleNullPropagation,
                source
            ),
            Ceiling.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DecimalMethodUtil.Ceiling,
                state.HandleNullPropagation,
                source
            ),
            Round.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DecimalMethodUtil.Round,
                state.HandleNullPropagation,
                source
            ),
            _ => null
        };
    }
}