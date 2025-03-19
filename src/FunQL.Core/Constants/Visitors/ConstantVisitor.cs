// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Visitors;

/// <summary>Default implementation of <see cref="IConstantVisitor{TState}"/>.</summary>
/// <inheritdoc/>
public class ConstantVisitor<TState> : IConstantVisitor<TState> where TState : IVisitorState
{
    /// <inheritdoc/>
    public virtual Task Visit(Constant node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, cancellationToken);
}