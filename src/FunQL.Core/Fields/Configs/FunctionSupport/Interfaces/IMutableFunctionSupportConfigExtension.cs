// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;

/// <summary>Mutable version of <see cref="IFunctionSupportConfigExtension"/>.</summary>
public interface IMutableFunctionSupportConfigExtension : IMutableConfigExtension, IFunctionSupportConfigExtension
{
    /// <summary>Sets whether function with given <paramref name="name"/> is supported.</summary>
    /// <param name="name">Name of the function.</param>
    /// <param name="supported">Whether function is supported.</param>
    public void SetFunctionSupported(string name, bool supported);

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IFunctionSupportConfigExtension ToConfig();
}