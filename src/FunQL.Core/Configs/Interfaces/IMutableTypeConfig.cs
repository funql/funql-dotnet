// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="ITypeConfig"/>.</summary>
public interface IMutableTypeConfig : IMutableExtensibleConfig, ITypeConfig
{
    /// <summary>Type that this config is specifying.</summary>
    public new Type Type { get; set; }

    /// <summary>Whether type is nullable.</summary>
    public new bool IsNullable { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new ITypeConfig ToConfig();
}