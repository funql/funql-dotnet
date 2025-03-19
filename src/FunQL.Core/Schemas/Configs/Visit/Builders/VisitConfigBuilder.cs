// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Schemas.Configs.Visit.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;

namespace FunQL.Core.Schemas.Configs.Visit.Builders;

/// <summary>Default implementation of the <see cref="IVisitConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class VisitConfigBuilder(
    IMutableVisitConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IVisitConfigBuilder
{
    /// <inheritdoc cref="IVisitConfigBuilder.MutableConfig"/>
    public override IMutableVisitConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IVisitConfigBuilder.Build"/>
    public override IVisitConfigExtension Build() => MutableConfig.ToConfig();
}