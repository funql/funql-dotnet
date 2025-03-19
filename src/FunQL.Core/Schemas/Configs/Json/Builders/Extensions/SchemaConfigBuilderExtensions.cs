// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Json.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Json.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableJsonConfigExtension"/> for <see cref="IMutableExtensibleConfig.GetExtensions"/>
    /// and returns <see cref="IJsonConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableJsonConfigExtension"/> for.</param>
    /// <returns>The <see cref="IJsonConfigBuilder"/> to build <see cref="IMutableJsonConfigExtension"/>.</returns>
    public static IJsonConfigBuilder JsonConfig(this ISchemaConfigBuilder builder)
    {
        const string name = IJsonConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableJsonConfigExtension>(name);
        if (config == null)
        {
            config = new MutableJsonConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        return new JsonConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableJsonConfigExtension"/> for <see cref="IMutableExtensibleConfig.GetExtensions"/>,
    /// calling the <paramref name="action"/> to configure it and returns <see cref="ISchemaConfigBuilder"/> to continue
    /// building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableJsonConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableJsonConfigExtension"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder WithJsonConfig(
        this ISchemaConfigBuilder builder,
        Action<IJsonConfigBuilder>? action = null
    )
    {
        var nestedBuilder = JsonConfig(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}