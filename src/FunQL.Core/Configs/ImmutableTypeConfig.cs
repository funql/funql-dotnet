// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class for an immutable <see cref="ITypeConfig"/>.</summary>
/// <param name="Type">Type that this config is specifying.</param>
/// <param name="IsNullable">Whether type is nullable.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public abstract record ImmutableTypeConfig(
    Type Type,
    bool IsNullable,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), ITypeConfig;