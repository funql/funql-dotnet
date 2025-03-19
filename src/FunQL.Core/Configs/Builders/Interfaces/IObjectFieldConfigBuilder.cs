// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFieldConfig"/> for object types.</summary>
public interface IObjectFieldConfigBuilder : IFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder.HasName"/>
    public new IObjectFieldConfigBuilder HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder.HasType"/>
    public new IObjectFieldConfigBuilder HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder.IsNullable"/>
    public new IObjectFieldConfigBuilder IsNullable(bool nullable = true);

    /// <inheritdoc cref="IObjectTypeConfigBuilder.SimpleField"/>
    public ISimpleFieldConfigBuilder SimpleField(string name, Type type);

    /// <inheritdoc cref="IObjectTypeConfigBuilder.ObjectField"/>
    public IObjectFieldConfigBuilder ObjectField(string name, Type type);

    /// <inheritdoc cref="IObjectTypeConfigBuilder.ListField"/>
    public IListFieldConfigBuilder ListField(string name, Type type);
}