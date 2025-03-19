// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Requests.Nodes;

/// <summary>Represents a generic FunQL response containing data and associated metadata.</summary>
/// <param name="Data">The response data.</param>
/// <param name="Metadata">The response metadata.</param>
/// <typeparam name="T">Type of the response data.</typeparam>
public record Response<T>(
    T Data,
    IReadOnlyDictionary<string, object> Metadata
);