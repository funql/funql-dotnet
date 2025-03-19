// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Extensions;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Executors.Validate;

/// <summary>
/// Execution handler that validates the <see cref="IExecutorState.Request"/> using the
/// <see cref="IValidateConfigExtension"/> configured for <see cref="ISchemaConfig"/>.
/// </summary>
/// <remarks>
/// Requires <see cref="SchemaConfigExecuteContext"/> context and <see cref="IValidateConfigExtension"/> to be
/// configured.
/// </remarks>
public sealed class ValidateRequestExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "FunQL.Core.ValidateRequestExecutionHandler";
    
    /// <summary>Default order of this handler.</summary>
    /// <remarks>Should be called after parsing and before execution, so somewhere in the middle.</remarks>
    public const int DefaultOrder = 0;

    /// <inheritdoc/>
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        // Get arguments
        var request = state.Request
            ?? throw new InvalidOperationException();
        var schemaConfig = state.RequireContext<SchemaConfigExecuteContext>().SchemaConfig;

        await schemaConfig.ValidateRequest(request, cancellationToken);

        await next(state, cancellationToken);
    }
}