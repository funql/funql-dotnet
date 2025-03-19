// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IConfigExtension"/>.</summary>
public interface IMutableConfigExtension : IMutableConfig, IConfigExtension
{
    /// <summary>Name of this extension.</summary>
    public new string Name { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IConfigExtension ToConfig();
}