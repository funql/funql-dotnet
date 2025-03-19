// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISimpleFieldConfigBuilder{T}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="FieldConfigBuilder{T}"/>
public sealed class SimpleFieldConfigBuilder<T>(
    ISimpleFieldConfigBuilder builder
) : FieldConfigBuilder<T>, ISimpleFieldConfigBuilder<T>
{
    /// <inheritdoc/>
    protected override ISimpleFieldConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="ISimpleFieldConfigBuilder{T}.HasName"/>
    public override ISimpleFieldConfigBuilder<T> HasName(string name) =>
        (ISimpleFieldConfigBuilder<T>)base.HasName(name);

    /// <inheritdoc cref="ISimpleFieldConfigBuilder{T}.HasType"/>
    public override ISimpleFieldConfigBuilder<T> HasType(Type type) =>
        (ISimpleFieldConfigBuilder<T>)base.HasType(type);

    /// <inheritdoc cref="ISimpleFieldConfigBuilder{T}.IsNullable"/>
    public override ISimpleFieldConfigBuilder<T> IsNullable(bool nullable = true) =>
        (ISimpleFieldConfigBuilder<T>)base.IsNullable(nullable);

    #region ISimpleTypeConfigBuilder

    /// <inheritdoc/>
    ISimpleFieldConfigBuilder ISimpleFieldConfigBuilder.HasName(string name) => HasName(name);

    /// <inheritdoc/>
    ISimpleFieldConfigBuilder ISimpleFieldConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    ISimpleFieldConfigBuilder ISimpleFieldConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    #endregion
}