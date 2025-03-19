// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IListTypeConfig"/>.</summary>
public interface IListTypeConfigBuilder : ITypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder.MutableConfig"/>
    public new IMutableListTypeConfig MutableConfig { get; }

    /// <inheritdoc cref="ITypeConfigBuilder.HasType"/>
    public new IListTypeConfigBuilder HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.IsNullable"/>
    public new IListTypeConfigBuilder IsNullable(bool nullable = true);

    /// <summary>Configures a simple type as the item type for the list.</summary>
    /// <param name="type">The CLR type of the simple items in the list.</param>
    /// <returns>An <see cref="ISimpleTypeConfigBuilder"/> for further configuration of the item type.</returns>
    public ISimpleTypeConfigBuilder SimpleItem(Type type);

    /// <summary>Configures an object type as the item type for the list.</summary>
    /// <param name="type">The CLR type of the object items in the list.</param>
    /// <returns>An <see cref="IObjectTypeConfigBuilder"/> for further configuration of the item type.</returns>
    public IObjectTypeConfigBuilder ObjectItem(Type type);

    /// <summary>Configures a list type as the item type for the list.</summary>
    /// <param name="type">The CLR type of the list items in the list.</param>
    /// <returns>An <see cref="IListTypeConfigBuilder"/> for further configuration of the item type.</returns>
    public IListTypeConfigBuilder ListItem(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.Build"/>
    public new IListTypeConfig Build();
}