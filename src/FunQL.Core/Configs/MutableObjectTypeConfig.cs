// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableObjectTypeConfig"/>.</summary>
/// <inheritdoc cref="MutableTypeConfig"/>
public sealed class MutableObjectTypeConfig(
    Type type
) : MutableTypeConfig(type), IMutableObjectTypeConfig
{
    /// <summary>Current list with <see cref="IMutableFieldConfig"/>.</summary>
    private readonly List<IMutableFieldConfig> _fieldConfigs = [];

    /// <inheritdoc/>
    public IEnumerable<IMutableFieldConfig> GetFieldConfigs() => _fieldConfigs;

    /// <inheritdoc/>
    public IMutableFieldConfig? FindFieldConfig(string name) => _fieldConfigs.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddFieldConfig(IMutableFieldConfig fieldConfig) => _fieldConfigs.Add(fieldConfig);

    /// <inheritdoc/>
    public IMutableFieldConfig? RemoveFieldConfig(string name)
    {
        var fieldConfig = FindFieldConfig(name);
        if (fieldConfig != null)
            _fieldConfigs.Remove(fieldConfig);

        return fieldConfig;
    }

    /// <inheritdoc cref="IMutableObjectTypeConfig.ToConfig"/>
    public override IObjectTypeConfig ToConfig() => new ImmutableObjectTypeConfig(
        Type,
        IsNullable,
        _fieldConfigs.ToImmutableDictionary(it => it.Name, it => it.ToConfig()),
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region IObjectTypeConfig

    /// <inheritdoc/>
    IEnumerable<IFieldConfig> IObjectTypeConfig.GetFieldConfigs() => GetFieldConfigs();

    /// <inheritdoc/>
    IFieldConfig? IObjectTypeConfig.FindFieldConfig(string name) => FindFieldConfig(name);

    #endregion
}