// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Configs.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfig"/>.</summary>
public static class RequestConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="Input"/> <see cref="IParameterConfig"/> from given <paramref name="requestConfig"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="requestConfig">Config to get <see cref="IParameterConfig"/> from.</param>
    /// <returns>The <see cref="Input"/> <see cref="IParameterConfig"/>.</returns>
    public static IParameterConfig? FindInputParameterConfig(this IRequestConfig requestConfig) =>
        requestConfig.FindParameterConfig(Input.FunctionName);
}