// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Filtering.Configs.FunctionSupport.Extensions;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Extensions;

/// <summary>Extensions related to <see cref="ITypeConfigBuilder"/>.</summary>
public static class TypeConfigBuilderExtensions
{
    /// <summary>
    /// Returns the <see cref="IFilterFunctionSupportConfigBuilder"/> to build the filter
    /// <see cref="IMutableFunctionSupportConfigExtension"/> for given <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">
    /// Builder to configure filter <see cref="IMutableFunctionSupportConfigExtension"/> for.
    /// </param>
    /// <returns>
    /// The <see cref="IFilterFunctionSupportConfigBuilder"/> to build filter
    /// <see cref="IMutableFunctionSupportConfigExtension"/>.
    /// </returns>
    public static IFilterFunctionSupportConfigBuilder FilterSupport(this ITypeConfigBuilder builder) =>
        new FilterFunctionSupportConfigBuilder(builder.MutableConfig.GetOrAddFilterSupportConfigExtension());

    /// <summary>
    /// Specifies that given <paramref name="builder"/> supports the <see cref="Filter"/> function and calls the
    /// <paramref name="action"/> to configure it, returning the <paramref name="builder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="Filter"/> function support for.</param>
    /// <param name="action">Optional action to configure <see cref="Filter"/> support.</param>
    /// <returns>The builder to continue building.</returns>
    public static ITypeConfigBuilder SupportsFilter(
        this ITypeConfigBuilder builder,
        Action<IFilterFunctionSupportConfigBuilder>? action = null
    )
    {
        var nestedBuilder = FilterSupport(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}