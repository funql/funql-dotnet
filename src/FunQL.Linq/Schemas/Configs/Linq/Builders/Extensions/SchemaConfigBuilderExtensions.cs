// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Execute.Extensions;
using FunQL.Linq.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Linq.Schemas.Configs.Linq.Builders.Interfaces;
using FunQL.Linq.Schemas.Configs.Linq.Interfaces;

namespace FunQL.Linq.Schemas.Configs.Linq.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableLinqConfigExtension"/> for <see cref="IExtensibleConfig.GetExtensions"/> and
    /// returns <see cref="ILinqConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableLinqConfigExtension"/> for.</param>
    /// <param name="withLinqExecutionHandlers">
    /// Whether to add all LINQ execution handlers as well. Default <c>true</c>.
    /// </param>
    /// <returns>
    /// The <see cref="ILinqConfigBuilder"/> to build <see cref="IMutableLinqConfigExtension"/>.
    /// </returns>
    public static ILinqConfigBuilder LinqFeature(
        this ISchemaConfigBuilder builder,
        bool withLinqExecutionHandlers = true
    )
    {
        const string name = ILinqConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableLinqConfigExtension>(name);
        if (config == null)
        {
            config = new MutableLinqConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }

        // Add LINQ execution handlers only if the ExecuteConfigExtension is configured
        if (withLinqExecutionHandlers && builder.MutableConfig.FindExecuteConfigExtension() != null)
            builder.ExecuteFeature().WithLinqExecutionHandlers();

        return new LinqConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableLinqConfigExtension"/> for <see cref="IExtensibleConfig.GetExtensions"/>,
    /// calling the <paramref name="action"/> to configure it and returns <see cref="ISchemaConfigBuilder"/> to continue
    /// building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableLinqConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableLinqConfigExtension"/>.</param>
    /// <param name="withLinqExecutionHandlers">
    /// Whether to add all LINQ execution handlers as well. Default <c>true</c>.
    /// </param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddLinqFeature(
        this ISchemaConfigBuilder builder,
        Action<ILinqConfigBuilder>? action = null,
        bool withLinqExecutionHandlers = true
    )
    {
        var nestedBuilder = LinqFeature(builder, withLinqExecutionHandlers);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}