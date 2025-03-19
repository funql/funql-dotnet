// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Counting.Configs.Builders.Interfaces;
using FunQL.Core.Counting.Nodes;

namespace FunQL.Core.Counting.Configs.Builders;

/// <summary>Default implementation of the <see cref="ICountParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class CountParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), ICountParameterConfigBuilder
{
    /// <inheritdoc/>
    public new ICountParameterConfigBuilder HasName(string name) => (ICountParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public ICountParameterConfigBuilder HasDefaultValue(Count? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }
}