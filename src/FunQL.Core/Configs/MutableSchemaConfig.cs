// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableSchemaConfig"/>.</summary>
public sealed class MutableSchemaConfig : MutableExtensibleConfig, IMutableSchemaConfig
{
    /// <summary>Current list with <see cref="IMutableRequestConfig"/>.</summary>
    private readonly List<IMutableRequestConfig> _requestConfigs = [];

    /// <summary>Current list with <see cref="IMutableFunctionConfig"/>.</summary>
    private readonly List<IMutableFunctionConfig> _functionConfigs = [];

    /// <inheritdoc/>
    public IEnumerable<IMutableRequestConfig> GetRequestConfigs() => _requestConfigs;

    /// <inheritdoc/>
    public IMutableRequestConfig? FindRequestConfig(string name) =>
        _requestConfigs.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddRequestConfig(IMutableRequestConfig requestConfig) => _requestConfigs.Add(requestConfig);

    /// <inheritdoc/>
    public IMutableRequestConfig? RemoveRequestConfig(string name)
    {
        var config = FindRequestConfig(name);
        if (config != null)
            _requestConfigs.Remove(config);

        return config;
    }

    /// <inheritdoc/>
    public IEnumerable<IMutableFunctionConfig> GetFunctionConfigs() => _functionConfigs;

    /// <inheritdoc/>
    public IMutableFunctionConfig? FindFunctionConfig(string name) =>
        _functionConfigs.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddFunctionConfig(IMutableFunctionConfig functionConfig) => _functionConfigs.Add(functionConfig);

    /// <inheritdoc/>
    public IMutableFunctionConfig? RemoveFunctionConfig(string name)
    {
        var config = FindFunctionConfig(name);
        if (config != null)
            _functionConfigs.Remove(config);

        return config;
    }

    /// <inheritdoc cref="IMutableSchemaConfig.ToConfig"/>
    public override ISchemaConfig ToConfig() => new ImmutableSchemaConfig(
        _requestConfigs.ToImmutableDictionary(it => it.Name, it => it.ToConfig()),
        _functionConfigs.ToImmutableDictionary(it => it.Name, it => it.ToConfig()),
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region ISchemaConfig

    /// <inheritdoc/>
    IEnumerable<IRequestConfig> ISchemaConfig.GetRequestConfigs() => GetRequestConfigs();

    /// <inheritdoc/>
    IRequestConfig? ISchemaConfig.FindRequestConfig(string name) => FindRequestConfig(name);

    /// <inheritdoc/>
    IEnumerable<IFunctionConfig> ISchemaConfig.GetFunctionConfigs() => GetFunctionConfigs();

    /// <inheritdoc/>
    IFunctionConfig? ISchemaConfig.FindFunctionConfig(string name) => FindFunctionConfig(name);

    #endregion
}