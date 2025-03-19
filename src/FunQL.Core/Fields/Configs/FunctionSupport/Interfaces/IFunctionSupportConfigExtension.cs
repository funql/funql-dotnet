// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;

/// <summary>Extension for specifying the functions that are supported, e.g. on a certain field or type.</summary>
public interface IFunctionSupportConfigExtension : IConfigExtension
{
    /// <summary>Gets functions with their support status that have been configured for this extension.</summary>
    /// <returns>Configured functions with their support status.</returns>
    public IEnumerable<(string FunctionName, bool IsSupported)> GetConfiguredFunctions();

    /// <summary>
    /// Returns whether function with given <paramref name="name"/> is supported or <c>null</c> if function support is
    /// unknown.
    /// </summary>
    /// <param name="name">Name of the function.</param>
    /// <returns>Whether function is supported or <c>null</c> if unknown.</returns>
    public bool? IsFunctionSupported(string name);
}