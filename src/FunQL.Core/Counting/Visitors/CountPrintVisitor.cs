// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Counting.Nodes;

namespace FunQL.Core.Counting.Visitors;

/// <summary>Default implementation of <see cref="ICountPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="CountVisitor{TState}"/>
public class CountPrintVisitor<TState>(
    IConstantVisitor<TState> constantVisitor
) : CountVisitor<TState>(constantVisitor), ICountPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Count node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.Constant, state, ct), cancellationToken);
}