// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Configs.Interfaces;

namespace FunQL.Core.Limiting.Configs.Extensions;

/// <summary>Extensions related to <see cref="IParameterConfig"/>.</summary>
public static class ParameterConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="ILimitConfigExtension"/> for <see cref="ILimitConfigExtension.DefaultName"/> or <c>null</c>
    /// if not found.
    /// </summary>
    /// <param name="parameterConfig">Config to get extension from.</param>
    /// <returns>The <see cref="ILimitConfigExtension"/> or <c>null</c> if not found.</returns>
    public static ILimitConfigExtension? FindLimitConfigExtension(this IParameterConfig parameterConfig) =>
        parameterConfig.FindExtension<ILimitConfigExtension>(ILimitConfigExtension.DefaultName);
}