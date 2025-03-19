// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Limiting.Configs.Interfaces;

/// <summary>Mutable version of <see cref="ILimitConfigExtension"/>.</summary>
public interface IMutableLimitConfigExtension : IMutableConfigExtension, ILimitConfigExtension
{
    /// <inheritdoc cref="ILimitConfigExtension.MaxLimit"/>
    public new int MaxLimit { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new ILimitConfigExtension ToConfig();
}