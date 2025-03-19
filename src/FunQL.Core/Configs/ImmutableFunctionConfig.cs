// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IFunctionConfig"/>.</summary>
/// <param name="Name">Name of this field.</param>
/// <param name="ArgumentTypeConfigs">Argument type configs for this function.</param>
/// <param name="ReturnTypeConfig">Return type config for this function.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableFunctionConfig(
    string Name,
    IReadOnlyList<ITypeConfig> ArgumentTypeConfigs,
    ITypeConfig ReturnTypeConfig,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), IFunctionConfig;