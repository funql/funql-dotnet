// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IObjectTypeConfig"/>.</summary>
public interface IObjectTypeConfigBuilder : ITypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder.MutableConfig"/>
    public new IMutableObjectTypeConfig MutableConfig { get; }

    /// <inheritdoc cref="ITypeConfigBuilder.HasType"/>
    public new IObjectTypeConfigBuilder HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.IsNullable"/>
    public new IObjectTypeConfigBuilder IsNullable(bool nullable = true);

    /// <summary>Adds a simple field with the specified name and type to this object type configuration.</summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="type">The CLR type of the field.</param>
    /// <returns>An <see cref="ISimpleFieldConfigBuilder"/> to further configure the field.</returns>
    public ISimpleFieldConfigBuilder SimpleField(string name, Type type);

    /// <summary>Adds an object field with the specified name and type to this object type configuration.</summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="type">The CLR type of the field.</param>
    /// <returns>An <see cref="IObjectFieldConfigBuilder"/> to further configure the field.</returns>
    public IObjectFieldConfigBuilder ObjectField(string name, Type type);

    /// <summary>Adds a list field with the specified name and type to this object type configuration.</summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="type">The CLR type of the field.</param>
    /// <returns>An <see cref="IListFieldConfigBuilder"/> to further configure the field.</returns>
    public IListFieldConfigBuilder ListField(string name, Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.Build"/>
    public new IObjectTypeConfig Build();
}