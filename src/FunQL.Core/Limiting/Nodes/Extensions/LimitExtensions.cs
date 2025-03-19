// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Limiting.Nodes.Extensions;

/// <summary>Extensions for <see cref="Limit"/>.</summary>
public static class LimitExtensions
{
    /// <summary>
    /// Returns the <see cref="Limit.Constant"/> value as <see cref="int"/> or throws if value is not an
    /// <see cref="int"/>.
    /// </summary>
    /// <param name="limit">Node to get value for.</param>
    /// <returns>The limit value.</returns>
    /// <exception cref="InvalidOperationException">If value is not an <see cref="int"/>.</exception>
    public static int Value(this Limit limit) =>
        limit.Constant.Value as int? ?? throw new InvalidOperationException("Limit value must be an int");
}