// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;

namespace FunQL.Core.Schemas.Executors.Parse;

/// <summary>
/// Context used by <see cref="ParseRequestForParametersExecutionHandler"/> in order to parse the request for given
/// parameters.
///
/// This can be used to parse a request for which its parameters are split up, e.g. when passed as query parameters for
/// REST calls.
/// </summary>
/// <param name="RequestName">Name of the request.</param>
/// <param name="Input">Value of <see cref="Inputting.Nodes.Input"/> parameter.</param>
/// <param name="Filter">Value of <see cref="Filtering.Nodes.Filter"/> parameter.</param>
/// <param name="Sort">Value of <see cref="Sorting.Nodes.Sort"/> parameter.</param>
/// <param name="Skip">Value of <see cref="Skipping.Nodes.Skip"/> parameter.</param>
/// <param name="Limit">Value of <see cref="Limiting.Nodes.Limit"/> parameter.</param>
/// <param name="Count">Value of <see cref="Counting.Nodes.Count"/> parameter.</param>
public sealed record ParseRequestForParametersExecuteContext(
    string RequestName,
    string? Input = null,
    string? Filter = null,
    string? Sort = null,
    string? Skip = null,
    string? Limit = null,
    string? Count = null
) : IExecuteContext;