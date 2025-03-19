// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Fields;

namespace FunQL.Core.Fields.Visitors.Functions;

/// <summary>Default implementation of <see cref="IFieldFunctionVisitor{TState}"/>.</summary>
/// <param name="fieldPathVisitor">
/// Visitor to delegate <see cref="IFieldPathVisitor{TState}"/> implementation to.
/// </param>
/// <inheritdoc/>
public class FieldFunctionVisitor<TState>(
    IFieldPathVisitor<TState> fieldPathVisitor
) : IFieldFunctionVisitor<TState>
    where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IFieldPathVisitor{TState}"/> implementation to.</summary>
    private readonly IFieldPathVisitor<TState> _fieldPathVisitor = fieldPathVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(FieldFunction node, TState state, CancellationToken cancellationToken) => node switch
    {
        Year year => Visit(year, state, cancellationToken),
        Month month => Visit(month, state, cancellationToken),
        Day day => Visit(day, state, cancellationToken),
        Hour hour => Visit(hour, state, cancellationToken),
        Minute minute => Visit(minute, state, cancellationToken),
        Second second => Visit(second, state, cancellationToken),
        Millisecond millisecond => Visit(millisecond, state, cancellationToken),
        Floor floor => Visit(floor, state, cancellationToken),
        Ceiling ceiling => Visit(ceiling, state, cancellationToken),
        Round round => Visit(round, state, cancellationToken),
        Lower lower => Visit(lower, state, cancellationToken),
        Upper upper => Visit(upper, state, cancellationToken),
        IsNull isNull => Visit(isNull, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <summary>Visits the <see cref="FieldFunction"/> and its <see cref="FieldFunction.FieldPath"/>.</summary>
    private Task VisitFunction(FieldFunction node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.FieldPath, state, ct), cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Year node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Month node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Day node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Hour node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Minute node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Second node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Millisecond node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Floor node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Ceiling node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Round node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Lower node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Upper node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(IsNull node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(FieldPath node, TState state, CancellationToken cancellationToken) =>
        _fieldPathVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Field node, TState state, CancellationToken cancellationToken) =>
        _fieldPathVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(NamedField node, TState state, CancellationToken cancellationToken) =>
        _fieldPathVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(ListItemField node, TState state, CancellationToken cancellationToken) =>
        _fieldPathVisitor.Visit(node, state, cancellationToken);
}