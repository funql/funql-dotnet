// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Requests.Nodes;

namespace WebApi.CustomResults;

/// <summary>
/// Extensions for <see cref="IResultExtensions"/> so you can use them like <c>Results.Extensions.Xyz</c>.
/// </summary>
public static class ResultsExtensions
{
    /// <summary>
    /// Produces a <see cref="StatusCodes.Status200OK"/> response for given <paramref name="response"/>.
    /// </summary>
    /// <param name="_">Object to register this extension for.</param>
    /// <param name="response">The response to create HTTP response for.</param>
    /// <typeparam name="T">
    /// Type of the data in the list that will be serialized as JSON to the response body.
    /// </typeparam>
    /// <returns>The <see cref="IResult"/> for the response.</returns>
    /// <remarks>Use like <c>Results.Extensions.OkListResponse(response)</c>.</remarks>
    public static IResult OkListResponse<T>(this IResultExtensions _, ListResponse<T> response) => 
        new ListResponseResult<T>(response);
}