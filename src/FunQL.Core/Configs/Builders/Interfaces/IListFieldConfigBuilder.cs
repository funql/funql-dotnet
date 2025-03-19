// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFieldConfig"/> for list types.</summary>
public interface IListFieldConfigBuilder : IFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder.HasName"/>
    public new IListFieldConfigBuilder HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder.HasType"/>
    public new IListFieldConfigBuilder HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder.IsNullable"/>
    public new IListFieldConfigBuilder IsNullable(bool nullable = true);

    /// <inheritdoc cref="IListTypeConfigBuilder.SimpleItem"/>
    public ISimpleTypeConfigBuilder SimpleItem(Type type);

    /// <inheritdoc cref="IListTypeConfigBuilder.ObjectItem"/>
    public IObjectTypeConfigBuilder ObjectItem(Type type);

    /// <inheritdoc cref="IListTypeConfigBuilder.ListItem"/>
    public IListTypeConfigBuilder ListItem(Type type);
}