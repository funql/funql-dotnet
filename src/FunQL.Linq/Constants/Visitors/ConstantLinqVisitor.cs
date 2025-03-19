// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Constants.Visitors;

/// <summary>Default implementation of <see cref="IConstantLinqVisitor{ILinqVisitorState}"/>.</summary>
public class ConstantLinqVisitor : ConstantVisitor<ILinqVisitorState>, IConstantLinqVisitor<ILinqVisitorState>
{
    /// <inheritdoc/>
    public override Task Visit(Constant node, ILinqVisitorState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, () => { state.Result = Expression.Constant(node.Value); }, cancellationToken);
}