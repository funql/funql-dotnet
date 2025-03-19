// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableRequestConfig"/>.</summary>
/// <param name="name">Name of the request.</param>
/// <param name="returnTypeConfig">Config of the return type for this request.</param>
public sealed class MutableRequestConfig(
    string name,
    IMutableTypeConfig returnTypeConfig
) : MutableExtensibleConfig, IMutableRequestConfig
{
    /// <summary>Current list with <see cref="IMutableParameterConfig"/>.</summary>
    private readonly List<IMutableParameterConfig> _parameterConfigs = [];

    /// <inheritdoc cref="IMutableRequestConfig.Name"/>
    public string Name { get; set; } = name;

    /// <inheritdoc/>
    public IMutableTypeConfig ReturnTypeConfig { get; set; } = returnTypeConfig;

    /// <inheritdoc/>
    public IEnumerable<IMutableParameterConfig> GetParameterConfigs() => _parameterConfigs;

    /// <inheritdoc/>
    public IMutableParameterConfig? FindParameterConfig(string name) =>
        _parameterConfigs.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddParameterConfig(IMutableParameterConfig parameterConfig) => _parameterConfigs.Add(parameterConfig);

    /// <inheritdoc/>
    public IMutableParameterConfig? RemoveParameterConfig(string name)
    {
        var config = FindParameterConfig(name);
        if (config != null)
            _parameterConfigs.Remove(config);

        return config;
    }

    /// <inheritdoc cref="IMutableRequestConfig.ToConfig"/>
    public override IRequestConfig ToConfig() => new ImmutableRequestConfig(
        Name,
        ReturnTypeConfig.ToConfig(),
        _parameterConfigs.ToImmutableDictionary(it => it.Name, it => it.ToConfig()),
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region IRequestConfig

    /// <inheritdoc/>
    ITypeConfig IRequestConfig.ReturnTypeConfig => ReturnTypeConfig;

    /// <inheritdoc/>
    IEnumerable<IParameterConfig> IRequestConfig.GetParameterConfigs() => GetParameterConfigs();

    /// <inheritdoc/>
    IParameterConfig? IRequestConfig.FindParameterConfig(string name) => FindParameterConfig(name);

    #endregion
}