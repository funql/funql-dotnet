// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;

namespace FunQL.Core.Schemas.Extensions;

/// <summary>Extensions for <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>Applies the <paramref name="schemaConfigurator"/> to <paramref name="builder"/>.</summary>
    /// <param name="builder">Builder to apply configurator to.</param>
    /// <param name="schemaConfigurator">Configurator to apply.</param>
    /// <returns>Builder to continue building.</returns>
    public static ISchemaConfigBuilder ApplyConfigurator(
        this ISchemaConfigBuilder builder,
        ISchemaConfigurator schemaConfigurator
    )
    {
        schemaConfigurator.Configure(builder);
        return builder;
    }
}