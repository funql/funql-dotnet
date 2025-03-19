// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Skipping.Configs.Builders.Interfaces;

namespace FunQL.Core.Skipping.Configs.Builders.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfigBuilder"/>.</summary>
public static class RequestConfigBuilderExtensions
{
    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Nodes.Skip"/> parameter and returns <see cref="ISkipParameterConfigBuilder"/> to configure
    /// it.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <returns>
    /// The <see cref="ISkipParameterConfigBuilder"/> to build <see cref="IMutableParameterConfig"/>.
    /// </returns>
    public static ISkipParameterConfigBuilder Skip(this IRequestConfigBuilder builder)
    {
        const string name = Nodes.Skip.FunctionName;
        var config = builder.MutableConfig.FindParameterConfig(name);
        if (config == null)
        {
            config = new MutableParameterConfig(name);
            builder.MutableConfig.AddParameterConfig(config);
        }

        return new SkipParameterConfigBuilder(config);
    }


    /// <summary>
    /// Gets or adds <see cref="IMutableParameterConfig"/> for <see cref="IMutableRequestConfig.GetParameterConfigs"/>
    /// for the <see cref="Nodes.Skip"/> parameter, calling the <paramref name="action"/> to configure it and returns
    /// <see cref="IRequestConfigBuilder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="IMutableParameterConfig"/> for.</param>
    /// <param name="action">Optional action to configure <see cref="IMutableParameterConfig"/>.</param>
    /// <returns>The builder to continue building.</returns>
    public static IRequestConfigBuilder SupportsSkip(
        this IRequestConfigBuilder builder,
        Action<ISkipParameterConfigBuilder>? action = null
    )
    {
        var nestedBuilder = Skip(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}