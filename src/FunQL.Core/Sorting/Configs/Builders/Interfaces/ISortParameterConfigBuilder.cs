// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="Sort"/> parameter config.</summary>
public interface ISortParameterConfigBuilder : IParameterConfigBuilder
{
    /// <inheritdoc cref="ISortParameterConfigBuilder.HasName"/>
    public new ISortParameterConfigBuilder HasName(string name);

    /// <summary>Configures <see cref="IMutableParameterConfig.DefaultValue"/>.</summary>
    /// <param name="defaultValue">Default value to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public ISortParameterConfigBuilder HasDefaultValue(Sort? defaultValue);
}