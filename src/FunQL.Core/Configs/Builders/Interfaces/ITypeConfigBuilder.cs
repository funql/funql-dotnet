// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="ITypeConfig"/>.</summary>
public interface ITypeConfigBuilder : IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public new IMutableTypeConfig MutableConfig { get; }

    /// <summary>Configures <see cref="IMutableTypeConfig.Type"/>.</summary>
    /// <param name="type">Type to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public ITypeConfigBuilder HasType(Type type);

    /// <summary>Configures <see cref="IMutableTypeConfig.IsNullable"/>.</summary>
    /// <param name="nullable">Whether type is nullable. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public ITypeConfigBuilder IsNullable(bool nullable = true);

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public new ITypeConfig Build();
}