// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Print.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Print.Interfaces;

namespace FunQL.Core.Schemas.Configs.Print.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutablePrintConfigExtension"/> for <see cref="IMutableExtensibleConfig.GetExtensions"/>
    /// and returns <see cref="IPrintConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutablePrintConfigExtension"/> for.</param>
    /// <returns>The <see cref="IPrintConfigBuilder"/> to build <see cref="IMutablePrintConfigExtension"/>.</returns>
    public static IPrintConfigBuilder PrintFeature(this ISchemaConfigBuilder builder)
    {
        const string name = IPrintConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutablePrintConfigExtension>(name);
        if (config == null)
        {
            config = new MutablePrintConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        return new PrintConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutablePrintConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/>, calling the <paramref name="action"/> to configure it and
    /// returns <see cref="ISchemaConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutablePrintConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutablePrintConfigExtension"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddPrintFeature(
        this ISchemaConfigBuilder builder,
        Action<IPrintConfigBuilder>? action = null
    )
    {
        var nestedBuilder = PrintFeature(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}