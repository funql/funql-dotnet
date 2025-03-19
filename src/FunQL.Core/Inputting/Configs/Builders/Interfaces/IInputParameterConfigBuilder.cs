// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="Input"/> <see cref="IParameterConfig"/>.</summary>
public interface IInputParameterConfigBuilder : IParameterConfigBuilder
{
    /// <inheritdoc cref="IParameterConfigBuilder.HasName"/>
    public new IInputParameterConfigBuilder HasName(string name);

    /// <summary>Configures <see cref="IMutableParameterConfig.DefaultValue"/>.</summary>
    /// <param name="defaultValue">Default value to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public IInputParameterConfigBuilder HasDefaultValue(Input? defaultValue);
}