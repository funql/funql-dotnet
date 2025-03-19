// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="ISchemaConfig"/>.</summary>
/// <param name="RequestConfigs">The request configs for this config.</param>
/// <param name="FunctionConfigs">The function configs for this config.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableSchemaConfig(
    IImmutableDictionary<string, IRequestConfig> RequestConfigs,
    IImmutableDictionary<string, IFunctionConfig> FunctionConfigs,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), ISchemaConfig
{
    /// <inheritdoc/>
    public IEnumerable<IRequestConfig> GetRequestConfigs() => RequestConfigs.Values;

    /// <inheritdoc/>
    public IRequestConfig? FindRequestConfig(string name) => RequestConfigs.GetValueOrDefault(name);

    /// <inheritdoc/>
    public IEnumerable<IFunctionConfig> GetFunctionConfigs() => FunctionConfigs.Values;

    /// <inheritdoc/>
    public IFunctionConfig? FindFunctionConfig(string name) => FunctionConfigs.GetValueOrDefault(name);
}