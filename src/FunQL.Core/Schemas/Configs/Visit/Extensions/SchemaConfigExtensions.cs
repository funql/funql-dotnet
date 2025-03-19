// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Schemas.Configs.Visit.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/>.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IVisitConfigExtension"/> for <see cref="IVisitConfigExtension.DefaultName"/> or <c>null</c>
    /// if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="IVisitConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IVisitConfigExtension? FindVisitConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<IVisitConfigExtension>(IVisitConfigExtension.DefaultName);
}