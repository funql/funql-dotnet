// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IParameterConfig"/>.</summary>
public interface IMutableParameterConfig : IMutableExtensibleConfig, IParameterConfig
{
    /// <inheritdoc cref="IParameterConfig.Name"/>
    public new string Name { get; set; }

    /// <inheritdoc cref="IParameterConfig.IsSupported"/>
    public new bool IsSupported { get; set; }

    /// <inheritdoc cref="IParameterConfig.IsRequired"/>
    public new bool IsRequired { get; set; }

    /// <inheritdoc cref="IParameterConfig.DefaultValue"/>
    public new Parameter? DefaultValue { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IParameterConfig ToConfig();
}