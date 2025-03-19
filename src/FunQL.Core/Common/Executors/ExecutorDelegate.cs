// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Executors;

/// <summary>The <see cref="IExecutor.Execute"/> method defined as a delegate.</summary>
/// <param name="state">State of the executor during execution.</param>
/// <param name="cancellationToken">Token to cancel execution.</param>
/// <returns>Task to await the execution.</returns>
public delegate Task ExecutorDelegate(IExecutorState state, CancellationToken cancellationToken);