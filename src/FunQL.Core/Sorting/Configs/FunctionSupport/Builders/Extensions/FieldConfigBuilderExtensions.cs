// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Sorting.Configs.FunctionSupport.Extensions;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Extensions;

/// <summary>Extensions related to <see cref="IFieldConfigBuilder"/>.</summary>
public static class FieldConfigBuilderExtensions
{
    /// <summary>
    /// Returns the <see cref="ISortFunctionSupportConfigBuilder"/> to build the sort
    /// <see cref="IMutableFunctionSupportConfigExtension"/> for given <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">
    /// Builder to configure sort <see cref="IMutableFunctionSupportConfigExtension"/> for.
    /// </param>
    /// <returns>
    /// The <see cref="ISortFunctionSupportConfigBuilder"/> to build sort
    /// <see cref="IMutableFunctionSupportConfigExtension"/>.
    /// </returns>
    public static ISortFunctionSupportConfigBuilder SortSupport(this IFieldConfigBuilder builder) =>
        new SortFunctionSupportConfigBuilder(builder.MutableConfig.TypeConfig.GetOrAddSortSupportConfigExtension());

    /// <summary>
    /// Specifies that given <paramref name="builder"/> supports the <see cref="Sort"/> function and calls the
    /// <paramref name="action"/> to configure it, returning the <paramref name="builder"/> to continue building.
    /// </summary>
    /// <param name="builder">Builder to configure <see cref="Sort"/> function support for.</param>
    /// <param name="action">Optional action to configure <see cref="Sort"/> support.</param>
    /// <returns>The builder to continue building.</returns>
    public static IFieldConfigBuilder SupportsSort(
        this IFieldConfigBuilder builder,
        Action<ISortFunctionSupportConfigBuilder>? action = null
    )
    {
        var nestedBuilder = SortSupport(builder);
        action?.Invoke(nestedBuilder);
        return builder;
    }
}