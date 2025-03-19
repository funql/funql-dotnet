// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.FunctionSupport.Extensions;

/// <summary>Extensions related to sort support for <see cref="ITypeConfig"/>.</summary>
public static class TypeConfigSortSupportExtensions
{
    /// <summary>Default name for the <see cref="Sort"/> <see cref="IFunctionSupportConfigExtension"/>.</summary>
    private const string DefaultExtensionName = "FunQL.Core.Sorting.FunctionSupportConfigExtension";

    /// <summary>
    /// Gets the <see cref="IFunctionSupportConfigExtension"/> for <see cref="DefaultExtensionName"/> or null if it does
    /// not exist.
    /// </summary>
    /// <param name="typeConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IFunctionSupportConfigExtension"/> or null if it does not exist.</returns>
    public static IFunctionSupportConfigExtension? FindSortSupportConfigExtension(this ITypeConfig typeConfig) =>
        typeConfig.FindExtension<IFunctionSupportConfigExtension>(DefaultExtensionName);

    /// <summary>
    /// Gets the <see cref="IMutableFunctionSupportConfigExtension"/> for <see cref="DefaultExtensionName"/> or null if
    /// it does not exist.
    /// </summary>
    /// <param name="typeConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IMutableFunctionSupportConfigExtension"/> or null if it does not exist.</returns>
    public static IMutableFunctionSupportConfigExtension? FindSortSupportConfigExtension(
        this IMutableTypeConfig typeConfig
    ) => typeConfig.FindExtension<IMutableFunctionSupportConfigExtension>(DefaultExtensionName);

    /// <summary>
    /// Gets or adds the sort <see cref="IMutableFunctionSupportConfigExtension"/> to given <paramref name="config"/>.
    /// This will also set <see cref="Sort.FunctionName"/> as supported by default.
    /// </summary>
    /// <param name="config">Config to get extension from or add extension to.</param>
    /// <returns>The extension.</returns>
    public static IMutableFunctionSupportConfigExtension GetOrAddSortSupportConfigExtension(
        this IMutableTypeConfig config
    )
    {
        var extension = config.FindSortSupportConfigExtension();
        if (extension == null)
        {
            extension = new MutableFunctionSupportConfigExtension(DefaultExtensionName);
            extension.SetFunctionSupported(Sort.FunctionName, true);
            config.AddExtension(extension);
        }

        return extension;
    }
}