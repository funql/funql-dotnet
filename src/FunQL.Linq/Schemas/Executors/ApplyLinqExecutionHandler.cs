// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Counting.Nodes.Extensions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Schemas.Executors;
using FunQL.Core.Skipping.Nodes;
using FunQL.Core.Sorting.Nodes;
using FunQL.Linq.Schemas.Extensions;

namespace FunQL.Linq.Schemas.Executors;

/// <summary>
/// Execution handler that applies the <see cref="IExecutorState.Request"/> to
/// <see cref="ApplyLinqExecuteContext.Queryable"/> using LINQ. Enters <see cref="ExecuteLinqExecuteContext"/> with the
/// <see cref="IQueryable"/> for which LINQ is applied, so that the next handler can execute it. 
/// </summary>
/// <remarks>Requires <see cref="SchemaConfigExecuteContext"/> context.</remarks>
public sealed class ApplyLinqExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "FunQL.Linq.ApplyLinqExecutionHandler";
    
    /// <summary>Default order of this handler.</summary>
    /// <remarks>Should be called before <see cref="ExecuteLinqExecutionHandler"/>.</remarks>
    public const int DefaultOrder = ExecuteLinqExecutionHandler.DefaultOrder - 1_000;

    /// <inheritdoc/>
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        var queryable = state.FindContext<ApplyLinqExecuteContext>()?.Queryable;
        // Early return if no ApplyLinqExecuteContext set
        if (queryable == null)
        {
            await next(state, cancellationToken);
            return;
        }

        var request = state.Request ?? throw new InvalidOperationException();
        var requestName = request.Name;
        var schemaConfig = state.RequireContext<SchemaConfigExecuteContext>().SchemaConfig;
        var requestConfig = schemaConfig.FindRequestConfig(requestName)
            ?? throw new InvalidOperationException("No RequestConfig found for given name");

        // Apply Filter
        var filter = FindParameter<Filter>(request, requestConfig, Filter.FunctionName);
        if (filter != null)
            queryable = QueryableExtensions.ApplyFilter((dynamic)queryable, schemaConfig, requestName, filter);

        // Count must be executed over the filtered result set, without sort, skip or limit applied
        var countQueryable = queryable;

        // Apply Sort
        var sort = FindParameter<Sort>(request, requestConfig, Sort.FunctionName);
        if (sort != null)
            queryable = QueryableExtensions.ApplySort((dynamic)queryable, schemaConfig, requestName, sort, false);

        // Apply Skip
        var skip = FindParameter<Skip>(request, requestConfig, Skip.FunctionName);
        if (skip != null)
            queryable = QueryableExtensions.ApplySkip((dynamic)queryable, skip);

        // Apply Limit
        var limit = FindParameter<Limit>(request, requestConfig, Limit.FunctionName);
        if (limit != null)
            queryable = QueryableExtensions.ApplyLimit((dynamic)queryable, limit);

        // Clear Count queryable if no Count requested, so that count is not executed
        var count = FindParameter<Count>(request, requestConfig, Count.FunctionName);
        if (count?.Value() != true)
            countQueryable = null;

        // Enter ExecuteLinqExecuteContext for next handler to execute the LINQ request
        state.EnterContext(new ExecuteLinqExecuteContext(queryable, countQueryable));
        await next(state, cancellationToken);
        state.ExitContext();
    }

    /// <summary>
    /// Returns parameter for type <typeparamref name="T"/> if found in given <paramref name="request"/> or returns the
    /// <see cref="IParameterConfig.DefaultValue"/> set for given <paramref name="parameterName"/>, otherwise returns
    /// <c>null</c>.
    /// </summary>
    private static T? FindParameter<T>(
        Request request,
        IRequestConfig requestConfig,
        string parameterName
    ) where T : Parameter =>
        (T?)(request.Parameters.FirstOrDefault(it => it is T && it.Name == parameterName)
            ?? requestConfig.FindParameterConfig(parameterName)?.DefaultValue);
}