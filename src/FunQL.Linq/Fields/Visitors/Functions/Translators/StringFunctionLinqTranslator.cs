// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Translator for <see cref="string"/> functions.</summary>
public sealed class StringFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        if (source.Type != typeof(string))
            return null;

        return node.Name switch
        {
            Lower.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                StringMethodUtil.ToLower,
                state.HandleNullPropagation,
                source
            ),
            Upper.FunctionName => LinqExpressionUtil.CreateFunctionCall(
                StringMethodUtil.ToUpper,
                state.HandleNullPropagation,
                source
            ),
            _ => null
        };
    }
}