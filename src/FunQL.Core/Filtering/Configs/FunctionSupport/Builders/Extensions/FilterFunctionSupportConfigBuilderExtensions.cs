// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Interfaces;

namespace FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Extensions;

/// <summary>Extensions related to <see cref="IFilterFunctionSupportConfigBuilder"/>.</summary>
public static class FilterFunctionSupportConfigBuilderExtensions
{
    /// <summary>Configures whether the equality filter functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether equality functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static TBuilder SupportsEqualityFilterFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFilterFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsEqual(supported)
            .SupportsNotEqual(supported);

    /// <summary>Configures whether the comparison filter functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether comparison functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    public static TBuilder SupportsComparisonFilterFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFilterFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsEqualityFilterFunctions(supported)
            .SupportsGreaterThan(supported)
            .SupportsGreaterThanOrEqual(supported)
            .SupportsLessThan(supported)
            .SupportsLessThanOrEqual(supported);
    
    /// <summary>Configures whether the <see cref="string"/> filter functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="string"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    /// <remarks>Also configures the <see cref="string"/> field functions.</remarks>
    public static TBuilder SupportsStringFilterFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFilterFunctionSupportConfigBuilder =>
        (TBuilder)builder.SupportsComparisonFilterFunctions(supported)
            .SupportsHas(supported)
            .SupportsStartsWith(supported)
            .SupportsEndsWith(supported)
            .SupportsRegexMatch(supported)
            .SupportsStringFieldFunctions(supported);
    
    /// <summary>Configures whether the <see cref="DateTime"/> filter functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="DateTime"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    /// <remarks>Also configures the <see cref="DateTime"/> field functions.</remarks>
    public static TBuilder SupportsDateTimeFilterFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFilterFunctionSupportConfigBuilder =>
        builder.SupportsComparisonFilterFunctions(supported)
            .SupportsDateTimeFieldFunctions(supported);
    
    /// <summary>Configures whether the <see cref="double"/> filter functions are supported.</summary>
    /// <param name="builder">The builder to configure.</param>
    /// <param name="supported">Whether <see cref="double"/> functions are supported. Default <c>true</c>.</param>
    /// <typeparam name="TBuilder">Type of the builder.</typeparam>
    /// <returns>The builder to continue building.</returns>
    /// <remarks>Also configures the <see cref="double"/> field functions.</remarks>
    public static TBuilder SupportsDoubleFilterFunctions<TBuilder>(this TBuilder builder, bool supported = true)
        where TBuilder : IFilterFunctionSupportConfigBuilder =>
        builder.SupportsComparisonFilterFunctions(supported)
            .SupportsDoubleFieldFunctions(supported);
}