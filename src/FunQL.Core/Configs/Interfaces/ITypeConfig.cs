// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Common interface for a config defining a type.</summary>
public interface ITypeConfig : IExtensibleConfig
{
    /// <summary>Type that this config is specifying.</summary>
    public Type Type { get; }

    /// <summary>Whether type is nullable.</summary>
    public bool IsNullable { get; }
}