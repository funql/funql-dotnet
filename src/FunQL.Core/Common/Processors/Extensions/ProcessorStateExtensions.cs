// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Processors.Extensions;

/// <summary>Extensions for <see cref="IProcessorState{TContext}"/>.</summary>
public static class ProcessorStateExtensions
{
    /// <summary>Find context of type <typeparamref name="T"/>.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="TContext">Type of the contexts in state.</typeparam>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context or <c>null</c> if not found.</returns>
    public static T? FindContext<TContext, T>(this IProcessorState<TContext> state)
        where TContext : IProcessContext
        where T : TContext => (T?)state.FindContext(typeof(T));

    /// <summary>Require context for given <paramref name="type"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <param name="type">Type of the context to find.</param>
    /// <typeparam name="TContext">Type of the contexts in state.</typeparam>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static TContext RequireContext<TContext>(this IProcessorState<TContext> state, Type type)
        where TContext : IProcessContext =>
        state.FindContext(type) ?? throw new InvalidOperationException($"Required context not found for type {type}");

    /// <summary>Require context of type <typeparamref name="T"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="TContext">Type of the contexts in state.</typeparam>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static T RequireContext<TContext, T>(this IProcessorState<TContext> state)
        where TContext : IProcessContext
        where T : TContext => (T)state.RequireContext(typeof(T));
}