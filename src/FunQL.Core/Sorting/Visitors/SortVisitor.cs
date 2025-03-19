// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Visitors;

/// <summary>Default implementation of <see cref="ISortVisitor{TState}"/>.</summary>
/// <param name="fieldArgumentVisitor">
/// Visitor to delegate <see cref="IFieldArgumentVisitor{TState}"/> implementation to.
/// </param>
/// <inheritdoc/>
public class SortVisitor<TState>(
    IFieldArgumentVisitor<TState> fieldArgumentVisitor
) : ISortVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IFieldArgumentVisitor{TState}"/> implementation to.</summary>
    private readonly IFieldArgumentVisitor<TState> _fieldArgumentVisitor = fieldArgumentVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(Sort node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            foreach (var expression in node.Expressions)
                await Visit(expression, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(SortExpression node, TState state, CancellationToken cancellationToken) => node switch
    {
        Ascending ascending => Visit(ascending, state, cancellationToken),
        Descending descending => Visit(descending, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <inheritdoc/>
    public virtual Task Visit(Ascending node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.FieldArgument, state, ct), cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Descending node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.FieldArgument, state, ct), cancellationToken);

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
}