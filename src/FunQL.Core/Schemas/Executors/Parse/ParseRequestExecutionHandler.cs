// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Schemas.Configs.Parse.Extensions;

namespace FunQL.Core.Schemas.Executors.Parse;

/// <summary>
/// Execution handler that parses the <see cref="IExecutorState.Request"/> if not already done so and
/// <see cref="ParseRequestExecuteContext"/> is found.
/// </summary>
/// <remarks>Requires <see cref="SchemaConfigExecuteContext"/> context.</remarks>
public sealed class ParseRequestExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "FunQL.Core.ParseRequestExecutionHandler";
    
    /// <summary>Default order of this handler.</summary>
    /// <remarks>
    /// Should be called early in the pipeline as parsing is the first step. Adding <c>1_000_000</c> to allow for other
    /// handlers to use an even lower order so they can run before this handler.
    /// </remarks>
    public const int DefaultOrder = int.MinValue + 1_000_000;

    /// <inheritdoc/>
    public Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        // Early return if Request already parsed
        if (state.Request != null)
            return next(state, cancellationToken);

        var context = state.FindContext<ParseRequestExecuteContext>();
        // Early return if parse context is missing
        if (context == null)
            return next(state, cancellationToken);

        var schemaConfig = state.RequireContext<SchemaConfigExecuteContext>().SchemaConfig;

        state.Request = schemaConfig.ParseRequest(context.Request);

        return next(state, cancellationToken);
    }
}