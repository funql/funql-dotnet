// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableListTypeConfig"/>.</summary>
/// <param name="type"><inheritdoc cref="MutableTypeConfig"/></param>
/// <param name="elementTypeConfig">Config of the elements in the list.</param>
public sealed class MutableListTypeConfig(
    Type type,
    IMutableTypeConfig elementTypeConfig
) : MutableTypeConfig(type), IMutableListTypeConfig
{
    /// <inheritdoc/>
    public IMutableTypeConfig ElementTypeConfig { get; set; } = elementTypeConfig;

    /// <inheritdoc cref="IMutableListTypeConfig.ToConfig"/>
    public override IListTypeConfig ToConfig() => new ImmutableListTypeConfig(
        Type,
        IsNullable,
        ElementTypeConfig.ToConfig(),
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );

    #region IListTypeConfig

    /// <inheritdoc/>
    ITypeConfig IListTypeConfig.ElementTypeConfig => ElementTypeConfig;

    #endregion
}