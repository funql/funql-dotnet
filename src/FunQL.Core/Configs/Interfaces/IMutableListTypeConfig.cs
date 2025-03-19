// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IListTypeConfig"/>.</summary>
public interface IMutableListTypeConfig : IMutableTypeConfig, IListTypeConfig
{
    /// <summary>Config of the elements in the list.</summary>
    public new IMutableTypeConfig ElementTypeConfig { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IListTypeConfig ToConfig();
}