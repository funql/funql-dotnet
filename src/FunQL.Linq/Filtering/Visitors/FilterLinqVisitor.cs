// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using FunQL.Core.Common.Extensions;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Visitors;
using FunQL.Linq.Common.Exceptions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Common.Visitors.Extensions;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Filtering.Visitors.Translators;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Filtering.Visitors;

/// <summary>Default implementation of <see cref="IFilterLinqVisitor{ILinqVisitorState}"/>.</summary>
/// <param name="translators">List of translators to translate binary expressions.</param>
/// <param name="fieldArgumentVisitor">
/// Visitor to delegate <see cref="IFieldArgumentVisitor{TState}"/> implementation to.
/// </param>
/// <param name="constantVisitor">
/// Visitor to delegate <see cref="IConstantVisitor{TState}"/> implementation to.
/// </param>
public class FilterLinqVisitor(
    IEnumerable<IBinaryExpressionLinqTranslator> translators,
    IFieldArgumentVisitor<ILinqVisitorState> fieldArgumentVisitor,
    IConstantVisitor<ILinqVisitorState> constantVisitor
) : FilterVisitor<ILinqVisitorState>(fieldArgumentVisitor, constantVisitor), IFilterLinqVisitor<ILinqVisitorState>
{
    /// <summary>List of translators to translate binary expressions.</summary>
    private readonly IEnumerable<IBinaryExpressionLinqTranslator> _translators = translators
        .OrderBy(it => it.Order)
        .ToList();

    /// <summary>
    /// Translates given <paramref name="node"/> using configured list of <see cref="IBinaryExpressionLinqTranslator"/>.
    /// </summary>
    /// <exception cref="LinqException">If none of the translators could translate the expression.</exception>
    private void TranslateBinary(
        BooleanExpression node,
        Expression left,
        Expression right,
        ILinqVisitorState state
    )
    {
        var result = _translators
            .Select(it => it.Translate(node, left, right, state))
            .FirstOrDefault(it => it != null);

        if (result == null)
            throw new LinqException($"Failed to translate filter function '{node.Name}' to LINQ.");

        state.Result = result;
    }

    /// <inheritdoc/>
    public override Task Visit(Filter node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var parameter = state.RequireSource<ParameterExpression>();
            await Visit(node.Expression, state, ct);
            var expression = state.RequireResult();
            state.Result = Expression.Lambda(expression, parameter);
        }, cancellationToken);

    /// <summary>Translates an <see cref="And"/> or <see cref="Or"/> node.</summary>
    private Task TranslateAndOr(
        BooleanExpression node,
        BooleanExpression leftNode,
        BooleanExpression rightNode,
        ILinqVisitorState state,
        CancellationToken cancellationToken
    ) => state.OnVisit(node, async ct =>
    {
        await Visit(leftNode, state, ct);
        var left = state.RequireResult();
        await Visit(rightNode, state, ct);
        var right = state.RequireResult();

        TranslateBinary(node, left, right, state);
    }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(And node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateAndOr(node, node.Left, node.Right, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Or node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateAndOr(node, node.Left, node.Right, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Not node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.BooleanExpression, state, ct);
            var expression = state.RequireResult();
            if (expression.Type.UnwrapNullableType() != typeof(bool))
                throw new InvalidOperationException($"'Not' node does not support type {expression.Type}");

            state.Result = Expression.Not(expression);
        }, cancellationToken);

    /// <summary>Translates an <see cref="Any"/> or <see cref="All"/> node.</summary>
    private Task TranslateAnyAll(
        BooleanExpression node,
        FieldPath fieldPath,
        BooleanExpression predicate,
        MethodInfo methodInfo,
        ILinqVisitorState state,
        CancellationToken cancellationToken
    ) => state.OnVisit(node, async ct =>
    {
        await Visit(fieldPath, state, ct);
        var source = state.RequireResult();

        // Create lambda parameter (representing each element of source collection) that body should use
        if (!source.Type.IsCollectionType(out var elementType))
            throw new InvalidOperationException("Source must be a collection");
        var lambdaParameter = Expression.Parameter(elementType, "it");

        state.EnterContext(new LambdaVisitContext(lambdaParameter));
        await Visit(predicate, state, ct);
        state.ExitContext();
        var body = state.RequireResult();

        state.Result = LinqExpressionUtil.CreateFunctionCall(
            methodInfo.MakeGenericMethod(elementType),
            state.HandleNullPropagation,
            source,
            Expression.Lambda(body, lambdaParameter)
        );
    }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(All node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateAnyAll(node, node.FieldPath, node.Predicate, EnumerableMethodUtil.All, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Any node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateAnyAll(node, node.FieldPath, node.Predicate, EnumerableMethodUtil.Any, state, cancellationToken);

    /// <summary>
    /// Translates a binary <paramref name="node"/> for given <paramref name="fieldArgument"/> and
    /// <paramref name="constant"/> using configured list of <see cref="IBinaryExpressionLinqTranslator"/>.
    /// </summary>
    /// <param name="node">Node to translate.</param>
    /// <param name="fieldArgument">Field argument of comparison to translate.</param>
    /// <param name="constant">Constant of comparison to translate.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>Task that handles visit.</returns>
    /// <exception cref="LinqException">If node could not be translated.</exception>
    protected Task TranslateBinary(
        BooleanExpression node,
        FieldArgument fieldArgument,
        Constant constant,
        ILinqVisitorState state,
        CancellationToken cancellationToken
    ) => state.OnVisit(node, async ct =>
    {
        await Visit(fieldArgument, state, ct);
        var left = state.RequireResult();
        await Visit(constant, state, ct);
        var right = state.RequireResult();

        TranslateBinary(node, left, right, state);
    }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Equal node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(NotEqual node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(GreaterThan node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(GreaterThanOrEqual node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(LessThan node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(LessThanOrEqual node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Has node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(StartsWith node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(EndsWith node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(RegexMatch node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        TranslateBinary(node, node.FieldArgument, node.Constant, state, cancellationToken);
}