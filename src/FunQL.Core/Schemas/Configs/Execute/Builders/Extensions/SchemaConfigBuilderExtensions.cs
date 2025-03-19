// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableExecuteConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/> and returns <see cref="IExecuteConfigBuilder"/> to
    /// configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableExecuteConfigExtension"/> for.</param>
    /// <param name="withCoreExecutionHandlers">
    /// Whether to add all core execution handlers as well. Default <c>true</c>.
    /// </param>
    /// <returns>
    /// The <see cref="IExecuteConfigBuilder"/> to build <see cref="IMutableExecuteConfigExtension"/>.
    /// </returns>
    public static IExecuteConfigBuilder ExecuteFeature(
        this ISchemaConfigBuilder builder, 
        bool withCoreExecutionHandlers = true
    )
    {
        const string name = IExecuteConfigExtension.DefaultName;
        var config = builder.MutableConfig.FindExtension<IMutableExecuteConfigExtension>(name);
        if (config == null)
        {
            config = new MutableExecuteConfigExtension(name);
            builder.MutableConfig.AddExtension(config);
        }
        
        var executeBuilder = new ExecuteConfigBuilder(config);

        if (withCoreExecutionHandlers)
            executeBuilder.WithCoreExecutionHandlers();

        return executeBuilder;
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableExecuteConfigExtension"/> for
    /// <see cref="IMutableExtensibleConfig.GetExtensions"/>, calling the <paramref name="action"/> to configure it and
    /// returns <see cref="ISchemaConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableExecuteConfigExtension"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableExecuteConfigExtension"/>.</param>
    /// <param name="withCoreExecutionHandlers">
    /// Whether to add all core execution handlers as well. Default <c>true</c>.
    /// </param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddExecuteFeature(
        this ISchemaConfigBuilder builder,
        Action<IExecuteConfigBuilder>? action = null,
        bool withCoreExecutionHandlers = true
    )
    {
        var nestedBuilder = ExecuteFeature(builder, withCoreExecutionHandlers);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}