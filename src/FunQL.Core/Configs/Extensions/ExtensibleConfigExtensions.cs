// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Extensions;

/// <summary>Extensions related to <see cref="IExtensibleConfig"/>.</summary>
public static class ExtensibleConfigExtensions
{
    /// <summary>
    /// Finds <see cref="IConfigExtension"/> for given <paramref name="name"/> and casts it to type
    /// <typeparamref name="T"/> if possible.
    /// </summary>
    /// <param name="config">Config to find extension.</param>
    /// <param name="name">Name of the extension.</param>
    /// <typeparam name="T">Type of the extension to cast to.</typeparam>
    /// <returns>
    /// The extension cast to <typeparamref name="T"/> if found; otherwise <c>null</c> if not found or not assignable to
    /// <typeparamref name="T"/>.
    /// </returns>
    public static T? FindExtension<T>(this IExtensibleConfig config, string name) where T : class, IConfigExtension =>
        config.FindExtension(name) as T;
}