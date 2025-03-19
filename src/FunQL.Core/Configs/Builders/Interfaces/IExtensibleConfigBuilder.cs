// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IExtensibleConfig"/>.</summary>
public interface IExtensibleConfigBuilder : IConfigBuilder
{
    /// <inheritdoc cref="IConfigBuilder.MutableConfig"/>
    public new IMutableExtensibleConfig MutableConfig { get; }

    /// <inheritdoc cref="IConfigBuilder.Build"/>
    public new IExtensibleConfig Build();
}