// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Common.Executors;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute;

/// <summary>Immutable implementation of <see cref="IExecuteConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="ExecutorProvider">The <see cref="IExecuteConfigExtension.ExecutorProvider"/>.</param>
/// <param name="ExecutorStateFactory">The <see cref="IExecuteConfigExtension.ExecutorStateFactory"/>.</param>
/// <param name="ExecutionSteps">The <see cref="IExecuteConfigExtension.GetExecutionSteps"/>.</param>
public sealed record ImmutableExecuteConfigExtension(
    string Name,
    Func<ISchemaConfig, IExecutor> ExecutorProvider,
    Func<ISchemaConfig, IExecutorState> ExecutorStateFactory,
    IImmutableDictionary<string, ExecutionStep> ExecutionSteps
) : ImmutableConfigExtension(Name), IExecuteConfigExtension
{
    /// <inheritdoc/>
    public IEnumerable<ExecutionStep> GetExecutionSteps() => ExecutionSteps.Values;

    /// <inheritdoc/>
    public ExecutionStep? FindExecutionStep(string name) => ExecutionSteps.GetValueOrDefault(name);
}