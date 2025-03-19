// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class of a <see cref="IMutableConfig"/>.</summary>
public abstract class MutableConfig : IMutableConfig
{
    /// <inheritdoc/>
    public abstract IConfig ToConfig();
}