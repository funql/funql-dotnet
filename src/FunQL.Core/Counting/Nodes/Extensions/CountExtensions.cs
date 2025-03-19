// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Counting.Nodes.Extensions;

/// <summary>Extensions for <see cref="Count"/>.</summary>
public static class CountExtensions
{
    /// <summary>
    /// Returns the <see cref="Count.Constant"/> value as <see cref="bool"/> or throws if value is not a
    /// <see cref="bool"/>.
    /// </summary>
    /// <param name="count">Node to get value for.</param>
    /// <returns>The count value.</returns>
    /// <exception cref="InvalidOperationException">If value is not a <see cref="bool"/>.</exception>
    public static bool Value(this Count count) =>
        count.Constant.Value as bool? ?? throw new InvalidOperationException("Count value must be a bool");
}