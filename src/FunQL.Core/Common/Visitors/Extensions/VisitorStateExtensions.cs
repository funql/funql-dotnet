// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Processors.Extensions;

namespace FunQL.Core.Common.Visitors.Extensions;

/// <summary>Extensions for <see cref="IVisitorState"/>.</summary>
public static class VisitorStateExtensions
{
    /// <summary>Find context of type <typeparamref name="T"/>.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context or <c>null</c> if not found.</returns>
    public static T? FindContext<T>(this IVisitorState state) where T : IVisitContext =>
        state.FindContext<IVisitContext, T>();

    /// <summary>Require context for given <paramref name="type"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <param name="type">Type of the context to find.</param>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static IVisitContext RequireContext(this IVisitorState state, Type type) =>
        state.RequireContext<IVisitContext>(type);

    /// <summary>Require context of type <typeparamref name="T"/> and throw if not found.</summary>
    /// <param name="state">State to find context.</param>
    /// <typeparam name="T">Type of the context to find.</typeparam>
    /// <returns>The found context.</returns>
    /// <exception cref="ArgumentException">If context was not found.</exception>
    public static T RequireContext<T>(this IVisitorState state) where T : IVisitContext =>
        state.RequireContext<IVisitContext, T>();

    /// <summary>Calls the visit callbacks and executes the <paramref name="action"/>.</summary>
    /// <param name="state">State to call callbacks on.</param>
    /// <param name="node">Node being visited.</param>
    /// <param name="action">Action to execute.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <typeparam name="TState">Type of the state.</typeparam>
    public static async Task OnVisit<T, TState>(
        this TState state,
        T node,
        Func<CancellationToken, Task> action,
        CancellationToken cancellationToken
    ) where T : QueryNode where TState : IVisitorState
    {
        await state.OnEnter(node, cancellationToken);
        await action(cancellationToken);
        await state.OnExit(node, cancellationToken);
    }

    /// <summary>Calls the visit callbacks and executes the <paramref name="action"/>.</summary>
    /// <param name="state">State to call callbacks on.</param>
    /// <param name="node">Node being visited.</param>
    /// <param name="action">Action to execute.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <typeparam name="TState">Type of the state.</typeparam>
    public static async Task OnVisit<T, TState>(
        this TState state,
        T node,
        Action action,
        CancellationToken cancellationToken
    ) where T : QueryNode where TState : IVisitorState
    {
        await state.OnEnter(node, cancellationToken);
        action();
        await state.OnExit(node, cancellationToken);
    }

    /// <summary>Calls the visit callbacks.</summary>
    /// <param name="state">State to call callbacks on.</param>
    /// <param name="node">Node being visited.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <typeparam name="TState">Type of the state.</typeparam>
    public static async Task OnVisit<T, TState>(
        this TState state,
        T node,
        CancellationToken cancellationToken
    ) where T : QueryNode where TState : IVisitorState
    {
        await state.OnEnter(node, cancellationToken);
        await state.OnExit(node, cancellationToken);
    }
}