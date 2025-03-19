// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IListTypeConfig"/>.</summary>
/// <param name="Type">Type that this config is specifying.</param>
/// <param name="IsNullable">Whether type is nullable.</param>
/// <param name="ElementTypeConfig">Config of the elements in the list.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableListTypeConfig(
    Type Type,
    bool IsNullable,
    ITypeConfig ElementTypeConfig,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableTypeConfig(Type, IsNullable, Extensions), IListTypeConfig;