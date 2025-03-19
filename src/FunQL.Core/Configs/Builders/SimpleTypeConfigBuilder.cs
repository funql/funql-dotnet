// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISimpleTypeConfigBuilder"/>.</summary>
/// <inheritdoc cref="TypeConfigBuilder"/>
public sealed class SimpleTypeConfigBuilder(
    IMutableSimpleTypeConfig mutableConfig
) : TypeConfigBuilder(mutableConfig), ISimpleTypeConfigBuilder
{
    /// <inheritdoc cref="ISimpleTypeConfigBuilder.MutableConfig"/>
    public override IMutableSimpleTypeConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="ISimpleTypeConfigBuilder.HasType"/>
    public override ISimpleTypeConfigBuilder HasType(Type type) => (ISimpleTypeConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="ISimpleTypeConfigBuilder.IsNullable"/>
    public override ISimpleTypeConfigBuilder IsNullable(bool nullable = true) =>
        (ISimpleTypeConfigBuilder)base.IsNullable(nullable);

    /// <inheritdoc cref="ISimpleTypeConfigBuilder.Build"/>
    public override ISimpleTypeConfig Build() => MutableConfig.ToConfig();
}