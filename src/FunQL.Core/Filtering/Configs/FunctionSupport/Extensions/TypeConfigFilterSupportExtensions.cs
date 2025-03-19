// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.FunctionSupport.Extensions;

/// <summary>Extensions related to filter support for <see cref="ITypeConfig"/>.</summary>
public static class TypeConfigFilterSupportExtensions
{
    /// <summary>Default name for the <see cref="Filter"/> <see cref="IFunctionSupportConfigExtension"/>.</summary>
    private const string DefaultExtensionName = "FunQL.Core.Filtering.FunctionSupportConfigExtension";

    /// <summary>
    /// Gets the <see cref="IFunctionSupportConfigExtension"/> for <see cref="DefaultExtensionName"/> or null if it does
    /// not exist.
    /// </summary>
    /// <param name="typeConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IFunctionSupportConfigExtension"/> or null if it does not exist.</returns>
    public static IFunctionSupportConfigExtension? FindFilterSupportConfigExtension(this ITypeConfig typeConfig) =>
        typeConfig.FindExtension<IFunctionSupportConfigExtension>(DefaultExtensionName);

    /// <summary>
    /// Gets the <see cref="IMutableFunctionSupportConfigExtension"/> for <see cref="DefaultExtensionName"/> or null if
    /// it does not exist.
    /// </summary>
    /// <param name="typeConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IMutableFunctionSupportConfigExtension"/> or null if it does not exist.</returns>
    public static IMutableFunctionSupportConfigExtension? FindFilterSupportConfigExtension(
        this IMutableTypeConfig typeConfig
    ) => typeConfig.FindExtension<IMutableFunctionSupportConfigExtension>(DefaultExtensionName);

    /// <summary>
    /// Gets or adds the filter <see cref="IMutableFunctionSupportConfigExtension"/> to given <paramref name="config"/>.
    /// This will also set <see cref="Filter.FunctionName"/> as supported by default.
    /// </summary>
    /// <param name="config">Config to get extension from or add extension to.</param>
    /// <returns>The extension.</returns>
    public static IMutableFunctionSupportConfigExtension GetOrAddFilterSupportConfigExtension(
        this IMutableTypeConfig config
    )
    {
        var extension = config.FindFilterSupportConfigExtension();
        if (extension == null)
        {
            extension = new MutableFunctionSupportConfigExtension(DefaultExtensionName);
            extension.SetFunctionSupported(Filter.FunctionName, true);
            config.AddExtension(extension);
        }

        return extension;
    }
}