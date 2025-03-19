// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Processors;

/// <summary>Abstract implementation of <see cref="IProcessorState{TContext}"/>.</summary>
/// <typeparam name="TContext">Type of the <see cref="IProcessContext"/>.</typeparam>
public abstract class ProcessorState<TContext> : IProcessorState<TContext> where TContext : class, IProcessContext
{
    /// <summary>Current list of contexts entered.</summary>
    private readonly List<TContext> _contexts = [];

    /// <inheritdoc/>
    public void EnterContext(TContext context)
    {
        _contexts.Add(context);
    }

    /// <inheritdoc/>
    public void ExitContext()
    {
        _contexts.RemoveAt(_contexts.Count - 1);
    }

    /// <inheritdoc/>
    public TContext? FindContext(Type type)
    {
        if (!typeof(TContext).IsAssignableFrom(type))
            throw new ArgumentException($"Type must implement {typeof(TContext).Name}", nameof(type));

        // Reverse lookup as we need to return the last added context
        for (var index = _contexts.Count - 1; index >= 0; index--)
        {
            var item = _contexts[index];
            if (type.IsInstanceOfType(item))
                return item;
        }

        return null;
    }
}