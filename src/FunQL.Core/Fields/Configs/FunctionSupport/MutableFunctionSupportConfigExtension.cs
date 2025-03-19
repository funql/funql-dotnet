// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;

namespace FunQL.Core.Fields.Configs.FunctionSupport;

/// <summary>Default implementation of <see cref="IMutableFunctionSupportConfigExtension"/>.</summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableFunctionSupportConfigExtension(
    string name
) : MutableConfigExtension(name), IMutableFunctionSupportConfigExtension
{
    /// <summary>Current configured functions.</summary>
    private readonly Dictionary<string, bool> _configuredFunctions = new();

    /// <inheritdoc/>
    public IEnumerable<(string FunctionName, bool IsSupported)> GetConfiguredFunctions() =>
        _configuredFunctions.Select(it => (it.Key, it.Value));

    /// <inheritdoc/>
    public bool? IsFunctionSupported(string name) =>
        _configuredFunctions.TryGetValue(name, out var value) ? value : null;

    /// <inheritdoc/>
    public void SetFunctionSupported(string name, bool supported)
    {
        _configuredFunctions[name] = supported;
    }

    /// <inheritdoc cref="IMutableFunctionSupportConfigExtension.ToConfig"/>
    public override IFunctionSupportConfigExtension ToConfig() => new ImmutableFunctionSupportConfigExtension(
        Name,
        _configuredFunctions.ToImmutableDictionary()
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}