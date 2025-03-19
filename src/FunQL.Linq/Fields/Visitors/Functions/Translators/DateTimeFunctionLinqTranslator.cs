// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Translator for <see cref="DateTime"/> functions.</summary>
public sealed class DateTimeFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        if (source.Type.UnwrapNullableType() != typeof(DateTime))
            return null;

        return node.Name switch
        {
            Year.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Year,
                state.HandleNullPropagation,
                source
            ),
            Month.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Month,
                state.HandleNullPropagation,
                source
            ),
            Day.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Day,
                state.HandleNullPropagation,
                source
            ),
            Hour.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Hour,
                state.HandleNullPropagation,
                source
            ),
            Minute.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Minute,
                state.HandleNullPropagation,
                source
            ),
            Second.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Second,
                state.HandleNullPropagation,
                source
            ),
            Millisecond.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                DateTimeMethodUtil.Millisecond,
                state.HandleNullPropagation,
                source
            ),
            _ => null
        };
    }
}