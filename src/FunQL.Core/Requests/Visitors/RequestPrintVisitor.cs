// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Default implementation of <see cref="IRequestPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="RequestVisitor{TState}"/>
public class RequestPrintVisitor<TState>(
    IParameterVisitor<TState> parameterVisitor
) : RequestVisitor<TState>(parameterVisitor), IRequestPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <inheritdoc/>
    public override Task Visit(Request node, TState state, CancellationToken cancellationToken) =>
        state.VisitAndWriteFunction(
            node,
            node.Parameters.Select<Parameter, Func<CancellationToken, Task>>(parameter =>
                ct => Visit(parameter, state, ct)
            ),
            cancellationToken
        );
}