// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;

namespace FunQL.Linq.Common.Visitors.Extensions;

/// <summary>Extensions related to <see cref="ILinqVisitorState"/>.</summary>
public static class LinqVisitorStateExtensions
{
    /// <summary>Returns <see cref="ILinqVisitorState.Result"/> or throws if <c>null</c>.</summary>
    public static Expression RequireResult(this ILinqVisitorState state) =>
        state.Result ?? throw new InvalidOperationException("Result is required");

    /// <summary>
    /// Returns <see cref="ILinqVisitorState.Result"/> as <typeparamref name="T"/> or throws if it's not of type
    /// <typeparamref name="T"/> or <c>null</c>.
    /// </summary>
    public static T RequireResult<T>(this ILinqVisitorState state) where T : Expression =>
        state.Result as T ?? throw new InvalidOperationException($"Result must be of type {typeof(T)}");

    /// <summary>
    /// Returns <see cref="ILinqVisitorState.Source"/> as <typeparamref name="T"/> or throws if it's not of type
    /// <typeparamref name="T"/>.
    /// </summary>
    public static T RequireSource<T>(this ILinqVisitorState state) where T : Expression =>
        state.Source as T ?? throw new InvalidOperationException($"Source must be of type {typeof(T)}");
}