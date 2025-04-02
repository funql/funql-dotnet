// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;
using FunQL.Core.Counting.Executors;
using FunQL.Core.Requests.Nodes;
using Microsoft.AspNetCore.Http.Metadata;

namespace WebApi.CustomResults;

/// <summary>
/// An <see cref="IResult"/> that on execution will write the <see cref="ListResponse{T}.Data"/> to the response with
/// the <see cref="StatusCodes.Status200OK"/> status code and writes known <see cref="ListResponse{T}.Metadata"/> as
/// headers.
/// </summary>
/// <param name="response">The response to write to the response.</param>
/// <typeparam name="T">Type of the data in the list that will be serialized as JSON to the response body.</typeparam>
/// <remarks>Writes <see cref="CountMetadataConstants.MetadataKey"/> if available.</remarks>
public sealed class ListResponseResult<T>(
    ListResponse<T> response
) : IResult, IEndpointMetadataProvider, IStatusCodeHttpResult, IValueHttpResult, IValueHttpResult<List<T>>
{
    /// <summary>Content type of data returned.</summary>
    private const string ContentType = "application/json";
    /// <summary>Header key used for the count metadata.</summary>
    private const string TotalCountHeaderKey = "Total-Count";
    
    /// <summary>The HTTP status code: <see cref="StatusCodes.Status200OK"/>.</summary>
    public int StatusCode => StatusCodes.Status200OK;

    /// <inheritdoc/>
    int? IStatusCodeHttpResult.StatusCode => StatusCode;

    /// <inheritdoc/>
    public List<T> Value { get; } = response.Data;
    
    /// <inheritdoc/>
    object IValueHttpResult.Value => Value;
    
    /// <inheritdoc/>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCode;

        if (response.Metadata.TryGetValue(CountMetadataConstants.MetadataKey, out var count))
            httpContext.Response.Headers[TotalCountHeaderKey] = count.ToString();

        return httpContext.Response.WriteAsJsonAsync(response.Data);
    }

    /// <inheritdoc/>
    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(
            new ProducesResponseTypeMetadata(StatusCodes.Status200OK, typeof(List<T>), [ContentType])
        );
    }
}