// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Fields.Visitors.Fields;

/// <summary>Default implementation of <see cref="IFieldPathLinqVisitor{ILinqVisitorState}"/>.</summary>
/// <inheritdoc cref="FieldPathVisitor{ILinqVisitorState}"/>
/// <remarks>
/// This implementation only translates the full <see cref="FieldPath"/> and does not translate each individual
/// <see cref="Field"/>. Individual <see cref="Field"/>s will not be visited upon visiting <see cref="FieldPath"/>.
/// </remarks>
public class FieldPathLinqVisitor : FieldPathVisitor<ILinqVisitorState>, IFieldPathLinqVisitor<ILinqVisitorState>
{
    /// <inheritdoc/>
    public override Task Visit(FieldPath node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, () =>
        {
            var objectTypeConfig = state.RequireContext<FieldPathLinqVisitContext>().ObjectTypeConfig;
            var source = state.Source;
            state.Result = GetMemberExpression(objectTypeConfig, source, node, state);
        }, cancellationToken);

    /// <summary>
    /// Gets the <see cref="Expression"/> that accesses the field of given <paramref name="source"/> representing given
    /// <paramref name="fieldPath"/> based on given <paramref name="sourceConfig"/>.
    /// </summary>
    private static Expression GetMemberExpression(
        IObjectTypeConfig sourceConfig,
        Expression source,
        FieldPath fieldPath,
        ILinqVisitorState state
    )
    {
        var lambdaContext = state.FindContext<LambdaVisitContext>();
        var memberExpression = source;

        foreach (var (field, fieldConfig, typeConfig) in fieldPath.ResolveConfigs(sourceConfig))
        {
            // If fieldConfig defined, Expression should access the CLR member for current field
            if (fieldConfig != null)
            {
                if (fieldConfig.MemberInfo == null)
                    throw new InvalidOperationException($"No MemberInfo configured for field {field}");

                memberExpression = LinqExpressionUtil.CreateFunctionCall(
                    fieldConfig.MemberInfo,
                    state.HandleNullPropagation,
                    memberExpression
                );
                continue;
            }

            // If fieldConfig not defined, but typeConfig is, this is a list item, so Expression should be the parameter
            // for the lambda expression
            if (typeConfig != null)
            {
                memberExpression = lambdaContext?.LambdaParameter ?? Expression.Parameter(typeConfig.Type, "it");
                continue;
            }

            // No config to create Expression for, so throw
            throw new InvalidOperationException($"No config found for field '{field}'");
        }

        return memberExpression;
    }
}