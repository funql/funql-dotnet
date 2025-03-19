// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ExtensibleConfigBuilder"/>
public abstract class ParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ExtensibleConfigBuilder(mutableConfig), IParameterConfigBuilder
{
    /// <inheritdoc cref="IParameterConfigBuilder.MutableConfig"/>
    public override IMutableParameterConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public virtual IParameterConfigBuilder HasName(string name)
    {
        MutableConfig.Name = name;
        return this;
    }

    /// <inheritdoc cref="IParameterConfigBuilder.Build"/>
    public override IParameterConfig Build() => MutableConfig.ToConfig();
}