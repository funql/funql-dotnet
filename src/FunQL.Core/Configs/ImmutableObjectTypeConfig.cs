// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IObjectTypeConfig"/>.</summary>
/// <param name="Type">Type that this config is specifying.</param>
/// <param name="IsNullable">Whether type is nullable.</param>
/// <param name="FieldConfigs">The field configs for this config.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableObjectTypeConfig(
    Type Type,
    bool IsNullable,
    IImmutableDictionary<string, IFieldConfig> FieldConfigs,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableTypeConfig(Type, IsNullable, Extensions), IObjectTypeConfig
{
    /// <inheritdoc/>
    public IEnumerable<IFieldConfig> GetFieldConfigs() => FieldConfigs.Values;

    /// <inheritdoc/>
    public IFieldConfig? FindFieldConfig(string name) => FieldConfigs.GetValueOrDefault(name);
}