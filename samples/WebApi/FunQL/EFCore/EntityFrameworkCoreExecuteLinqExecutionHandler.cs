// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Counting.Executors.Extensions;
using FunQL.Linq.Schemas.Executors;
using Microsoft.EntityFrameworkCore;

namespace WebApi.FunQL.EFCore;

/// <summary>
/// Execution handler that executes the <see cref="ExecuteLinqExecuteContext.Queryable"/> to get the data for
/// <see cref="IExecutorState.Request"/>.
/// <para>
/// This handler replaces the <see cref="ExecuteLinqExecutionHandler"/> so it can use the async EFCore methods instead:
/// <list type="bullet">
/// <item>
/// <description>
///   <see cref="EntityFrameworkQueryableExtensions.ToListAsync{T}(IQueryable{T},CancellationToken)"/> to get the data
/// </description>
/// </item>
/// <item>
/// <description>
///   <see cref="EntityFrameworkQueryableExtensions.CountAsync{T}(IQueryable{T},CancellationToken)"/> to count the items
/// </description>
/// </item>
/// </list>
/// </para>
/// </summary>
/// <remarks>
/// Requires <see cref="ExecuteLinqExecuteContext"/> context, just like <see cref="ExecuteLinqExecutionHandler"/>.
///
/// <para>
/// Note that <see cref="ExecuteLinqExecutionHandler"/> already handles <see cref="IAsyncEnumerable{T}"/> the same or
/// similar to how <see cref="EntityFrameworkQueryableExtensions.ToListAsync{T}(IQueryable{T},CancellationToken)"/>
/// handles it. Only the <see cref="EntityFrameworkQueryableExtensions.CountAsync{T}(IQueryable{T},CancellationToken)"/>
/// is different as there's no abstract way to count asynchronously; this is a specific implementation in EFCore.
/// </para>
/// </remarks>
public class EntityFrameworkCoreExecuteLinqExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "WebApi.EntityFrameworkCoreExecuteLinqExecutionHandler";
    
    // Handler should be called late in the pipeline as LINQ does the data fetching, which is pretty much at the end
    /// <summary>Default order of this handler.</summary>
    /// <remarks>
    /// Should be called before <see cref="ExecuteLinqExecutionHandler"/> so this handler can take over the execution.
    /// </remarks>
    public const int DefaultOrder = ExecuteLinqExecutionHandler.DefaultOrder - 100;

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

        // Query the data
        state.Data = await EntityFrameworkQueryableExtensions.ToListAsync((dynamic)queryable, cancellationToken);

        // Count the data if necessary
        if (countQueryable != null)
        {
            int totalCount = await EntityFrameworkQueryableExtensions.CountAsync(
                (dynamic)countQueryable,
                cancellationToken
            );
            state.SetTotalCount(totalCount);
        }
        
        // This is the last step that executes the request, so don't call next
    }
}