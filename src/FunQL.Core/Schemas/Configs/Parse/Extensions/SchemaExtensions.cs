// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Schemas.Configs.Parse.Extensions;

/// <summary>Extensions related to <see cref="Schema"/> and parsing requests.</summary>
public static class SchemaExtensions
{
    /// <summary>Parses given <paramref name="request"/>.</summary>
    /// <param name="schema">The schema to parse request with.</param>
    /// <param name="request">The FunQL request text to parse.</param>
    /// <returns>A <see cref="Request"/> representing the parsed request.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no parse configuration is found.</exception>
    public static Request ParseRequest(this Schema schema, string request) =>
        schema.SchemaConfig.ParseRequest(request);
    
    /// <summary>Parses a <see cref="Request"/> with given <paramref name="requestName"/> for given parameters.</summary>
    /// <param name="schema">The schema to parse request with.</param>
    /// <param name="requestName">The name of the request.</param>
    /// <param name="input">The input parameter to parse.</param>
    /// <param name="filter">The filter parameter to parse.</param>
    /// <param name="sort">The sort parameter to parse.</param>
    /// <param name="skip">The skip parameter to parse.</param>
    /// <param name="limit">The limit parameter to parse.</param>
    /// <param name="count">The count parameter to parse.</param>
    /// <returns>A <see cref="Request"/> containing the parsed components.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no parse configuration is found.</exception>
    /// <remarks>
    /// This is useful for e.g. REST APIs, where each FunQL parameter is passed as a separate query parameter. This will
    /// parse each separate parameter and create a <see cref="Request"/> out of it.
    /// </remarks>
    public static Request ParseRequestForParameters(
        this Schema schema,
        string requestName,
        string? input = null,
        string? filter = null,
        string? sort = null,
        string? skip = null,
        string? limit = null,
        string? count = null
    ) => schema.SchemaConfig.ParseRequestForParameters(requestName, input, filter, sort, skip, limit, count);
}