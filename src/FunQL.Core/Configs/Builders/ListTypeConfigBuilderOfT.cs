// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IListTypeConfigBuilder{TElement}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="TypeConfigBuilder{T}"/>
public sealed class ListTypeConfigBuilder<TElement>(
    IListTypeConfigBuilder builder
) : TypeConfigBuilder<TElement>, IListTypeConfigBuilder<TElement>
{
    /// <inheritdoc/>
    protected override IListTypeConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="IListTypeConfigBuilder.MutableConfig"/>
    public override IMutableListTypeConfig MutableConfig => Builder.MutableConfig;

    /// <inheritdoc cref="IListTypeConfigBuilder{TElement}.HasType"/>
    public override IListTypeConfigBuilder<TElement> HasType(Type type) =>
        (IListTypeConfigBuilder<TElement>)base.HasType(type);

    /// <inheritdoc cref="IListTypeConfigBuilder{T}.IsNullable"/>
    public override IListTypeConfigBuilder<TElement> IsNullable(bool nullable = true) =>
        (IListTypeConfigBuilder<TElement>)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleTypeConfigBuilder<TElement> SimpleItem()
    {
        var builder = Builder.SimpleItem(typeof(TElement));
        return new SimpleTypeConfigBuilder<TElement>(builder);
    }

    /// <inheritdoc/>
    public IObjectTypeConfigBuilder<TElement> ObjectItem()
    {
        var builder = Builder.ObjectItem(typeof(TElement));
        return new ObjectTypeConfigBuilder<TElement>(builder);
    }

    /// <inheritdoc/>
    public IListTypeConfigBuilder<TNestedElement> ListItem<TNestedElement>()
    {
        var builder = Builder.ListItem(typeof(TElement));
        return new ListTypeConfigBuilder<TNestedElement>(builder);
    }

    /// <inheritdoc cref="IListTypeConfigBuilder.Build"/>
    public override IListTypeConfig Build() => (IListTypeConfig)base.Build();

    #region IListTypeConfigBuilder

    /// <inheritdoc/>
    IListTypeConfigBuilder IListTypeConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    IListTypeConfigBuilder IListTypeConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    /// <inheritdoc/>
    ISimpleTypeConfigBuilder IListTypeConfigBuilder.SimpleItem(Type type) => Builder.SimpleItem(type);

    /// <inheritdoc/>
    IObjectTypeConfigBuilder IListTypeConfigBuilder.ObjectItem(Type type) => Builder.ObjectItem(type);

    /// <inheritdoc/>
    IListTypeConfigBuilder IListTypeConfigBuilder.ListItem(Type type) => Builder.ListItem(type);

    #endregion
}