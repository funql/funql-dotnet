// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Extensions;

/// <summary>Extensions related to <see cref="Type"/>.</summary>
public static class TypeExtensions
{
    /// <summary>
    /// Returns whether <paramref name="type"/> is nullable: Class/interface or <see cref="Nullable{T}"/>.
    /// </summary>
    public static bool IsNullableType(this Type type) =>
        !type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

    /// <summary>
    /// Returns <paramref name="type"/> as nullable type: Given <paramref name="type"/> if class/interface, otherwise
    /// given <paramref name="type"/> wrapped in <see cref="Nullable{T}"/>.
    /// </summary>
    public static Type ToNullableType(this Type type) =>
        IsNullableType(type) ? type : typeof(Nullable<>).MakeGenericType(type);

    /// <summary>
    /// Unwraps given <paramref name="type"/> if necessary, returning the <see cref="Nullable{T}.Value"/> type in case
    /// <paramref name="type"/> is <see cref="Nullable{T}"/>.
    /// </summary>
    public static Type UnwrapNullableType(this Type type) =>
        Nullable.GetUnderlyingType(type) ?? type;

    /// <summary>
    /// Returns whether <paramref name="type"/> is a collection type (Collection, List, IEnumerable) and sets the
    /// <paramref name="elementType"/> to the collection's element type if it is.
    /// </summary>
    /// <remarks>
    /// Technically <see cref="string"/> is a collection of <see cref="char"/>, but <see cref="string"/> is considered
    /// not a collection by this method.
    /// </remarks>
    public static bool IsCollectionType(this Type type, out Type elementType)
    {
        elementType = type;

        // Ignore string, which implements IEnumerable<> but isn't a collection
        if (type == typeof(string))
            return false;

        var collectionInterface = type.GetInterfaces()
            .Union([type])
            .FirstOrDefault(t =>
                t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            );
        if (collectionInterface == null)
            return false;

        // First argument of IEnumerable<> is the elementType
        elementType = collectionInterface.GetGenericArguments().Single();
        return true;
    }
}