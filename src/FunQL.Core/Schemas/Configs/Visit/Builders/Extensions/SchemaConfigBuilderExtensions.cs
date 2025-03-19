// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;

namespace FunQL.Core.Schemas.Configs.Visit.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableVisitConfigExtension"/> for <see cref="IMutableExtensibleConfig.GetExtensions"/>
    /// and returns <see cref="IVisitConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableVisitConfigExtension"/> for.</param>
    /// <returns>The <see cref="IVisitConfigBuilder"/> to build <see cref="IMutableVisitConfigExtension"/>.</returns>
    public static IVisitConfigBuilder VisitFeature(this ISchemaConfigBuilder builder)
    {
        const string name = IVisitConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableVisitConfigExtension>(name);
        if (config == null)
        {
            config = new MutableVisitConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        return new VisitConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableVisitConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/>, calling the <paramref name="action"/> to configure it and
    /// returns <see cref="ISchemaConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableVisitConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableVisitConfigExtension"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddVisitFeature(
        this ISchemaConfigBuilder builder,
        Action<IVisitConfigBuilder>? action = null
    )
    {
        var nestedBuilder = VisitFeature(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}