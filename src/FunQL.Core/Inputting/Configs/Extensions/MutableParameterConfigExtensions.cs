// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Interfaces;

namespace FunQL.Core.Inputting.Configs.Extensions;

/// <summary>Extensions related to <see cref="IMutableParameterConfig"/>.</summary>
public static class MutableParameterConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IMutableInputConfigExtension"/> for <see cref="IInputConfigExtension.DefaultName"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="parameterConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IInputConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IMutableInputConfigExtension? FindInputConfigExtension(
        this IMutableParameterConfig parameterConfig
    ) => parameterConfig.FindExtension<IMutableInputConfigExtension>(IInputConfigExtension.DefaultName);

    /// <summary>
    /// Gets or adds the <see cref="IInputConfigExtension"/> to given <paramref name="config"/> for given
    /// <paramref name="type"/>.
    /// </summary>
    /// <param name="config">Config to get extension from or add extension to.</param>
    /// <param name="type">Type of the input.</param>
    /// <returns>The extension.</returns>
    public static IMutableInputConfigExtension GetOrAddInputConfigExtension(
        this IMutableParameterConfig config,
        Type type
    )
    {
        var extension = config.FindInputConfigExtension();
        if (extension == null)
        {
            extension = new MutableInputConfigExtension(
                IInputConfigExtension.DefaultName,
                new MutableSimpleTypeConfig(type)
            );
            config.AddExtension(extension);
        }

        return extension;
    }
}