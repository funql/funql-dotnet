// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Processors;

/// <summary>
/// State of a specific processor.
///
/// A processor is a component responsible for carrying out specific tasks or operations on data, for example a parser
/// or a visitor, both transforming input into output.
///
/// The state of a processor allows for components to update and keep track of the current process state. This also
/// allows for sharing a single state with multiple processors.
/// </summary>
/// <typeparam name="TContext">Type of the <see cref="IProcessContext"/>.</typeparam>
public interface IProcessorState<TContext> where TContext : IProcessContext
{
    /// <summary>
    /// Enter a given <paramref name="context"/>, which can be used by other processors using <see cref="FindContext"/>
    /// to get additional context when processing.
    ///
    /// Use <see cref="ExitContext"/> when leaving the context. 
    /// </summary>
    /// <param name="context">Context to enter.</param>
    public void EnterContext(TContext context);

    /// <summary>
    /// Exit the last entered <typeparamref name="TContext"/> that was added with <see cref="EnterContext"/>.
    /// </summary>
    public void ExitContext();

    /// <summary>
    /// Find the last added <typeparamref name="TContext"/> that is an instance of given <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Type of the context.</param>
    /// <returns>The <typeparamref name="TContext"/> or null if not found.</returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="type"/> not of type <typeparamref name="TContext"/>.
    /// </exception>
    public TContext? FindContext(Type type);
}