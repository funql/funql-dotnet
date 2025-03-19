// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Configs.Builders.Interfaces;

namespace FunQL.Core.Filtering.Configs.Builders.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfigBuilder"/>.</summary>
public static class RequestConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Filter"/> parameter and returns <see cref="IFilterParameterConfigBuilder"/> to configure it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <returns>
    /// The <see cref="IFilterParameterConfigBuilder"/> to build <see cref="IMutableParameterConfig"/>.
    /// </returns>
    public static IFilterParameterConfigBuilder Filter(this IRequestConfigBuilder builder)
    {
        const string name = Nodes.Filter.FunctionName;
        var config = builder.MutableConfig.FindParameterConfig(name);
        if (config == null)
        {
            config = new MutableParameterConfig(name);
            builder.MutableConfig.AddParameterConfig(config);
        }

        return new FilterParameterConfigBuilder(config);
    }

    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Filter"/> parameter, calling the <paramref name="action"/> to configure it and returns
    /// <see cref="IRequestConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableParameterConfig"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder SupportsFilter(
        this IRequestConfigBuilder builder,
        Action<IFilterParameterConfigBuilder>? action = null
    )
    {
        var nestedBuilder = Filter(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}