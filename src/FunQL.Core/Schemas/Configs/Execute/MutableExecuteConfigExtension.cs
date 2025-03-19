// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Common.Executors;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;
using FunQL.Core.Schemas.Executors;

namespace FunQL.Core.Schemas.Configs.Execute;

/// <summary>
/// Default implementation of <see cref="IMutableExecuteConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
///   - <see cref="ExecutorStateFactory"/> will enter <see cref="SchemaConfigExecuteContext"/>
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableExecuteConfigExtension : MutableConfigExtension, IMutableExecuteConfigExtension
{
    /// <summary>Current list of steps.</summary>
    private readonly List<ExecutionStep> _steps = [];

    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutableExecuteConfigExtension(string name) : base(name)
    {
        IExecutor? executor = null;
        ExecutorProvider = _ => executor ??= new PipelineExecutor(_steps);

        ExecutorStateFactory = schemaConfig =>
        {
            var state = new ExecutorState();
            state.EnterContext(new SchemaConfigExecuteContext(schemaConfig));
            return state;
        };
    }

    /// <inheritdoc cref="IMutableExecuteConfigExtension.ExecutorProvider"/>
    public Func<ISchemaConfig, IExecutor> ExecutorProvider { get; set; }

    /// <inheritdoc cref="IMutableExecuteConfigExtension.ExecutorStateFactory"/>
    public Func<ISchemaConfig, IExecutorState> ExecutorStateFactory { get; set; }

    /// <inheritdoc/>
    public IEnumerable<ExecutionStep> GetExecutionSteps() => _steps;
    
    /// <inheritdoc/>
    public ExecutionStep? FindExecutionStep(string name) =>
        _steps.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddExecutionStep(ExecutionStep step)
    {
        _steps.Add(step);
    }

    /// <inheritdoc/>
    public ExecutionStep? RemoveExecutionStep(string name)
    {
        var step = FindExecutionStep(name);
        if (step != null)
            _steps.Remove(step);

        return step;
    }

    /// <inheritdoc cref="IMutableExecuteConfigExtension.ToConfig"/>
    public override IExecuteConfigExtension ToConfig() => new ImmutableExecuteConfigExtension(
        Name, ExecutorProvider, ExecutorStateFactory, _steps.ToImmutableDictionary(it => it.Name, it => it)
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}