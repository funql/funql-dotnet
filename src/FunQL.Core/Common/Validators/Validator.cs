// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Requests.Visitors;

namespace FunQL.Core.Common.Validators;

/// <summary>
/// Default implementation of <see cref="IValidator"/> using a <see cref="IRequestVisitor{TState}"/> to validate all
/// nodes in the node tree.
/// </summary>
/// <param name="visitor">Visitor used for walking the node tree.</param>
public class Validator(IRequestVisitor<IVisitorState> visitor) : IValidator
{
    /// <summary>Visitor used for walking the node tree.</summary>
    private readonly IRequestVisitor<IVisitorState> _visitor = visitor;

    /// <inheritdoc/>
    public Task Validate(Request node, IValidatorState state, CancellationToken cancellationToken) => _visitor.Visit(
        node,
        new VisitorState(
            (queryNode, _, ct) => state.ValidateOnEnter(queryNode, ct),
            (queryNode, _, ct) => state.ValidateOnExit(queryNode, ct)
        ),
        cancellationToken
    );
}