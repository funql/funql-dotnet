// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISimpleFieldConfigBuilder"/>.</summary>
/// <inheritdoc cref="FieldConfigBuilder"/>
public sealed class SimpleFieldConfigBuilder(
    IMutableFieldConfig mutableConfig
) : FieldConfigBuilder(mutableConfig), ISimpleFieldConfigBuilder
{
    /// <inheritdoc cref="ISimpleFieldConfigBuilder.HasName"/>
    public override ISimpleFieldConfigBuilder HasName(string name) => (ISimpleFieldConfigBuilder)base.HasName(name);

    /// <inheritdoc cref="ISimpleFieldConfigBuilder.HasType"/>
    public override ISimpleFieldConfigBuilder HasType(Type type) => (ISimpleFieldConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="ISimpleFieldConfigBuilder.IsNullable"/>
    public override ISimpleFieldConfigBuilder IsNullable(bool nullable = true) =>
        (ISimpleFieldConfigBuilder)base.IsNullable(nullable);
}