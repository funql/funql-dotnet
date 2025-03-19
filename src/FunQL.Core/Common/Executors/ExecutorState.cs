// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Processors;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Common.Executors;

/// <summary>Implementation of <see cref="IExecutorState"/>.</summary>
public class ExecutorState : ProcessorState<IExecuteContext>, IExecutorState
{
    /// <inheritdoc/>
    public Request? Request { get; set; }

    /// <inheritdoc/>
    public object? Data { get; set; }

    /// <inheritdoc/>
    public IDictionary<string, object>? Metadata { get; set; }
}