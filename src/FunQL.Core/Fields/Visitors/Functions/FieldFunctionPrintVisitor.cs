// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Fields;

namespace FunQL.Core.Fields.Visitors.Functions;

/// <summary>Default implementation of <see cref="IFieldFunctionPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="FieldFunctionVisitor{TState}"/>
public class FieldFunctionPrintVisitor<TState>(
    IFieldPathVisitor<TState> fieldPathVisitor
) : FieldFunctionVisitor<TState>(fieldPathVisitor), IFieldFunctionPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <summary>Visits and prints given <paramref name="node"/>.</summary>
    /// <param name="node">Node to visit.</param>
    /// <param name="state">State of the visitor.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    private Task VisitFunction(
        FieldFunction node,
        TState state,
        CancellationToken cancellationToken
    ) => state.VisitAndWriteFunction(node, ct => Visit(node.FieldPath, state, ct), cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Year node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Month node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Day node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Hour node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Minute node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Second node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Millisecond node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Floor node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Ceiling node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Round node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Lower node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Upper node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(IsNull node, TState state, CancellationToken cancellationToken) =>
        VisitFunction(node, state, cancellationToken);
}