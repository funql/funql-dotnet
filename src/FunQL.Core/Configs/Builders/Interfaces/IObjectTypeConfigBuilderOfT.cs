// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="IObjectTypeConfig"/> for type <typeparamref name="T"/>.
/// </summary>
/// <inheritdoc cref="ITypeConfigBuilder{T}"/>
public interface IObjectTypeConfigBuilder<T> : ITypeConfigBuilder<T>, IObjectTypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder{T}.HasType"/>
    public new IObjectTypeConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder{T}.IsNullable"/>
    public new IObjectTypeConfigBuilder<T> IsNullable(bool nullable = true);

    /// <summary>Adds a simple field to the object type configuration for given <paramref name="expression"/>.</summary>
    /// <typeparam name="TField">The type of the field being configured.</typeparam>
    /// <param name="expression">An expression representing the field, such as <c>x => x.FieldName</c>.</param>
    /// <returns>An <see cref="ISimpleFieldConfigBuilder{TField}"/> to further configure the field.</returns>
    public ISimpleFieldConfigBuilder<TField> SimpleField<TField>(Expression<Func<T, TField>> expression);

    /// <summary>
    /// Adds an object field to the object type configuration for given <paramref name="expression"/>.
    /// </summary>
    /// <typeparam name="TField">The type of the field being configured.</typeparam>
    /// <param name="expression">An expression representing the field, such as <c>x => x.FieldName</c>.</param>
    /// <returns>An <see cref="IObjectFieldConfigBuilder{TField}"/> to further configure the field.</returns>
    public IObjectFieldConfigBuilder<TField> ObjectField<TField>(Expression<Func<T, TField>> expression);

    /// <summary>Adds a list field to the object type configuration for given <paramref name="expression"/>.</summary>
    /// <typeparam name="TElement">The type of the elements in the list field.</typeparam>
    /// <param name="expression">An expression representing the field, such as <c>x => x.ListField</c>.</param>
    /// <returns>An <see cref="IListFieldConfigBuilder{TField}"/> to further configure the field.</returns>
    public IListFieldConfigBuilder<TElement> ListField<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression);
}