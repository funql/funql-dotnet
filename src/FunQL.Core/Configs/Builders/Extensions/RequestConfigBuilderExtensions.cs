// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;

namespace FunQL.Core.Configs.Builders.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfigBuilder"/>.</summary>
public static class RequestConfigBuilderExtensions
{
    /// <summary>Configures the request to return a simple type.</summary>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <typeparam name="T">The CLR type of the return value.</typeparam>
    /// <returns>An <see cref="ISimpleFieldConfigBuilder{T}"/> to further configure the return type.</returns>
    public static ISimpleTypeConfigBuilder<T> SimpleReturn<T>(this IRequestConfigBuilder builder) =>
        new SimpleTypeConfigBuilder<T>(builder.SimpleReturn(typeof(T)));

    /// <summary>Configures the request to return an object type.</summary>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <typeparam name="T">The CLR type of the return value.</typeparam>
    /// <returns>An <see cref="IObjectTypeConfigBuilder{T}"/> to further configure the return type.</returns>
    public static IObjectTypeConfigBuilder<T> ObjectReturn<T>(this IRequestConfigBuilder builder) =>
        new ObjectTypeConfigBuilder<T>(builder.ObjectReturn(typeof(T)));

    /// <summary>Configures the request to return a list type.</summary>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <typeparam name="T">The CLR type of the return value.</typeparam>
    /// <typeparam name="TElement">The CLR type of the elements in the list.</typeparam>
    /// <returns>An <see cref="IListTypeConfigBuilder{T}"/> to further configure the return type.</returns>
    public static IListTypeConfigBuilder<TElement> ListReturn<T, TElement>(this IRequestConfigBuilder builder)
        where T : IEnumerable<TElement> =>
        new ListTypeConfigBuilder<TElement>(builder.ListReturn(typeof(T)));

    /// <summary>Configures the request to return a simple type.</summary>
    /// <typeparam name="T">The CLR type of the return value.</typeparam>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <param name="action">Action to configure the simple type.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder ReturnsSimple<T>(
        this IRequestConfigBuilder builder,
        Action<ISimpleTypeConfigBuilder<T>> action
    )
    {
        action(builder.SimpleReturn<T>());
        return builder;
    }

    /// <summary>Configures the request to return an object type.</summary>
    /// <typeparam name="T">The CLR type of the return value.</typeparam>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <param name="action">Action to configure the object type.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder ReturnsObject<T>(
        this IRequestConfigBuilder builder,
        Action<IObjectTypeConfigBuilder<T>> action
    )
    {
        action(builder.ObjectReturn<T>());
        return builder;
    }

    /// <summary>Configures the request to return a list type of objects for type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The CLR type of the elements in the list.</typeparam>
    /// <param name="builder">The <see cref="IRequestConfigBuilder"/> to configure.</param>
    /// <param name="action">Action to configure the element type <typeparamref name="T"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder ReturnsListOfObjects<T>(
        this IRequestConfigBuilder builder,
        Action<IObjectTypeConfigBuilder<T>> action
    )
    {
        action(builder.ListReturn<List<T>, T>().ObjectItem());
        return builder;
    }
}