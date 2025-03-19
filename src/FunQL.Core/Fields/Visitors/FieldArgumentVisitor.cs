// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Functions;

namespace FunQL.Core.Fields.Visitors;

/// <summary>Default implementation of <see cref="IFieldArgumentVisitor{TState}"/>.</summary>
/// <param name="fieldFunctionVisitor">
/// Visitor to delegate <see cref="IFieldFunctionVisitor{TState}"/> implementation to.
/// </param>
/// <inheritdoc/>
public class FieldArgumentVisitor<TState>(
    IFieldFunctionVisitor<TState> fieldFunctionVisitor
) : IFieldArgumentVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IFieldFunctionVisitor{TState}"/> implementation to.</summary>
    private readonly IFieldFunctionVisitor<TState> _fieldFunctionVisitor = fieldFunctionVisitor;

    /// <inheritdoc/>
    public Task Visit(FieldArgument node, TState state, CancellationToken cancellationToken) => node switch
    {
        FieldPath fieldPath => Visit(fieldPath, state, cancellationToken),
        FieldFunction fieldFunction => Visit(fieldFunction, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <inheritdoc/>
    public Task Visit(FieldPath node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Field node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(NamedField node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(ListItemField node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(FieldFunction node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Year node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Month node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Day node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Hour node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Minute node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Second node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Millisecond node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Floor node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Ceiling node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Round node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Lower node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(Upper node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);

    /// <inheritdoc/>
    public Task Visit(IsNull node, TState state, CancellationToken cancellationToken) =>
        _fieldFunctionVisitor.Visit(node, state, cancellationToken);
}