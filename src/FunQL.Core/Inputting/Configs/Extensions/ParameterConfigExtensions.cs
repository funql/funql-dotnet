// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Interfaces;

namespace FunQL.Core.Inputting.Configs.Extensions;

/// <summary>Extensions related to <see cref="IParameterConfig"/>.</summary>
public static class ParameterConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IInputConfigExtension"/> for <see cref="IInputConfigExtension.DefaultName"/> or <c>null</c>
    /// if not found.
    /// </summary>
    /// <param name="parameterConfig">Config to get extension from.</param>
    /// <returns>The <see cref="IInputConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IInputConfigExtension? FindInputConfigExtension(this IParameterConfig parameterConfig) =>
        parameterConfig.FindExtension<IInputConfigExtension>(IInputConfigExtension.DefaultName);
}