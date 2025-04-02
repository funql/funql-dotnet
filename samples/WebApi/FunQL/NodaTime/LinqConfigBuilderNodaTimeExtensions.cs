// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Linq.Schemas.Configs.Linq.Builders.Interfaces;

namespace WebApi.FunQL.NodaTime;

/// <summary>Extensions related to <see cref="ILinqConfigBuilder"/> and <see cref="NodaTime"/>.</summary>
public static class LinqConfigBuilderNodaTimeExtensions
{
    /// <summary>
    /// Adds the <see cref="InstantFunctionLinqTranslator"/> to given <paramref name="builder"/> if not yet added.
    /// </summary>
    /// <param name="builder">Builder to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public static ILinqConfigBuilder WithInstantFunctionLinqTranslator(
        this ILinqConfigBuilder builder
    )
    {
        // Early return if translator already added
        if (builder.MutableConfig.GetFieldFunctionLinqTranslators().Any(it => it is InstantFunctionLinqTranslator))
            return builder;
        
        builder.MutableConfig.AddFieldFunctionLinqTranslator(new InstantFunctionLinqTranslator());

        return builder;
    }
}