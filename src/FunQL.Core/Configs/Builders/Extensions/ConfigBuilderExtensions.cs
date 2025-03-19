// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;

namespace FunQL.Core.Configs.Builders.Extensions;

/// <summary>Extensions related to <see cref="IConfigBuilder"/>.</summary>
public static class ConfigBuilderExtensions
{
    /// <summary>
    /// Calls the given <paramref name="action"/> to configure the <paramref name="builder"/> and returns the
    /// <paramref name="builder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to call action on.</param>
    /// <param name="action">Action to call.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The <paramref name="builder"/> to continue building.</returns>
    public static TBuilder Configure<TBuilder>(this TBuilder builder, Action<TBuilder> action)
        where TBuilder : IConfigBuilder
    {
        action(builder);
        return builder;
    }
}