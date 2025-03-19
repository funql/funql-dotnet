// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Parse.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;

namespace FunQL.Core.Schemas.Configs.Parse.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableParseConfigExtension"/> for <see cref="IMutableExtensibleConfig.GetExtensions"/>
    /// and returns <see cref="IParseConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParseConfigExtension"/> for.</param>
    /// <returns>The <see cref="IParseConfigBuilder"/> to build <see cref="IMutableParseConfigExtension"/>.</returns>
    public static IParseConfigBuilder ParseFeature(this ISchemaConfigBuilder builder)
    {
        const string name = IParseConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableParseConfigExtension>(name);
        if (config == null)
        {
            config = new MutableParseConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        return new ParseConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableParseConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/>, calling the <paramref name="action"/> to configure it and
    /// returns <see cref="ISchemaConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParseConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableParseConfigExtension"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddParseFeature(
        this ISchemaConfigBuilder builder,
        Action<IParseConfigBuilder>? action = null
    )
    {
        var nestedBuilder = ParseFeature(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}