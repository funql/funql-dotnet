// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using System.Reflection;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using FunQL.Linq.Utils;
using NodaTime;

namespace WebApi.FunQL.NodaTime;

/// <summary>Translator for <see cref="Instant"/> functions.</summary>
/// <remarks>
/// Translates <see cref="Instant"/> to <see cref="DateTime"/> and then delegates the translation logic to
/// <see cref="DateTimeFunctionLinqTranslator"/>, so e.g. <see cref="Year"/> and <see cref="Month"/> field functions can
/// be used on <see cref="Instant"/> types.
/// </remarks>
public class InstantFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <summary>Empty <see cref="Instant"/> we can use to get <see cref="MethodInfo"/>.</summary>
    private static readonly Instant DefaultInstant;
    /// <summary>The <see cref="MethodInfo"/> for <see cref="Instant.ToDateTimeUtc"/>.</summary>
    private static readonly MethodInfo InstantToDateTimeUtcMethod =
        MethodInfoUtil.MethodOf(DefaultInstant.ToDateTimeUtc);

    /// <summary>The <see cref="DateTime"/> translator to delegate translation logic to.</summary>
    private static readonly DateTimeFunctionLinqTranslator DateTimeFunctionLinqTranslator = new();
    
    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        if (source.Type.UnwrapNullableType() != typeof(Instant))
            return null;

        // Translate Instant to DateTime so we can use DateTime methods instead
        source = LinqExpressionUtil.CreateFunctionCall(
            InstantToDateTimeUtcMethod,
            state.HandleNullPropagation,
            source
        );

        return DateTimeFunctionLinqTranslator.Translate(node, source, state);
    }
}