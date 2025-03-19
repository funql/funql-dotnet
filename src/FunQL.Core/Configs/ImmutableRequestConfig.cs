// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IRequestConfig"/>.</summary>
/// <param name="Name">Name of the request.</param>
/// <param name="ReturnTypeConfig">Config of the return type for this request.</param>
/// <param name="ParameterConfigs">The parameter configs for this config.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableRequestConfig(
    string Name,
    ITypeConfig ReturnTypeConfig,
    IImmutableDictionary<string, IParameterConfig> ParameterConfigs,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), IRequestConfig
{
    /// <inheritdoc/>
    public IEnumerable<IParameterConfig> GetParameterConfigs() => ParameterConfigs.Values;

    /// <inheritdoc/>
    public IParameterConfig? FindParameterConfig(string name) => ParameterConfigs.GetValueOrDefault(name);
}