// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="ISimpleTypeConfig"/>.</summary>
public interface IMutableSimpleTypeConfig : IMutableTypeConfig, ISimpleTypeConfig
{
    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new ISimpleTypeConfig ToConfig();
}