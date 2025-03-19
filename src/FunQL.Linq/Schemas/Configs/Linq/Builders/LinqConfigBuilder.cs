// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Linq.Schemas.Configs.Linq.Builders.Interfaces;
using FunQL.Linq.Schemas.Configs.Linq.Interfaces;

namespace FunQL.Linq.Schemas.Configs.Linq.Builders;

/// <summary>Default implementation of the <see cref="ILinqConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class LinqConfigBuilder(
    IMutableLinqConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), ILinqConfigBuilder
{
    /// <inheritdoc cref="ILinqConfigBuilder.MutableConfig"/>
    public override IMutableLinqConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="ILinqConfigBuilder.Build"/>
    public override ILinqConfigExtension Build() => MutableConfig.ToConfig();
}