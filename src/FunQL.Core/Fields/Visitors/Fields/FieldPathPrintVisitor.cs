// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Visitors.Fields;

/// <summary>Default implementation of <see cref="IFieldPathPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="FieldPathVisitor{TState}"/>
public class FieldPathPrintVisitor<TState> : FieldPathVisitor<TState>, IFieldPathPrintVisitor<TState>
    where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(FieldPath node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var initialField = true;
            foreach (var field in node.Fields)
            {
                // Write field separator (if no brackets required)
                if (!initialField && field is NamedField namedField && !namedField.RequiresBracketNotation())
                    await state.Write(".", ct);

                // Write field
                await Visit(field, state, ct);

                initialField = false;
            }
        }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(NamedField node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(
            node,
            ct => node.RequiresBracketNotation()
                ? state.Write($"[\"{node.Name.Replace("\"", "\\\"")}\"]", ct)
                : state.Write($"{node.Name}", ct),
            cancellationToken
        );

    /// <inheritdoc/>
    public override Task Visit(ListItemField node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => state.Write("$it", ct), cancellationToken);
}