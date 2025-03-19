// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Linq.Common.Exceptions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Common.Visitors.Extensions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;

namespace FunQL.Linq.Fields.Visitors.Functions;

/// <summary>Default implementation of <see cref="IFieldFunctionLinqVisitor{ILinqVisitorState}"/>.</summary>
/// <param name="translators">List of translators to translate field functions.</param>
/// <param name="fieldPathVisitor"><inheritdoc cref="FieldFunctionVisitor{ILinqVisitorState}"/></param>
public class FieldFunctionLinqVisitor(
    IEnumerable<IFieldFunctionLinqTranslator> translators,
    IFieldPathVisitor<ILinqVisitorState> fieldPathVisitor
) : FieldFunctionVisitor<ILinqVisitorState>(fieldPathVisitor), IFieldFunctionLinqVisitor<ILinqVisitorState>
{
    /// <summary>List of translators to translate field functions.</summary>
    private readonly IEnumerable<IFieldFunctionLinqTranslator> _translators = translators
        .OrderBy(it => it.Order)
        .ToList();

    /// <summary>
    /// Translates <paramref name="node"/> using <see cref="_translators"/> or throws if <paramref name="node"/> could
    /// not be translated.
    /// </summary>
    /// <param name="node">Node to translate.</param>
    /// <param name="state">Current visitor state.</param>
    /// <param name="cancellationToken">Token to cancel async requests.</param>
    /// <returns>Task to await translation.</returns>
    /// <exception cref="LinqException">If <paramref name="node"/> could not be translated.</exception>
    protected Task Translate(FieldFunction node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            await Visit(node.FieldPath, state, ct);
            var source = state.RequireResult();

            var result = _translators
                .Select(it => it.Translate(node, source, state))
                .FirstOrDefault(it => it != null);

            if (result == null)
                throw new LinqException($"Failed to translate field function '{node.Name}' to LINQ.");

            state.Result = result;
        }, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Year node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Month node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Day node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Hour node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Minute node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Second node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Millisecond node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Floor node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Ceiling node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Round node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Lower node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(Upper node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);

    /// <inheritdoc/>
    public override Task Visit(IsNull node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        Translate(node, state, cancellationToken);
}