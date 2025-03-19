// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Visitors;

/// <summary>Default implementation of <see cref="IFilterPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="FilterVisitor{TState}"/>
public class FilterPrintVisitor<TState>(
    IFieldArgumentVisitor<TState> fieldArgumentVisitor,
    IConstantVisitor<TState> constantVisitor
) : FilterVisitor<TState>(fieldArgumentVisitor, constantVisitor),
    IFilterPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Filter node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.Expression, state, ct), cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(And node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(
            node,
            ct => Visit(node.Left, state, ct),
            ct => Visit(node.Right, state, ct),
            cancellationToken
        );

    /// <inheritdoc/>
    public override Task Visit(Or node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(
            node,
            ct => Visit(node.Left, state, ct),
            ct => Visit(node.Right, state, ct),
            cancellationToken
        );

    /// <inheritdoc/>
    public override Task Visit(Not node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.BooleanExpression, state, ct), cancellationToken);

    /// <summary>Visits and prints given <paramref name="node"/>.</summary>
    /// <param name="node">Node to visit.</param>
    /// <param name="fieldPath">Field path of collection that expression applies to.</param>
    /// <param name="itemExpression">Expression for each item in collection.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    private Task VisitCollectionExpression(
        BooleanExpression node,
        FieldPath fieldPath,
        BooleanExpression itemExpression,
        TState state,
        CancellationToken cancellationToken
    ) => state.VisitAndWriteFunction(
        node,
        ct => Visit(fieldPath, state, ct),
        ct => Visit(itemExpression, state, ct),
        cancellationToken
    );

    /// <inheritdoc/>
    public override Task Visit(All node, TState state, CancellationToken cancellationToken) =>
        VisitCollectionExpression(node, node.FieldPath, node.Predicate, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Any node, TState state, CancellationToken cancellationToken) =>
        VisitCollectionExpression(node, node.FieldPath, node.Predicate, state, cancellationToken);

    /// <summary>Visits and prints given <paramref name="node"/>.</summary>
    /// <param name="node">Node to visit.</param>
    /// <param name="fieldArgument">Field path or function expression applies to.</param>
    /// <param name="constant">Constant for the expression.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    private Task VisitBooleanExpression(
        BooleanExpression node,
        FieldArgument fieldArgument,
        Constant constant,
        TState state,
        CancellationToken cancellationToken
    ) => state.VisitAndWriteFunction(
        node,
        ct => Visit(fieldArgument, state, ct),
        ct => Visit(constant, state, ct),
        cancellationToken
    );

    /// <inheritdoc/>
    public override Task Visit(Equal node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(NotEqual node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(GreaterThan node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(GreaterThanOrEqual node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(LessThan node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(LessThanOrEqual node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Has node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(StartsWith node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(EndsWith node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(RegexMatch node, TState state, CancellationToken cancellationToken) =>
        VisitBooleanExpression(node, node.FieldArgument, node.Constant, state, cancellationToken);
}