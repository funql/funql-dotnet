// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Sorting.Configs.Builders.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISortParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class SortParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), ISortParameterConfigBuilder
{
    /// <inheritdoc/>
    public new ISortParameterConfigBuilder HasName(string name) => (ISortParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public ISortParameterConfigBuilder HasDefaultValue(Sort? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }
}