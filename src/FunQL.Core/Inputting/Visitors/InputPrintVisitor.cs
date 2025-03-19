// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Visitors;

/// <summary>Default implementation of <see cref="IInputPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="InputVisitor{TState}"/>
public class InputPrintVisitor<TState>(
    IConstantVisitor<TState> constantVisitor
) : InputVisitor<TState>(constantVisitor), IInputPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Input node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(node, ct => Visit(node.Constant, state, ct), cancellationToken);
}