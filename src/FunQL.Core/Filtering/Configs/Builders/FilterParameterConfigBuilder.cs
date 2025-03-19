// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Configs.Builders.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.Builders;

/// <summary>Default implementation of the <see cref="IFilterParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class FilterParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), IFilterParameterConfigBuilder
{
    /// <inheritdoc/>
    public new IFilterParameterConfigBuilder HasName(string name) => (IFilterParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public IFilterParameterConfigBuilder HasDefaultValue(Filter? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }
}