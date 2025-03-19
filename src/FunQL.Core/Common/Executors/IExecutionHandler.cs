// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Executors;

/// <summary>Interface for an execution handler, handling a specific step in an execution pipeline.</summary>
public interface IExecutionHandler
{
    /// <summary>
    /// Executes the handler's logic, potentially modifying the <paramref name="state"/> or controlling the flow of
    /// execution, and invokes <paramref name="next"/> delegate in the pipeline.
    /// </summary>
    /// <param name="state">State of the executor during execution.</param>
    /// <param name="next">
    /// The next executor delegate in the pipeline, representing the remaining steps to be executed.
    /// </param>
    /// <param name="cancellationToken">Token to cancel execution.</param>
    /// <returns>Task to await the execution.</returns>
    Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken);
}