// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Limiting.Configs.Interfaces;

namespace FunQL.Core.Limiting.Configs;

/// <summary>Immutable implementation of <see cref="ILimitConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="MaxLimit">Maximum value of limit.</param>
public sealed record ImmutableLimitConfigExtension(
    string Name,
    int MaxLimit
) : ImmutableConfigExtension(Name), ILimitConfigExtension;