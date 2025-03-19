// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IConfigExtensionBuilder"/>.</summary>
/// <inheritdoc cref="ConfigBuilder"/>
public abstract class ConfigExtensionBuilder(
    IMutableConfigExtension mutableConfig
) : ConfigBuilder(mutableConfig), IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public override IMutableConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public abstract override IConfigExtension Build();
}