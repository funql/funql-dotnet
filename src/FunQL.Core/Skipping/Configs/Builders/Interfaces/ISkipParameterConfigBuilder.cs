// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="Skip"/> <see cref="IParameterConfig"/>.</summary>
public interface ISkipParameterConfigBuilder : IParameterConfigBuilder
{
    /// <inheritdoc cref="IParameterConfigBuilder.HasName"/>
    public new ISkipParameterConfigBuilder HasName(string name);

    /// <summary>Configures <see cref="IMutableParameterConfig.DefaultValue"/>.</summary>
    /// <param name="defaultValue">Default value to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public ISkipParameterConfigBuilder HasDefaultValue(Skip? defaultValue);
}