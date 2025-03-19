// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableFunctionConfig"/>.</summary>
/// <param name="name">Name of this config.</param>
/// <param name="returnTypeConfig">Return type config for this function.</param>
public sealed class MutableFunctionConfig(
    string name,
    IMutableTypeConfig returnTypeConfig
) : MutableExtensibleConfig, IMutableFunctionConfig
{
    /// <inheritdoc cref="IMutableFunctionConfig.Name"/>
    public string Name { get; set; } = name;

    /// <inheritdoc/>
    public IReadOnlyList<IMutableTypeConfig> ArgumentTypeConfigs { get; set; } =
        ImmutableList<IMutableTypeConfig>.Empty;

    /// <inheritdoc/>
    public IMutableTypeConfig ReturnTypeConfig { get; set; } = returnTypeConfig;

    /// <inheritdoc cref="IMutableFunctionConfig.ToConfig"/>
    public override IFunctionConfig ToConfig() => new ImmutableFunctionConfig(
        Name,
        ArgumentTypeConfigs.Select(it => it.ToConfig()).ToImmutableList(),
        ReturnTypeConfig.ToConfig(),
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region IFunctionConfig

    /// <inheritdoc/>
    IReadOnlyList<ITypeConfig> IFunctionConfig.ArgumentTypeConfigs => ArgumentTypeConfigs;

    /// <inheritdoc/>
    ITypeConfig IFunctionConfig.ReturnTypeConfig => ReturnTypeConfig;

    #endregion
}