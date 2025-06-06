﻿// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Visitors;

/// <summary>Default implementation of <see cref="IInputVisitor{TState}"/>.</summary>
/// <param name="constantVisitor">Visitor to delegate <see cref="IConstantVisitor{TState}"/> implementation to.</param>
/// <inheritdoc/>
public class InputVisitor<TState>(
    IConstantVisitor<TState> constantVisitor
) : IInputVisitor<TState> where TState : IVisitorState
{
    /// <summary>Visitor to delegate <see cref="IConstantVisitor{TState}"/> implementation to.</summary>
    private readonly IConstantVisitor<TState> _constantVisitor = constantVisitor;

    /// <inheritdoc/>
    public virtual Task Visit(Input node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, ct => Visit(node.Constant, state, ct), cancellationToken);

    /// <inheritdoc/>
    public virtual Task Visit(Constant node, TState state, CancellationToken cancellationToken) =>
        _constantVisitor.Visit(node, state, cancellationToken);
}