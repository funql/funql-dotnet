// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Visitors;

/// <summary>Default implementation of <see cref="ILimitPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="LimitVisitor{TState}"/>
public class LimitPrintVisitor<TState>(
    IConstantVisitor<TState> constantVisitor
) : LimitVisitor<TState>(constantVisitor), ILimitPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Limit node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.Constant, state, ct), cancellationToken);
}