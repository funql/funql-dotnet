// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Configs.Builders.Interfaces;
using FunQL.Core.Limiting.Configs.Extensions;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Configs.Builders;

/// <summary>Default implementation of the <see cref="ILimitParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class LimitParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), ILimitParameterConfigBuilder
{
    /// <inheritdoc/>
    public new ILimitParameterConfigBuilder HasName(string name) => (ILimitParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public ILimitParameterConfigBuilder HasDefaultValue(Limit? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }

    /// <inheritdoc/>
    public ILimitParameterConfigBuilder HasMaxLimit(int maxLimit)
    {
        MutableConfig.GetOrAddLimitConfigExtension().MaxLimit = maxLimit;
        return this;
    }
}