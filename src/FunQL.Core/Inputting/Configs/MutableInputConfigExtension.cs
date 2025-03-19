// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Interfaces;

namespace FunQL.Core.Inputting.Configs;

/// <summary>Default implementation of <see cref="IMutableInputConfigExtension"/>.</summary>
/// <param name="name"><inheritdoc cref="MutableConfigExtension"/></param>
/// <param name="typeConfig">Config of the type of the input.</param>
public sealed class MutableInputConfigExtension(
    string name,
    IMutableTypeConfig typeConfig
) : MutableConfigExtension(name), IMutableInputConfigExtension
{
    /// <inheritdoc/>
    public IMutableTypeConfig TypeConfig { get; set; } = typeConfig;

    /// <inheritdoc cref="IMutableInputConfigExtension.ToConfig"/>
    public override IInputConfigExtension ToConfig() => new ImmutableInputConfigExtension(Name, TypeConfig.ToConfig());

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion

    #region IInputTypeConfigExtension

    /// <inheritdoc/>
    ITypeConfig IInputConfigExtension.TypeConfig => TypeConfig;

    #endregion
}