// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Limiting.Nodes.Extensions;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Schemas;
using FunQL.Core.Schemas.Configs.Execute.Extensions;
using FunQL.Core.Schemas.Executors.Parse;
using FunQL.Core.Skipping.Nodes;
using FunQL.Core.Skipping.Nodes.Extensions;
using FunQL.Core.Sorting.Nodes;
using FunQL.Linq.Common.Visitors.Extensions;
using FunQL.Linq.Schemas.Configs.Linq.Extensions;
using FunQL.Linq.Schemas.Executors;
using FunQL.Linq.Sorting.Visitors;
using FunQL.Linq.Utils;

namespace FunQL.Linq.Schemas.Extensions;

/// <summary>Extensions related to <see cref="IQueryable{T}"/>.</summary>
public static class QueryableExtensions
{
    #region Filter

    /// <summary>
    /// Applies the <see cref="Filter"/> parameter to given <paramref name="queryable"/> using the configuration from
    /// <paramref name="schemaConfig"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply the <see cref="Filter"/> to.</param>
    /// <param name="schemaConfig">The schema configuration that defines the filter configuration.</param>
    /// <param name="requestName">The name of the request for which the filter is being applied.</param>
    /// <param name="filter">The <see cref="Filter"/> to apply.</param>
    /// <returns>The <see cref="IQueryable{T}"/> with the filter applied.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the <paramref name="schemaConfig"/> does not contain a valid request or LINQ configuration.
    /// </exception>
    public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> queryable,
        ISchemaConfig schemaConfig,
        string requestName,
        Filter filter
    )
    {
        // Get configs
        var requestConfig = schemaConfig.FindRequestConfig(requestName)
            ?? throw new InvalidOperationException("No RequestConfig found for given name");
        var linqConfig = schemaConfig.FindLinqConfigExtension()
            ?? throw new InvalidOperationException("No LINQ config found");

        // Prepare visitor
        var visitor = linqConfig.FilterLinqVisitorProvider(schemaConfig);
        var state = linqConfig.LinqVisitorStateFactory(schemaConfig, requestConfig);

        // Visit filter to generate LINQ expression
        /* Awaiting result synchronously as LINQ translations should be synchronous */
        visitor.Visit(filter, state, CancellationToken.None).GetAwaiter().GetResult();
        var expression = state.RequireResult();

        // Apply the LINQ expression
        return queryable.Where((Expression<Func<T, bool>>)expression);
    }

    #endregion

    #region Sort

    /// <summary>
    /// Applies the <see cref="Sort"/> parameter to given <paramref name="queryable"/> using the configuration from
    /// <paramref name="schemaConfig"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply the <see cref="Sort"/> to.</param>
    /// <param name="schemaConfig">The schema configuration that defines the sort configuration.</param>
    /// <param name="requestName">The name of the request for which the sort is being applied.</param>
    /// <param name="sort">The <see cref="Sort"/> to apply.</param>
    /// <param name="alreadyOrdered">
    /// Whether given <paramref name="queryable"/> is already ordered, which is required to determine which LINQ order
    /// method to call.
    /// </param>
    /// <returns>The <see cref="IQueryable{T}"/> with the sort applied.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the <paramref name="schemaConfig"/> does not contain a valid request or LINQ configuration.
    /// </exception>
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> queryable,
        ISchemaConfig schemaConfig,
        string requestName,
        Sort sort,
        bool alreadyOrdered
    )
    {
        // Get configs
        var requestConfig = schemaConfig.FindRequestConfig(requestName)
            ?? throw new InvalidOperationException("No RequestConfig found for given name");
        var linqConfig = schemaConfig.FindLinqConfigExtension()
            ?? throw new InvalidOperationException("No LINQ config found");

        // Prepare visitor
        var visitor = linqConfig.SortLinqVisitorProvider(schemaConfig);
        var state = linqConfig.SortLinqVisitorStateFactory(schemaConfig, requestConfig);

        // Visit sort to generate LINQ expression
        /* Awaiting result synchronously as LINQ translations should be synchronous */
        visitor.Visit(sort, state, CancellationToken.None).GetAwaiter().GetResult();
        var expressions = state.SortExpressions
            ?? throw new InvalidOperationException("SortExpressions should have been set");

        // Apply the LINQ expressions
        return queryable.ApplySort(expressions, alreadyOrdered);
    }

    /// <summary>Applies given <paramref name="sortLinqExpressions"/> to <paramref name="queryable"/>.</summary>
    private static IOrderedQueryable<T> ApplySort<T>(
        this IQueryable<T> queryable,
        IEnumerable<SortLinqExpression> sortLinqExpressions,
        bool alreadyOrdered
    )
    {
        var sorted = false;
        foreach (var sortLinqExpression in sortLinqExpressions)
        {
            queryable = queryable.ApplySort(sortLinqExpression, alreadyOrdered);
            // Ordered after first apply
            alreadyOrdered = true;
            sorted = true;
        }

        if (!sorted)
            throw new ArgumentException("No sort expressions given", nameof(sortLinqExpressions));

        return (IOrderedQueryable<T>)queryable;
    }

    /// <summary>Applies given <paramref name="sortLinqExpression"/> to <paramref name="queryable"/>.</summary>
    private static IOrderedQueryable<T> ApplySort<T>(
        this IQueryable<T> queryable,
        SortLinqExpression sortLinqExpression,
        bool alreadyOrdered
    )
    {
        var (keySelector, direction) = sortLinqExpression;
        IOrderedQueryable result;
        if (!alreadyOrdered)
        {
            result = direction == SortDirection.Ascending
                ? QueryableMethodInfoUtil.InvokeOrderBy(queryable, keySelector)
                : QueryableMethodInfoUtil.InvokeOrderByDescending(queryable, keySelector);
        }
        else
        {
            if (queryable is not IOrderedQueryable orderedQueryable)
                throw new ArgumentException("Queryable has not been ordered yet!", nameof(queryable));

            result = direction == SortDirection.Ascending
                ? QueryableMethodInfoUtil.InvokeThenBy(orderedQueryable, keySelector)
                : QueryableMethodInfoUtil.InvokeThenByDescending(orderedQueryable, keySelector);
        }

        return (IOrderedQueryable<T>)result;
    }

    #endregion

    #region Pagination

    /// <summary>Applies the <see cref="Skip"/> parameter to given <paramref name="queryable"/>.</summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply <see cref="Skip"/> to.</param>
    /// <param name="skip">The <see cref="Skip"/> to apply.</param>
    /// <returns>The <see cref="IQueryable{T}"/> with the skip operation applied.</returns>
    /// <seealso cref="Queryable.Skip{T}(IQueryable{T},int)"/>
    public static IQueryable<T> ApplySkip<T>(this IQueryable<T> queryable, Skip skip) =>
        queryable.Skip(skip.Value());

    /// <summary>Applies the <see cref="Limit"/> parameter to given <paramref name="queryable"/>.</summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply <see cref="Limit"/> to.</param>
    /// <param name="limit">The <see cref="Limit"/> to apply.</param>
    /// <returns>The <see cref="IQueryable{T}"/> with the limit operation applied.</returns>
    /// <seealso cref="Queryable.Take{T}(IQueryable{T},int)"/>
    public static IQueryable<T> ApplyLimit<T>(this IQueryable<T> queryable, Limit limit) =>
        queryable.Take(limit.Value());

    #endregion

    #region Request

    /// <summary>
    /// Applies any <see cref="Request.Parameters"/> of given <paramref name="request"/> that can be applied to
    /// <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply request to.</param>
    /// <param name="schemaConfig">The schema configuration that defines the LINQ configuration.</param>
    /// <param name="request">The request to apply parameters for.</param>
    /// <returns>The <see cref="IQueryable{T}"/> with the parameters applied.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the <paramref name="schemaConfig"/> is not configured properly.
    /// </exception>
    /// <remarks>
    /// This will apply <see cref="Filter"/>, <see cref="Sort"/>, <see cref="Skip"/> and <see cref="Limit"/>. Use
    /// <see cref="ExecuteRequest{T}(System.Linq.IQueryable{T},FunQL.Core.Configs.Interfaces.ISchemaConfig,string,System.Action{FunQL.Core.Common.Executors.IExecutorState}?,System.Threading.CancellationToken)"/>
    /// where possible to make sure all execution steps are executed and all parameters are handled.
    /// </remarks>
    public static IQueryable<T> ApplyRequest<T>(
        this IQueryable<T> queryable,
        ISchemaConfig schemaConfig,
        Request request
    )
    {
        var requestName = request.Name;
        var requestConfig = schemaConfig.FindRequestConfig(requestName)
            ?? throw new InvalidOperationException($"No RequestConfig found for request '{requestName}'");
        
        // Apply Filter
        var filter = FindParameter<Filter>(request, requestConfig, Filter.FunctionName);
        if (filter != null)
            queryable = queryable.ApplyFilter(schemaConfig, requestName, filter);
        
        // Apply Sort
        var sort = FindParameter<Sort>(request, requestConfig, Sort.FunctionName);
        if (sort != null)
            queryable = queryable.ApplySort(schemaConfig, requestName, sort, false);

        // Apply Skip
        var skip = FindParameter<Skip>(request, requestConfig, Skip.FunctionName);
        if (skip != null)
            queryable = queryable.ApplySkip(skip);
        
        // Apply Limit
        var limit = FindParameter<Limit>(request, requestConfig, Limit.FunctionName);
        if (limit != null)
            queryable = queryable.ApplyLimit(limit);

        return queryable;
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

    /// <summary>
    /// Applies any <see cref="Request.Parameters"/> of given <paramref name="request"/> that can be applied to
    /// <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to apply request to.</param>
    /// <param name="schema">The schema that defines the LINQ configuration.</param>
    /// <param name="request">The request to apply parameters for.</param>
    /// <returns>The <see cref="IQueryable{T}"/> with the parameters applied.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the <paramref name="schema"/> is not configured properly.
    /// </exception>
    /// <remarks>
    /// This will apply <see cref="Filter"/>, <see cref="Sort"/>, <see cref="Skip"/> and <see cref="Limit"/>. Use
    /// <see cref="ExecuteRequest{T}(System.Linq.IQueryable{T},FunQL.Core.Configs.Interfaces.ISchemaConfig,string,System.Action{FunQL.Core.Common.Executors.IExecutorState}?,System.Threading.CancellationToken)"/>
    /// where possible to make sure all execution steps are executed and all parameters are handled.
    /// </remarks>
    public static IQueryable<T> ApplyRequest<T>(
        this IQueryable<T> queryable,
        Schema schema,
        Request request
    ) => queryable.ApplyRequest(schema.SchemaConfig, request);

    #endregion

    #region Execute

    /// <summary>
    /// Executes the <see cref="Request"/> represented by given <paramref name="request"/> for the data of given
    /// <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to execute the request on.</param>
    /// <param name="schemaConfig">The schema configuration defining the execution steps.</param>
    /// <param name="request">The text representation of the <see cref="Request"/> to be executed.</param>
    /// <param name="stateAction">Optional action to modify the <see cref="IExecutorState"/> before execution.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>A <see cref="ListResponse{T}"/> containing the query results and any associated metadata.</returns>
    /// <exception cref="InvalidOperationException">If the required execute config is not found.</exception>
    public static async Task<ListResponse<T>> ExecuteRequest<T>(
        this IQueryable<T> queryable,
        ISchemaConfig schemaConfig,
        string request,
        Action<IExecutorState>? stateAction = null,
        CancellationToken cancellationToken = default
    )
    {
        // Get configs
        var config = schemaConfig.FindExecuteConfigExtension()
            ?? throw new InvalidOperationException("No ExecuteConfigExtension found");

        // Prepare executor
        var executor = config.ExecutorProvider(schemaConfig);
        var state = config.ExecutorStateFactory(schemaConfig);
        state.EnterContext(new ParseRequestExecuteContext(request));
        state.EnterContext(new ApplyLinqExecuteContext(queryable));
        stateAction?.Invoke(state);

        // Execute the request
        await executor.Execute(state, cancellationToken);

        // Return the result
        var data = state.Data as List<T> ?? throw new InvalidOperationException("No result set");
        var metadata = state.Metadata ?? new Dictionary<string, object>();
        return new ListResponse<T>(data, metadata.AsReadOnly());
    }

    /// <summary>
    /// Executes the <see cref="Request"/> represented by given <paramref name="request"/> for the data of given
    /// <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to execute the request on.</param>
    /// <param name="schema">The schema defining the execution steps.</param>
    /// <param name="request">The text representation of the <see cref="Request"/> to be executed.</param>
    /// <param name="stateAction">Optional action to modify the <see cref="IExecutorState"/> before execution.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>A <see cref="ListResponse{T}"/> containing the query results and any associated metadata.</returns>
    /// <exception cref="InvalidOperationException">If the required execute config is not found.</exception>
    public static Task<ListResponse<T>> ExecuteRequest<T>(
        this IQueryable<T> queryable,
        Schema schema,
        string request,
        Action<IExecutorState>? stateAction = null,
        CancellationToken cancellationToken = default
    ) => queryable.ExecuteRequest(schema.SchemaConfig, request, stateAction, cancellationToken);

    /// <summary>
    /// Executes the <see cref="Request"/> represented by given <paramref name="requestName"/> and given parameters for
    /// the data of given <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to execute the request on.</param>
    /// <param name="schemaConfig">The schema configuration defining the execution steps.</param>
    /// <param name="requestName">The name of the request being executed.</param>
    /// <param name="input">Optional input constant to include in the request.</param>
    /// <param name="filter">Optional filter expression to apply to the query.</param>
    /// <param name="sort">Optional sort expressions to apply to the query.</param>
    /// <param name="skip">Optional skip constant for pagination.</param>
    /// <param name="limit">Optional limit constant for pagination.</param>
    /// <param name="count">Optional count constant for the request.</param>
    /// <param name="stateAction">Optional action to modify the <see cref="IExecutorState"/> before execution.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>A <see cref="ListResponse{T}"/> containing the query results and any associated metadata.</returns>
    /// <exception cref="InvalidOperationException">If the required execute config is not found.</exception>
    public static async Task<ListResponse<T>> ExecuteRequestForParameters<T>(
        this IQueryable<T> queryable,
        ISchemaConfig schemaConfig,
        string requestName,
        string? input = null,
        string? filter = null,
        string? sort = null,
        string? skip = null,
        string? limit = null,
        string? count = null,
        Action<IExecutorState>? stateAction = null,
        CancellationToken cancellationToken = default
    )
    {
        // Get configs
        var config = schemaConfig.FindExecuteConfigExtension()
            ?? throw new InvalidOperationException("No ExecuteConfigExtension found");

        // Prepare executor
        var executor = config.ExecutorProvider(schemaConfig);
        var state = config.ExecutorStateFactory(schemaConfig);
        state.EnterContext(new ParseRequestForParametersExecuteContext(
            requestName, input, filter, sort, skip, limit, count
        ));
        state.EnterContext(new ApplyLinqExecuteContext(queryable));
        stateAction?.Invoke(state);

        // Execute the request
        await executor.Execute(state, cancellationToken);

        // Return the result
        var data = state.Data as List<T> ?? throw new InvalidOperationException("No result set");
        var metadata = state.Metadata ?? new Dictionary<string, object>();
        return new ListResponse<T>(data, metadata.AsReadOnly());
    }
    
    /// <summary>
    /// Executes the <see cref="Request"/> represented by given <paramref name="requestName"/> and given parameters for
    /// the data of given <paramref name="queryable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queryable.</typeparam>
    /// <param name="queryable">The queryable to execute the request on.</param>
    /// <param name="schema">The schema defining the execution steps.</param>
    /// <param name="requestName">The name of the request being executed.</param>
    /// <param name="input">Optional input constant to include in the request.</param>
    /// <param name="filter">Optional filter expression to apply to the query.</param>
    /// <param name="sort">Optional sort expressions to apply to the query.</param>
    /// <param name="skip">Optional skip constant for pagination.</param>
    /// <param name="limit">Optional limit constant for pagination.</param>
    /// <param name="count">Optional count constant for the request.</param>
    /// <param name="stateAction">Optional action to modify the <see cref="IExecutorState"/> before execution.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <returns>A <see cref="ListResponse{T}"/> containing the query results and any associated metadata.</returns>
    /// <exception cref="InvalidOperationException">If the required execute config is not found.</exception>
    public static Task<ListResponse<T>> ExecuteRequestForParameters<T>(
        this IQueryable<T> queryable,
        Schema schema,
        string requestName,
        string? input = null,
        string? filter = null,
        string? sort = null,
        string? skip = null,
        string? limit = null,
        string? count = null,
        Action<IExecutorState>? stateAction = null,
        CancellationToken cancellationToken = default
    ) => queryable.ExecuteRequestForParameters(
        schema.SchemaConfig, requestName, input, filter, sort, skip, limit, count, stateAction, cancellationToken
    );

    #endregion
}