// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Visitors;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Common.Visitors.Extensions;

namespace FunQL.Linq.Sorting.Visitors;

/// <summary>Default implementation of <see cref="ISortLinqVisitor{ILinqVisitorState}"/>.</summary>
/// <inheritdoc cref="SortVisitor{ILinqVisitorState}"/>
public class SortLinqVisitor(
    IFieldArgumentVisitor<ILinqVisitorState> fieldArgumentVisitor
) : SortVisitor<ISortLinqVisitorState>(fieldArgumentVisitor), ISortLinqVisitor<ISortLinqVisitorState>
{
    /// <inheritdoc/>
    public override Task Visit(Sort node, ISortLinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var result = new List<SortLinqExpression>();
            foreach (var expression in node.Expressions)
            {
                await Visit(expression, state, ct);
                var linqExpression = state.RequireResult<LambdaExpression>();
                var direction = state.SortDirection
                    ?? throw new InvalidOperationException("SortDirection is required");
                result.Add(new SortLinqExpression(linqExpression, direction));
            }

            state.SortExpressions = result;
        }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Ascending node, ISortLinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var parameter = (ParameterExpression)state.Source;
            await Visit(node.FieldArgument, state, ct);
            var expression = state.RequireResult();
            state.Result = Expression.Lambda(expression, parameter);
            state.SortDirection = SortDirection.Ascending;
        }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Descending node, ISortLinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var parameter = (ParameterExpression)state.Source;
            await Visit(node.FieldArgument, state, ct);
            var expression = state.RequireResult();
            state.Result = Expression.Lambda(expression, parameter);
            state.SortDirection = SortDirection.Descending;
        }, cancellationToken);
}