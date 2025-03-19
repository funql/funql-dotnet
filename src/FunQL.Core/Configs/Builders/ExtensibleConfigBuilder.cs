// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IExtensibleConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigBuilder"/>
public abstract class ExtensibleConfigBuilder(
    IMutableExtensibleConfig mutableConfig
) : ConfigBuilder(mutableConfig), IExtensibleConfigBuilder
{
    /// <inheritdoc cref="IExtensibleConfigBuilder.MutableConfig"/>
    public override IMutableExtensibleConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IExtensibleConfigBuilder.Build"/>
    public abstract override IExtensibleConfig Build();
}