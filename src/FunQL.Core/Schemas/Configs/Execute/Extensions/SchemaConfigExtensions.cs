// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/>.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IExecuteConfigExtension"/> for <see cref="IExecuteConfigExtension.DefaultName"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="IExecuteConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IExecuteConfigExtension? FindExecuteConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<IExecuteConfigExtension>(IExecuteConfigExtension.DefaultName);
}