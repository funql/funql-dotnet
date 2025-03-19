// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using System.Reflection;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableFieldConfig"/>.</summary>
/// <param name="name">Name of this config.</param>
/// <param name="typeConfig">Config of the type of this field.</param>
public sealed class MutableFieldConfig(
    string name,
    IMutableTypeConfig typeConfig
) : MutableExtensibleConfig, IMutableFieldConfig
{
    /// <inheritdoc cref="IMutableFieldConfig.Name"/>
    public string Name { get; set; } = name;

    /// <inheritdoc/>
    public IMutableTypeConfig TypeConfig { get; set; } = typeConfig;

    /// <inheritdoc cref="IMutableFieldConfig.MemberInfo"/>
    public MemberInfo? MemberInfo { get; set; }

    /// <inheritdoc cref="IMutableFieldConfig.ToConfig"/>
    public override IFieldConfig ToConfig() => new ImmutableFieldConfig(
        Name,
        TypeConfig.ToConfig(),
        MemberInfo,
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region IFieldConfig

    /// <inheritdoc/>
    ITypeConfig IFieldConfig.TypeConfig => TypeConfig;

    #endregion
}