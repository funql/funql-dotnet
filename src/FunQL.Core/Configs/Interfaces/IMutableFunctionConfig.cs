// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IFunctionConfig"/>.</summary>
public interface IMutableFunctionConfig : IMutableExtensibleConfig, IFunctionConfig
{
    /// <inheritdoc cref="IFunctionConfig.Name"/>
    public new string Name { get; set; }

    /// <inheritdoc cref="IFunctionConfig.ArgumentTypeConfigs"/>
    public new IReadOnlyList<IMutableTypeConfig> ArgumentTypeConfigs { get; set; }

    /// <inheritdoc cref="IFunctionConfig.ReturnTypeConfig"/>
    public new IMutableTypeConfig ReturnTypeConfig { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IFunctionConfig ToConfig();
}