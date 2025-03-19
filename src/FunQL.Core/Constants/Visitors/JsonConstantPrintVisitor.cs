// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Visitors;
using FunQL.Core.Common.Visitors.Extensions;
using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Visitors;

/// <summary>Implementation of <see cref="IConstantPrintVisitor{TState}"/> using <see cref="JsonSerializer"/>.</summary>
/// <param name="jsonSerializerOptions">Options to use when writing JSON.</param>
/// <inheritdoc cref="IConstantPrintVisitor{TState}"/>
public class JsonConstantPrintVisitor<TState>(
    JsonSerializerOptions jsonSerializerOptions
) : ConstantVisitor<TState>, IConstantPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <summary>Options to use when writing JSON.</summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

    /// <inheritdoc/>
    public override Task Visit(Constant node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var jsonValue = JsonSerializer.Serialize(node.Value, _jsonSerializerOptions);
            await state.Write(jsonValue, ct);
        }, cancellationToken);
}