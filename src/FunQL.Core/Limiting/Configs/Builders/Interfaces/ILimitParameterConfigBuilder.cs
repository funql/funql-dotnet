// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Configs.Interfaces;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="Limit"/> <see cref="IParameterConfig"/>.</summary>
public interface ILimitParameterConfigBuilder : IParameterConfigBuilder
{
    /// <inheritdoc cref="IParameterConfigBuilder.HasName"/>
    public new ILimitParameterConfigBuilder HasName(string name);

    /// <summary>Configures <see cref="IMutableParameterConfig.DefaultValue"/>.</summary>
    /// <param name="defaultValue">Default value to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public ILimitParameterConfigBuilder HasDefaultValue(Limit? defaultValue);

    /// <summary>Configures <see cref="IMutableLimitConfigExtension.MaxLimit"/>.</summary>
    /// <param name="maxLimit">Maximum value of <see cref="Limit"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public ILimitParameterConfigBuilder HasMaxLimit(int maxLimit);
}