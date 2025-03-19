// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class of a <see cref="IMutableConfigExtension"/>.</summary>
/// <param name="name">Name of the config extension.</param>
public abstract class MutableConfigExtension(
    string name
) : MutableConfig, IMutableConfigExtension
{
    /// <inheritdoc cref="IMutableConfigExtension.Name"/>
    public string Name { get; set; } = name;

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public abstract override IConfigExtension ToConfig();
}