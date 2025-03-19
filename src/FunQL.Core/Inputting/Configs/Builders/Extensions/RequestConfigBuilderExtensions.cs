// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Builders.Interfaces;
using FunQL.Core.Inputting.Configs.Extensions;

namespace FunQL.Core.Inputting.Configs.Builders.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfigBuilder"/>.</summary>
public static class RequestConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Nodes.Input"/> parameter and returns <see cref="IInputParameterConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <param name="type">Type of the <see cref="Nodes.Input"/> value.</param>
    /// <returns>
    /// The <see cref="IInputParameterConfigBuilder"/> to build <see cref="IMutableParameterConfig"/>.
    /// </returns>
    public static IInputParameterConfigBuilder Input(this IRequestConfigBuilder builder, Type type)
    {
        const string name = Nodes.Input.FunctionName;
        var config = builder.MutableConfig.FindParameterConfig(name);
        if (config == null)
        {
            config = new MutableParameterConfig(name);
            builder.MutableConfig.AddParameterConfig(config);
        }

        config.GetOrAddInputConfigExtension(type).TypeConfig.Type = type;

        return new InputParameterConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Nodes.Input"/> parameter, calling the <paramref name="action"/> to configure it and returns
    /// <see cref="IRequestConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <param name="type">Type of the <see cref="Nodes.Input"/> value.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableParameterConfig"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder SupportsInput(
        this IRequestConfigBuilder builder,
        Type type,
        Action<IInputParameterConfigBuilder>? action = null
    )
    {
        var nestedBuilder = Input(builder, type);
        action?.Invoke(nestedBuilder);
        return builder;
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Nodes.Input"/> parameter, calling the <paramref name="action"/> to configure it and returns
    /// <see cref="IRequestConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableParameterConfig"/>.</param>
    /// <typeparam name="T">Type of the <see cref="Nodes.Input"/> value.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder SupportsInput<T>(
        this IRequestConfigBuilder builder,
        Action<IInputParameterConfigBuilder>? action = null
    ) => builder.SupportsInput(typeof(T), action);
}