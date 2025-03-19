// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Schemas.Configs.Print.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Print.Interfaces;

namespace FunQL.Core.Schemas.Configs.Print.Builders;

/// <summary>Default implementation of the <see cref="IPrintConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class PrintConfigBuilder(
    IMutablePrintConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IPrintConfigBuilder
{
    /// <inheritdoc cref="IPrintConfigBuilder.MutableConfig"/>
    public override IMutablePrintConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IPrintConfigBuilder.Build"/>
    public override IPrintConfigExtension Build() => MutableConfig.ToConfig();
}