// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Linq.Schemas.Extensions;
using WebApi.CustomResults;
using WebApi.EFCore;
using WebApi.FunQL;
using WebApi.Models;

namespace WebApi.Endpoints;

/// <summary>Handlers for the <see cref="Set"/> (<c>/sets</c>) endpoints.</summary>
public static class SetEndpoints
{
    /// <summary>Maps the <c>/sets</c> endpoints using given <paramref name="app"/>.</summary>
    /// <param name="app">Route builder to configure endpoints for.</param>
    /// <returns>The <paramref name="app"/> to continue building.</returns>
    public static IEndpointRouteBuilder MapSetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/sets", ListSets);

        return app;
    }

    /// <summary>Request handler to list all <see cref="Set"/>.</summary>
    /// <param name="filter">Optional filter query parameter.</param>
    /// <param name="sort">Optional sort query parameter.</param>
    /// <param name="skip">Optional skip query parameter.</param>
    /// <param name="limit">Optional limit query parameter.</param>
    /// <param name="count">Optional count query parameter.</param>
    /// <param name="context">Database to query data from.</param>
    /// <param name="schema">FunQL schema to execute query.</param>
    /// <param name="cancellationToken">Token to cancel async requests.</param>
    /// <returns>The <see cref="ListResponseResult{T}"/>.</returns>
    private static async Task<IResult> ListSets(
        string? filter,
        string? sort,
        string? skip,
        string? limit,
        string? count,
        ApiContext context, 
        ApiSchema schema, 
        CancellationToken cancellationToken
    )
    {
        var result = await context.Sets.ExecuteRequestForParameters(
            schema,
            // Execute the 'listSets()' request
            SetSchemaConfigurator.ListRequestName,
            filter: filter,
            sort: sort,
            skip: skip,
            limit: limit,
            count: count,
            cancellationToken: cancellationToken
        );

        return Results.Extensions.OkListResponse(result);
    }
}