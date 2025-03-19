// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Visitors;

/// <summary>Default implementation of <see cref="ISortPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="SortVisitor{TState}"/>
public class SortPrintVisitor<TState>(
    IFieldArgumentVisitor<TState> fieldArgumentVisitor
) : SortVisitor<TState>(fieldArgumentVisitor), ISortPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Sort node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(
            node,
            node.Expressions.Select<SortExpression, Func<CancellationToken, Task>>(expression =>
                ct => Visit(expression, state, ct)
            ),
            cancellationToken
        );

    /// <summary>Visits and prints given <paramref name="node"/>.</summary>
    /// <param name="node">Node to visit.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    private Task VisitExpression(SortExpression node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.FieldArgument, state, ct), cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Ascending node, TState state, CancellationToken cancellationToken) =>
        VisitExpression(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Descending node, TState state, CancellationToken cancellationToken) =>
        VisitExpression(node, state, cancellationToken);
}