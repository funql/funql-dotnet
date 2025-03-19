// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Fields.Visitors.Functions.Translators;

/// <summary>Translator for <see cref="object"/> functions.</summary>
public sealed class ObjectFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <inheritdoc/>
    /// <remarks>Uses <see cref="int.MaxValue"/> to assure translator is called after any other translators.</remarks>
    public override int Order => int.MaxValue;

    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        return node.Name switch
        {
            IsNull.FunctionName => Expression.Equal(
                source.EnsureNullableType(),
                Expression.Constant(null)
            ),
            _ => null
        };
    }
}