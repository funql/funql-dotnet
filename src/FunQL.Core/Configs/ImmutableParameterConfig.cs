// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IParameterConfig"/>.</summary>
/// <param name="Name">Name of the parameter.</param>
/// <param name="IsSupported">Whether the parameter is supported.</param>
/// <param name="IsRequired">Whether the parameter is required.</param>
/// <param name="DefaultValue">Default value of the parameter if not given.</param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableParameterConfig(
    string Name,
    bool IsSupported,
    bool IsRequired,
    Parameter? DefaultValue,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), IParameterConfig;