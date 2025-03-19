// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Processors;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Common.Executors;

/// <summary>State of an executor.</summary>
public interface IExecutorState : IProcessorState<IExecuteContext>
{
    /// <summary>Request to execute or <c>null</c> if not yet parsed.</summary>
    public Request? Request { get; set; }

    /// <summary>Data that was retrieved for <see cref="Request"/>.</summary>
    public object? Data { get; set; }

    /// <summary>Extra metadata for <see cref="Request"/>.</summary>
    public IDictionary<string, object>? Metadata { get; set; }
}