// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Processors.Extensions;

namespace FunQL.Core.Common.Validators.Extensions;

/// <summary>Extensions for <see cref="IValidatorState"/>.</summary>
public static class ValidatorStateExtensions
{
    /// <summary>Find context of type <typeparamref name="T"/>.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context or <c>null</c> if not found.</returns>
    public static T? FindContext<T>(this IValidatorState state) where T : IValidateContext =>
        state.FindContext<IValidateContext, T>();

    /// <summary>Require context for given <paramref name="type"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <param name="type">Type of the context to find.</param>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static IValidateContext RequireContext(this IValidatorState state, Type type) =>
        state.RequireContext<IValidateContext>(type);

    /// <summary>Require context of type <typeparamref name="T"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static T RequireContext<T>(this IValidatorState state) where T : IValidateContext =>
        state.RequireContext<IValidateContext, T>();
}