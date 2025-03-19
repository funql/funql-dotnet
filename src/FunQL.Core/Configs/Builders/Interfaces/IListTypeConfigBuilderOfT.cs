// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="IListTypeConfig"/> for type <typeparamref name="TElement"/>.
/// </summary>
/// <inheritdoc cref="ITypeConfigBuilder{T}"/>
public interface IListTypeConfigBuilder<TElement> : ITypeConfigBuilder<TElement>, IListTypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder{T}.HasType"/>
    public new IListTypeConfigBuilder<TElement> HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder{T}.IsNullable"/>
    public new IListTypeConfigBuilder<TElement> IsNullable(bool nullable = true);

    /// <summary>Configures the items to be simple items of type <typeparamref name="TElement"/>.</summary>
    /// <returns>An <see cref="ISimpleTypeConfigBuilder{T}"/> for further configuration of the item type.</returns>
    public ISimpleTypeConfigBuilder<TElement> SimpleItem();

    /// <summary>Configures the items to be object items of type <typeparamref name="TElement"/>.</summary>
    /// <returns>An <see cref="IObjectTypeConfigBuilder{T}"/> for further configuration of the item type.</returns>
    public IObjectTypeConfigBuilder<TElement> ObjectItem();

    /// <summary>Configures the items to be list items with element types of <typeparamref name="TElement"/>.</summary>
    /// <typeparam name="TNestedElement">Type of the item in the list.</typeparam>
    /// <returns>An <see cref="IListTypeConfigBuilder{T}"/> for further configuration of the item type.</returns>
    public IListTypeConfigBuilder<TNestedElement> ListItem<TNestedElement>();
}