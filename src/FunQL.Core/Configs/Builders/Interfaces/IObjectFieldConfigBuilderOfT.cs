// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="IFieldConfig"/> for object fields of type <typeparamref name="T"/>.
/// </summary>
/// <inheritdoc cref="IFieldConfigBuilder{T}"/>
public interface IObjectFieldConfigBuilder<T> : IFieldConfigBuilder<T>, IObjectFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasName"/>
    public new IObjectFieldConfigBuilder<T> HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasType"/>
    public new IObjectFieldConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.IsNullable"/>
    public new IObjectFieldConfigBuilder<T> IsNullable(bool nullable = true);

    /// <inheritdoc cref="IObjectTypeConfigBuilder{T}.SimpleField{T}"/>
    public ISimpleFieldConfigBuilder<TField> SimpleField<TField>(Expression<Func<T, TField>> expression);

    /// <inheritdoc cref="IObjectTypeConfigBuilder{T}.ObjectField{T}"/>
    public IObjectFieldConfigBuilder<TField> ObjectField<TField>(Expression<Func<T, TField>> expression);

    /// <inheritdoc cref="IObjectTypeConfigBuilder{T}.ListField{T}"/>
    public IListFieldConfigBuilder<TElement> ListField<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression);
}