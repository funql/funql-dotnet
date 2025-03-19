// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableParameterConfig"/>.</summary>
/// <param name="name">Name of the parameter.</param>
public sealed class MutableParameterConfig(
    string name
) : MutableExtensibleConfig, IMutableParameterConfig
{
    /// <inheritdoc cref="IMutableParameterConfig.Name"/>
    public string Name { get; set; } = name;

    /// <inheritdoc cref="IMutableParameterConfig.IsSupported"/>
    public bool IsSupported { get; set; } = true;

    /// <inheritdoc cref="IMutableParameterConfig.IsRequired"/>
    public bool IsRequired { get; set; }

    /// <inheritdoc cref="IMutableParameterConfig.DefaultValue"/>
    public Parameter? DefaultValue { get; set; }

    /// <inheritdoc cref="IMutableParameterConfig.ToConfig"/>
    public override IParameterConfig ToConfig() => new ImmutableParameterConfig(
        Name,
        IsSupported,
        IsRequired,
        DefaultValue,
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );
}