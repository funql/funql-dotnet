// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Interfaces;

/// <summary>Extension specifying the configuration for executing requests.</summary>
public interface IExecuteConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.ExecuteConfigExtension";

    /// <summary>Provider for the <see cref="IExecutor"/>.</summary>
    public Func<ISchemaConfig, IExecutor> ExecutorProvider { get; }

    /// <summary>Factory to create the <see cref="IExecutorState"/>.</summary>
    public Func<ISchemaConfig, IExecutorState> ExecutorStateFactory { get; }

    /// <summary>The list of <see cref="ExecutionStep"/> to execute.</summary>
    public IEnumerable<ExecutionStep> GetExecutionSteps();

    /// <summary>
    /// Gets the <see cref="ExecutionStep"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="ExecutionStep"/>.</param>
    /// <returns>The <see cref="ExecutionStep"/> or null if it does not exist.</returns>
    public ExecutionStep? FindExecutionStep(string name);
}