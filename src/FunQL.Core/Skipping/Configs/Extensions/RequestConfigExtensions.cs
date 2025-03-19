// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Configs.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfig"/>.</summary>
public static class RequestConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="Skip"/> <see cref="IParameterConfig"/> from given <paramref name="requestConfig"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="requestConfig">Config to get <see cref="IParameterConfig"/> from.</param>
    /// <returns>The <see cref="Skip"/> <see cref="IParameterConfig"/>.</returns>
    public static IParameterConfig? FindSkipParameterConfig(this IRequestConfig requestConfig) =>
        requestConfig.FindParameterConfig(Skip.FunctionName);
}