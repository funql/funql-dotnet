// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Visitors;

/// <summary>Default implementation of <see cref="ISkipPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="SkipVisitor{TState}"/>
public class SkipPrintVisitor<TState>(
    IConstantVisitor<TState> constantVisitor
) : SkipVisitor<TState>(constantVisitor), ISkipPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Skip node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.Constant, state, ct), cancellationToken);
}