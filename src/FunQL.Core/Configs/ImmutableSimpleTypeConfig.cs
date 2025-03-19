// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="ISimpleTypeConfig"/>.</summary>
/// <inheritdoc cref="ImmutableTypeConfig"/>
public sealed record ImmutableSimpleTypeConfig(
    Type Type,
    bool IsNullable,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableTypeConfig(Type, IsNullable, Extensions), ISimpleTypeConfig;