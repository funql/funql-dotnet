// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections;
using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Counting.Executors.Extensions;

namespace FunQL.Linq.Schemas.Executors;

/// <summary>
/// Execution handler that executes the <see cref="ExecuteLinqExecuteContext.Queryable"/> to get the data for
/// <see cref="IExecutorState.Request"/>.
/// </summary>
/// <remarks>Requires <see cref="ExecuteLinqExecuteContext"/> context.</remarks>
public sealed class ExecuteLinqExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "FunQL.Linq.ExecuteLinqExecutionHandler";
    
    /// <summary>Default order of this handler.</summary>
    /// <remarks>Should be called late in the pipeline as executing is the last step.</remarks>
    public const int DefaultOrder = int.MaxValue - 1_000_000;

    /// <inheritdoc/>
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        var context = state.FindContext<ExecuteLinqExecuteContext>();
        // Early return if no ExecuteLinqExecuteContext set
        if (context == null)
        {
            await next(state, cancellationToken);
            return;
        }

        var queryable = context.Queryable;
        var countQueryable = context.CountQueryable;

        IList data;
        // Query the data async if supported
        if (queryable is IAsyncEnumerable<dynamic> asyncEnumerable)
        {
            var dataType = typeof(List<>).MakeGenericType(queryable.ElementType);
            data = (IList)Activator.CreateInstance(dataType)!;
            await foreach (var element in asyncEnumerable.WithCancellation(cancellationToken))
            {
                data.Add(element);
            }
        }
        else
        {
            // No async support, so just call ToList()
            data = Enumerable.ToList((dynamic)queryable);
        }

        state.Data = data;

        // Count the data if necessary
        if (countQueryable != null)
        {
            int totalCount = Queryable.Count((dynamic)countQueryable);
            state.SetTotalCount(totalCount);
        }

        // This is the last step that executes the request, so don't call next
    }
}