// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Visitors;

/// <summary>Default implementation of <see cref="IFilterVisitor{TState}"/>.</summary>
/// <param name="fieldArgumentVisitor">
/// Visitor to delegate <see cref="IFieldArgumentVisitor{TState}"/> implementation to.
/// </param>
/// <param name="constantVisitor">
/// Visitor to delegate <see cref="IConstantVisitor{TState}"/> implementation to.
/// </param>
/// <inheritdoc/>
public class FilterVisitor<TState>(
    IFieldArgumentVisitor<TState> fieldArgumentVisitor,
    IConstantVisitor<TState> constantVisitor
) : IFilterVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IFieldArgumentVisitor{TState}"/> implementation to.</summary>
    private readonly IFieldArgumentVisitor<TState> _fieldArgumentVisitor = fieldArgumentVisitor;

    /// <summary>Visitor to delegate <see cref="IConstantVisitor{TState}"/> implementation to.</summary>
    private readonly IConstantVisitor<TState> _constantVisitor = constantVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(Filter node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.Expression, state, ct), cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(BooleanExpression node, TState state, CancellationToken cancellationToken) => node switch
    {
        And and => Visit(and, state, cancellationToken),
        Or or => Visit(or, state, cancellationToken),
        Not not => Visit(not, state, cancellationToken),
        All all => Visit(all, state, cancellationToken),
        Any any => Visit(any, state, cancellationToken),
        Equal equal => Visit(equal, state, cancellationToken),
        NotEqual notEqual => Visit(notEqual, state, cancellationToken),
        GreaterThan greaterThan => Visit(greaterThan, state, cancellationToken),
        GreaterThanOrEqual greaterThanOrEqual => Visit(greaterThanOrEqual, state, cancellationToken),
        LessThan lessThan => Visit(lessThan, state, cancellationToken),
        LessThanOrEqual lessThanOrEqual => Visit(lessThanOrEqual, state, cancellationToken),
        Has has => Visit(has, state, cancellationToken),
        StartsWith startsWith => Visit(startsWith, state, cancellationToken),
        EndsWith endsWith => Visit(endsWith, state, cancellationToken),
        RegexMatch regexMatch => Visit(regexMatch, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <inheritdoc/>
    public virtual Task Visit(And node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.Left, state, ct);
            await Visit(node.Right, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Or node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.Left, state, ct);
            await Visit(node.Right, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Not node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.BooleanExpression, state, ct), cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(All node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldPath, state, ct);
            await Visit(node.Predicate, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Any node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldPath, state, ct);
            await Visit(node.Predicate, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Equal node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(NotEqual node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(GreaterThan node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(GreaterThanOrEqual node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(LessThan node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(LessThanOrEqual node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Has node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(StartsWith node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(EndsWith node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(RegexMatch node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldArgument, state, ct);
            await Visit(node.Constant, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(FieldPath node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Field node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(NamedField node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(ListItemField node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(FieldFunction node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Year node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Month node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Day node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Hour node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Minute node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Second node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Millisecond node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Floor node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Ceiling node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Round node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Lower node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Upper node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(IsNull node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(FieldArgument node, TState state, CancellationToken cancellationToken) =>
        _fieldArgumentVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Constant node, TState state, CancellationToken cancellationToken) =>
        _constantVisitor.Visit(node, state, cancellationToken);
}