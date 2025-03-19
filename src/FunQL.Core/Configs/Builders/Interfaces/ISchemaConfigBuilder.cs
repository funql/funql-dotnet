// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="ISchemaConfig"/>.</summary>
public interface ISchemaConfigBuilder : IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public new IMutableSchemaConfig MutableConfig { get; }

    /// <summary>
    /// Gets or adds <see cref="IMutableRequestConfig"/> for <see cref="IMutableSchemaConfig.GetRequestConfigs"/> with
    /// given <paramref name="name"/> and returns <see cref="IRequestConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="name">Name of the request.</param>
    /// <returns>The <see cref="IRequestConfigBuilder"/> to build <see cref="IMutableRequestConfig"/>.</returns>
    public IRequestConfigBuilder Request(string name);

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public new ISchemaConfig Build();
}