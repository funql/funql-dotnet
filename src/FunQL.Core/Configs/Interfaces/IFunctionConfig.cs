// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>
/// Config for a function, specifying the function declaration, which defines the name, argument types and return type.
/// </summary>
public interface IFunctionConfig : IExtensibleConfig
{
    /// <summary>Name of this function.</summary>
    public string Name { get; }

    /// <summary>Argument type configs for this function.</summary>
    public IReadOnlyList<ITypeConfig> ArgumentTypeConfigs { get; }

    /// <summary>Return type config for this function.</summary>
    public ITypeConfig ReturnTypeConfig { get; }
}