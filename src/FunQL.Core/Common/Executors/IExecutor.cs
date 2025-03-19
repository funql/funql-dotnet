// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Executors;

/// <summary>
/// Interface for an executor that can execute a FunQL request for a given <see cref="IExecutorState"/>.
/// </summary>
public interface IExecutor
{
    /// <summary>Executes the FunQL request for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the executor during execution.</param>
    /// <param name="cancellationToken">Token to cancel execution.</param>
    /// <returns>Task to await the execution.</returns>
    public Task Execute(IExecutorState state, CancellationToken cancellationToken);
}