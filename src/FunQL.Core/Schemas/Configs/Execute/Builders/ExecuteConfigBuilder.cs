// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Builders;

/// <summary>Default implementation of the <see cref="IExecuteConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class ExecuteConfigBuilder(
    IMutableExecuteConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IExecuteConfigBuilder
{
    /// <inheritdoc cref="IExecuteConfigBuilder.MutableConfig"/>
    public override IMutableExecuteConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IExecuteConfigBuilder WithExecutionStep(ExecutionStep step)
    {
        // Replace any existing ExecutionStep
        MutableConfig.RemoveExecutionStep(step.Name);
        MutableConfig.AddExecutionStep(step);
        return this;
    }

    /// <inheritdoc cref="IExecuteConfigBuilder.Build"/>
    public override IExecuteConfigExtension Build() => MutableConfig.ToConfig();
}