// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>
/// Common interface for a config extension.
///
/// This allows for extending a default config with custom or specific configuration options. For example, it can
/// specify which functions are supported for a particular field, which can then be used during validation.
/// </summary>
/// <seealso cref="IExtensibleConfig"/>
public interface IConfigExtension : IConfig
{
    /// <summary>Name of this extension.</summary>
    public string Name { get; }
}