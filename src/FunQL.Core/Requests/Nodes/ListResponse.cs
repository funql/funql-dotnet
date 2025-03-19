// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Requests.Nodes;

/// <summary>Represents a FunQL response containing a list of items and associated metadata.</summary>
/// <param name="Data">The list of items.</param>
/// <param name="Metadata">The response metadata.</param>
/// <typeparam name="T">Type of the items in the list.</typeparam>
/// <remarks>This record extends <see cref="Response{T}"/> to specifically handle lists.</remarks>
public record ListResponse<T>(
    List<T> Data,
    IReadOnlyDictionary<string, object> Metadata
) : Response<List<T>>(Data, Metadata);