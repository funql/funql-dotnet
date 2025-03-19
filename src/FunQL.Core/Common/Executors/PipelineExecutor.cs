// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Executors;

/// <summary>
/// Implementation of <see cref="IExecutor"/> that allows for executing a pipeline of execution steps.
/// </summary>
/// <param name="executionSteps">The execution steps to execute.</param>
public class PipelineExecutor(IEnumerable<ExecutionStep> executionSteps) : IExecutor
{
    /// <summary>The delegate that calls the pipeline execution steps.</summary>
    private readonly ExecutorDelegate _executor = BuildExecutor(executionSteps);

    /// <summary>Executes the pipeline of execution steps using the provided executor state.</summary>
    /// <inheritdoc/>
    public Task Execute(IExecutorState state, CancellationToken cancellationToken)
    {
        return _executor(state, cancellationToken);
    }

    /// <summary>
    /// Builds the <see cref="ExecutorDelegate"/> by chaining together execution steps in the specified order.
    /// </summary>
    /// <param name="executionSteps">The execution steps to execute.</param>
    /// <returns>The <see cref="ExecutorDelegate"/> representing the composed pipeline.</returns>
    private static ExecutorDelegate BuildExecutor(IEnumerable<ExecutionStep> executionSteps)
    {
        ExecutorDelegate executor = CoreExecutor;

        // Sort by Order in descending order so that lowest Order is executed first (as lowest Order is added last in
        // execution pipeline)
        foreach (var step in executionSteps.OrderByDescending(it => it.Order))
        {
            executor = step.ExecutorProvider(executor);
        }

        return executor;
    }

    /// <summary>The core executor delegate representing the final step in the pipeline.</summary>
    /// <param name="state">State of the executor during execution.</param>
    /// <param name="cancellationToken">Token to cancel execution.</param>
    /// <returns>Task to await the execution.</returns>
    private static Task CoreExecutor(IExecutorState state, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException(
            "The last step is called, meaning the request was not executed properly. Please make sure to return a result in the pipeline.");
    }
}