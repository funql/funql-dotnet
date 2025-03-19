// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IParameterConfig"/>.</summary>
public interface IParameterConfigBuilder : IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public new IMutableParameterConfig MutableConfig { get; }

    /// <summary>Configures <see cref="IMutableParameterConfig.Name"/>.</summary>
    /// <param name="name">Name of the parameter.</param>
    /// <returns>The builder to continue building.</returns>
    public IParameterConfigBuilder HasName(string name);

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public new IParameterConfig Build();
}