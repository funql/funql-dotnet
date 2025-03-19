// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="Filter"/> parameter config.</summary>
public interface IFilterParameterConfigBuilder : IParameterConfigBuilder
{
    /// <inheritdoc cref="IParameterConfigBuilder.HasName"/>
    public new IFilterParameterConfigBuilder HasName(string name);

    /// <summary>Configures <see cref="IMutableParameterConfig.DefaultValue"/>.</summary>
    /// <param name="defaultValue">Default value to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterParameterConfigBuilder HasDefaultValue(Filter? defaultValue);
}