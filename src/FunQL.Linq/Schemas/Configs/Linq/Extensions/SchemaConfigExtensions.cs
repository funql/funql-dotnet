// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Linq.Schemas.Configs.Linq.Interfaces;

namespace FunQL.Linq.Schemas.Configs.Linq.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/>.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="ILinqConfigExtension"/> for <see cref="ILinqConfigExtension.DefaultName"/> or <c>null</c> if
    /// not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="ILinqConfigExtension"/> or <c>null</c> if not found.</returns>
    public static ILinqConfigExtension? FindLinqConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<ILinqConfigExtension>(ILinqConfigExtension.DefaultName);
}