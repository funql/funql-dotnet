// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISimpleTypeConfigBuilder{T}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="TypeConfigBuilder{T}"/>
public sealed class SimpleTypeConfigBuilder<T>(
    ISimpleTypeConfigBuilder builder
) : TypeConfigBuilder<T>, ISimpleTypeConfigBuilder<T>
{
    /// <inheritdoc/>
    protected override ISimpleTypeConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="ISimpleTypeConfigBuilder.MutableConfig"/>
    public override IMutableSimpleTypeConfig MutableConfig => Builder.MutableConfig;

    /// <inheritdoc cref="ISimpleTypeConfigBuilder{T}.HasType"/>
    public override ISimpleTypeConfigBuilder<T> HasType(Type type) => (ISimpleTypeConfigBuilder<T>)base.HasType(type);

    /// <inheritdoc cref="ISimpleTypeConfigBuilder{T}.IsNullable"/>
    public override ISimpleTypeConfigBuilder<T> IsNullable(bool nullable = true) =>
        (ISimpleTypeConfigBuilder<T>)base.IsNullable(nullable);

    /// <inheritdoc cref="ISimpleTypeConfigBuilder.Build"/>
    public override ISimpleTypeConfig Build() => (ISimpleTypeConfig)base.Build();

    #region ISimpleTypeConfigBuilder

    /// <inheritdoc/>
    ISimpleTypeConfigBuilder ISimpleTypeConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    ISimpleTypeConfigBuilder ISimpleTypeConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    #endregion
}