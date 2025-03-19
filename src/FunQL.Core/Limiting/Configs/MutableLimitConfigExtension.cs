// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Configs.Interfaces;

namespace FunQL.Core.Limiting.Configs;

/// <summary>Default implementation of <see cref="IMutableLimitConfigExtension"/>.</summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableLimitConfigExtension(
    string name
) : MutableConfigExtension(name), IMutableLimitConfigExtension
{
    /// <inheritdoc cref="IMutableLimitConfigExtension.MaxLimit"/>
    public int MaxLimit { get; set; } = ILimitConfigExtension.DefaultMaxLimit;

    /// <inheritdoc cref="IMutableLimitConfigExtension.ToConfig"/>
    public override ILimitConfigExtension ToConfig() => new ImmutableLimitConfigExtension(Name, MaxLimit);

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}