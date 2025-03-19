// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Skipping.Nodes.Extensions;

/// <summary>Extensions for <see cref="Skip"/>.</summary>
public static class SkipExtensions
{
    /// <summary>
    /// Returns the <see cref="Skip.Constant"/> value as <see cref="int"/> or throws if value is not an
    /// <see cref="int"/>.
    /// </summary>
    /// <param name="skip">Node to get value for.</param>
    /// <returns>The skip value.</returns>
    /// <exception cref="InvalidOperationException">If value is not an <see cref="int"/>.</exception>
    public static int Value(this Skip skip) =>
        skip.Constant.Value as int? ?? throw new InvalidOperationException("Skip value must be an int");
}