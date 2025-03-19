// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IConfigBuilder"/>.</summary>
/// <param name="mutableConfig">The <see cref="MutableConfig"/> being configured.</param>
public abstract class ConfigBuilder(IMutableConfig mutableConfig) : IConfigBuilder
{
    /// <inheritdoc/>
    public virtual IMutableConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public abstract IConfig Build();
}