// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Skipping.Configs.Builders.Interfaces;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISkipParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class SkipParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), ISkipParameterConfigBuilder
{
    /// <inheritdoc/>
    public new ISkipParameterConfigBuilder HasName(string name) => (ISkipParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public ISkipParameterConfigBuilder HasDefaultValue(Skip? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }
}