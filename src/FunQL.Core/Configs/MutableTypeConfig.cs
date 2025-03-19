// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class of a <see cref="IMutableTypeConfig"/>.</summary>
/// <param name="type">Type that this config is specifying.</param>
public abstract class MutableTypeConfig(
    Type type
) : MutableExtensibleConfig, IMutableTypeConfig
{
    /// <inheritdoc cref="IMutableTypeConfig.Type"/>
    public Type Type { get; set; } = type;

    /// <inheritdoc cref="IMutableTypeConfig.IsNullable"/>
    public bool IsNullable { get; set; }

    /// <inheritdoc cref="IMutableTypeConfig.ToConfig"/>
    public abstract override ITypeConfig ToConfig();
}