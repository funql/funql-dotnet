// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.Extensions;

/// <summary>Extensions related to <see cref="IRequestConfig"/>.</summary>
public static class RequestConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="Filter"/> <see cref="IParameterConfig"/> from given <paramref name="requestConfig"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="requestConfig">Config to get <see cref="IParameterConfig"/> from.</param>
    /// <returns>The <see cref="Filter"/> <see cref="IParameterConfig"/>.</returns>
    public static IParameterConfig? FindFilterParameterConfig(this IRequestConfig requestConfig) =>
        requestConfig.FindParameterConfig(Filter.FunctionName);
}