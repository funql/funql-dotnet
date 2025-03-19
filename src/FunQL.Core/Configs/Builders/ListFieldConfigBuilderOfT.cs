// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IListFieldConfigBuilder{TElement}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="FieldConfigBuilder{TElement}"/>
public sealed class ListFieldConfigBuilder<TElement>(
    IListFieldConfigBuilder builder
) : FieldConfigBuilder<TElement>, IListFieldConfigBuilder<TElement>
{
    /// <inheritdoc/>
    protected override IListFieldConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="IListFieldConfigBuilder{TElement}.HasName"/>
    public override IListFieldConfigBuilder<TElement> HasName(string name) =>
        (IListFieldConfigBuilder<TElement>)base.HasName(name);

    /// <inheritdoc cref="IListFieldConfigBuilder{TElement}.HasType"/>
    public override IListFieldConfigBuilder<TElement> HasType(Type type) =>
        (IListFieldConfigBuilder<TElement>)base.HasType(type);

    /// <inheritdoc cref="IListFieldConfigBuilder{TElement}.IsNullable"/>
    public override IListFieldConfigBuilder<TElement> IsNullable(bool nullable = true) =>
        (IListFieldConfigBuilder<TElement>)base.IsNullable(nullable);

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
        var builder = Builder.ListItem(typeof(TNestedElement));
        return new ListTypeConfigBuilder<TNestedElement>(builder);
    }

    #region IListFieldConfigBuilder

    /// <inheritdoc/>
    IListFieldConfigBuilder IListFieldConfigBuilder.HasName(string name) => HasName(name);

    /// <inheritdoc/>
    IListFieldConfigBuilder IListFieldConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    IListFieldConfigBuilder IListFieldConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    /// <inheritdoc/>
    ISimpleTypeConfigBuilder IListFieldConfigBuilder.SimpleItem(Type type) => Builder.SimpleItem(type);

    /// <inheritdoc/>
    IObjectTypeConfigBuilder IListFieldConfigBuilder.ObjectItem(Type type) => Builder.ObjectItem(type);

    /// <inheritdoc/>
    IListTypeConfigBuilder IListFieldConfigBuilder.ListItem(Type type) => Builder.ListItem(type);

    #endregion
}