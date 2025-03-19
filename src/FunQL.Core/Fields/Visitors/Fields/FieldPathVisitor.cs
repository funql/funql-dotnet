// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Visitors.Fields;

/// <summary>Default implementation of <see cref="IFieldPathVisitor{TState}"/>.</summary>
/// <inheritdoc/>
public class FieldPathVisitor<TState> : IFieldPathVisitor<TState> where TState : IVisitorState
{
    /// <inheritdoc/>
    public virtual Task Visit(FieldPath node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            foreach (var field in node.Fields)
                await Visit(field, state, ct);
        }, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Field node, TState state, CancellationToken cancellationToken) => node switch
    {
        NamedField namedField => Visit(namedField, state, cancellationToken),
        ListItemField listItemField => Visit(listItemField, state, cancellationToken),
        _ => throw new ArgumentOutOfRangeException(nameof(node))
    };

    /// <inheritdoc/>
    public virtual Task Visit(NamedField node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(ListItemField node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, cancellationToken);
}