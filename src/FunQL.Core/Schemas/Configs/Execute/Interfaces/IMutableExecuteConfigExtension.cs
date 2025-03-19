// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Interfaces;

/// <summary>Mutable version of <see cref="IExecuteConfigExtension"/>.</summary>
public interface IMutableExecuteConfigExtension : IExecuteConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IExecuteConfigExtension.ExecutorProvider"/>
    public new Func<ISchemaConfig, IExecutor> ExecutorProvider { get; set; }

    /// <inheritdoc cref="IExecuteConfigExtension.ExecutorStateFactory"/>
    public new Func<ISchemaConfig, IExecutorState> ExecutorStateFactory { get; set; }

    /// <summary>Adds given <paramref name="step"/>.</summary>
    /// <param name="step">Step to add.</param>
    public void AddExecutionStep(ExecutionStep step);
    
    /// <summary>Removes the <see cref="ExecutionStep"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="ExecutionStep"/>.</param>
    /// <returns>The <see cref="ExecutionStep"/> that was removed or null if it did not exist.</returns>
    public ExecutionStep? RemoveExecutionStep(string name);

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IExecuteConfigExtension ToConfig();
}