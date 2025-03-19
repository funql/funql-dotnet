// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Configs.Interfaces;
using static FunQL.Core.Limiting.Configs.Extensions.ParameterConfigExtensions;

namespace FunQL.Core.Limiting.Configs.Extensions;

/// <summary>Extensions related to <see cref="IMutableParameterConfig"/>.</summary>
public static class MutableParameterConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IMutableLimitConfigExtension"/> for <see cref="ILimitConfigExtension.DefaultName"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="ILimitConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IMutableLimitConfigExtension? FindLimitConfigExtension(this IMutableParameterConfig config) =>
        config.FindExtension<IMutableLimitConfigExtension>(ILimitConfigExtension.DefaultName);

    /// <summary>Gets or adds the <see cref="ILimitConfigExtension"/> to given <paramref name="config"/>.</summary>
    /// <param name="config">Config to get extension from or add extension to.</param>
    /// <returns>The extension.</returns>
    public static IMutableLimitConfigExtension GetOrAddLimitConfigExtension(this IMutableParameterConfig config)
    {
        var extension = config.FindLimitConfigExtension();
        if (extension == null)
        {
            extension = new MutableLimitConfigExtension(ILimitConfigExtension.DefaultName);
            config.AddExtension(extension);
        }

        return extension;
    }
}