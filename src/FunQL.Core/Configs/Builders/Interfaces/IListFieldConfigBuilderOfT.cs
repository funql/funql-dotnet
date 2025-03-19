// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="IFieldConfig"/> for type <typeparamref name="TElement"/>.
/// </summary>
/// <inheritdoc cref="IFieldConfigBuilder{T}"/>
public interface IListFieldConfigBuilder<TElement> : IFieldConfigBuilder<TElement>, IListFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasName"/>
    public new IListFieldConfigBuilder<TElement> HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasType"/>
    public new IListFieldConfigBuilder<TElement> HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.IsNullable"/>
    public new IListFieldConfigBuilder<TElement> IsNullable(bool nullable = true);

    /// <inheritdoc cref="IListTypeConfigBuilder{T}.SimpleItem()"/>
    public ISimpleTypeConfigBuilder<TElement> SimpleItem();

    /// <inheritdoc cref="IListTypeConfigBuilder{T}.ObjectItem()"/>
    public IObjectTypeConfigBuilder<TElement> ObjectItem();

    /// <inheritdoc cref="IListTypeConfigBuilder{T}.ListItem{T}"/>
    public IListTypeConfigBuilder<TNestedElement> ListItem<TNestedElement>();
}