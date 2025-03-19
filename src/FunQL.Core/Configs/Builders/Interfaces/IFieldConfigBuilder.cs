// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFieldConfig"/>.</summary>
public interface IFieldConfigBuilder : IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public new IMutableFieldConfig MutableConfig { get; }

    /// <summary>Configures <see cref="IMutableFieldConfig.Name"/>.</summary>
    /// <param name="name">Name of the field.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldConfigBuilder HasName(string name);

    /// <summary>
    /// Configures <see cref="IMutableFieldConfig.TypeConfig"/> <see cref="IMutableTypeConfig.Type"/>.
    /// </summary>
    /// <param name="type">Type to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldConfigBuilder HasType(Type type);

    /// <summary>
    /// Configures <see cref="IMutableFieldConfig.TypeConfig"/> <see cref="IMutableTypeConfig.IsNullable"/>.
    /// </summary>
    /// <param name="nullable">Whether type is nullable. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldConfigBuilder IsNullable(bool nullable = true);

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public new IFieldConfig Build();
}