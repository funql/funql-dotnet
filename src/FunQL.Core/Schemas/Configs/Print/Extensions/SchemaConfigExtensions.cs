// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Print.Interfaces;

namespace FunQL.Core.Schemas.Configs.Print.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/>.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IPrintConfigExtension"/> for <see cref="IPrintConfigExtension.DefaultName"/> or <c>null</c>
    /// if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="IPrintConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IPrintConfigExtension? FindPrintConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<IPrintConfigExtension>(IPrintConfigExtension.DefaultName);
}