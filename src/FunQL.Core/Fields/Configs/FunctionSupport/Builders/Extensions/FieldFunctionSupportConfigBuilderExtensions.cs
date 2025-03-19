// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Interfaces;

namespace FunQL.Core.Fields.Configs.FunctionSupport.Builders.Extensions;

/// <summary>Extensions related to <see cref="IFieldFunctionSupportConfigBuilder"/>.</summary>
public static class FieldFunctionSupportConfigBuilderExtensions
{
    /// <summary>Configures whether the core <see cref="DateTime"/> field functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="DateTime"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static TBuilder SupportsDateTimeFieldFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFieldFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsYear(supported)
            .SupportsMonth(supported)
            .SupportsDay(supported)
            .SupportsHour(supported)
            .SupportsMinute(supported)
            .SupportsSecond(supported)
            .SupportsMillisecond(supported);

    /// <summary>Configures whether the core <see cref="string"/> field functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="string"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static TBuilder SupportsStringFieldFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFieldFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsLower(supported)
            .SupportsUpper(supported);

    /// <summary>Configures whether the core <see cref="double"/> field functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="double"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static TBuilder SupportsDoubleFieldFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFieldFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsFloor(supported)
            .SupportsCeiling(supported)
            .SupportsRound(supported);
}