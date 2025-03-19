// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IRequestConfig"/>.</summary>
public interface IRequestConfigBuilder : IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public new IMutableRequestConfig MutableConfig { get; }

    /// <summary>Configures <see cref="IMutableRequestConfig.Name"/>.</summary>
    /// <param name="name">Name of the request.</param>
    /// <returns>The builder to continue building.</returns>
    public IRequestConfigBuilder HasName(string name);

    /// <summary>Configures a simple type as the return type of the request.</summary>
    /// <param name="type">The CLR <see cref="Type"/> being returned by the request.</param>
    /// <returns>An <see cref="ISimpleTypeConfigBuilder"/> for further configuration of the return type.</returns>
    public ISimpleTypeConfigBuilder SimpleReturn(Type type);

    /// <summary>Configures an object type as the return type of the request.</summary>
    /// <param name="type">The CLR <see cref="Type"/> being returned by the request.</param>
    /// <returns>An <see cref="IObjectTypeConfigBuilder"/> for further configuration of the return type.</returns>
    public IObjectTypeConfigBuilder ObjectReturn(Type type);

    /// <summary>Configures a list type as the return type of the request.</summary>
    /// <param name="type">The CLR <see cref="Type"/> being returned by the request.</param>
    /// <returns>An <see cref="IListTypeConfigBuilder"/> for further configuration of the return type.</returns>
    public IListTypeConfigBuilder ListReturn(Type type);

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public new IRequestConfig Build();
}