// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;

namespace FunQL.Core.Counting.Executors.Extensions;

/// <summary>Extensions for <see cref="IExecutorState"/> related to count.</summary>
public static class ExecutorStateExtensions
{
    /// <summary>Get the total count metadata value.</summary>
    /// <param name="state">State to get metadata from.</param>
    /// <returns>The count metadata value.</returns>
    /// <exception cref="InvalidOperationException">If the value is not of expected type <see cref="int"/>.</exception>
    public static int? GetTotalCount(this IExecutorState state)
    {
        object? totalCountObject = null;
        var found = state.Metadata?.TryGetValue(CountMetadataConstants.MetadataKey, out totalCountObject);
        if (found == true && totalCountObject is not int)
            throw new InvalidOperationException(
                $"Object for key {CountMetadataConstants.MetadataKey} expected to be of of type int, but was of type " +
                $"${totalCountObject?.GetType()}"
            );

        return (int?)totalCountObject;
    }

    /// <summary>Sets the total count metadata value.</summary>
    /// <param name="state">State to set metadata on.</param>
    /// <param name="totalCount">The value to set.</param>
    public static void SetTotalCount(this IExecutorState state, int totalCount)
    {
        state.Metadata ??= new Dictionary<string, object>();
        state.Metadata[CountMetadataConstants.MetadataKey] = totalCount;
    }
}