// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Executors;

/// <summary>
/// Marker interface for context that can be added to the <see cref="IExecutorState"/> for an execute operation.
/// </summary>
/// <seealso cref="IProcessorState{T}.EnterContext"/>
public interface IExecuteContext : IProcessContext;