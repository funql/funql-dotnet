// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Linq.Schemas.Configs.Linq.Interfaces;

namespace FunQL.Linq.Schemas.Configs.Linq.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="ILinqConfigExtension"/>.</summary>
public interface ILinqConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableLinqConfigExtension MutableConfig { get; }

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new ILinqConfigExtension Build();
}