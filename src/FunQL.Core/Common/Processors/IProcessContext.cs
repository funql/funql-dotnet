// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Processors;

/// <summary>
/// Marker interface for context that can be added to the <see cref="IProcessorState{TContext}"/> for a process
/// operation.
///
/// An example is when you have a <c>IConstantParser</c>, which needs external context to determine how to parse the
/// given input (like what is the expected <see cref="Type"/> of the output). You can then enter e.g. a
/// <c>ConstantContext(Type)</c>, pass the <see cref="IProcessorState{TContext}"/> to the parser so it can use the
/// context, and then exit the context when done.
/// </summary>
/// <seealso cref="IProcessorState{TContext}.EnterContext"/>
public interface IProcessContext;