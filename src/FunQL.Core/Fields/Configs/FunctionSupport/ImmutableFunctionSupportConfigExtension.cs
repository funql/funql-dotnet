// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;

namespace FunQL.Core.Fields.Configs.FunctionSupport;

/// <summary>Immutable implementation of <see cref="IFunctionSupportConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="ConfiguredFunctions">The configured functions.</param>
public sealed record ImmutableFunctionSupportConfigExtension(
    string Name,
    IImmutableDictionary<string, bool> ConfiguredFunctions
) : ImmutableConfigExtension(Name), IFunctionSupportConfigExtension
{
    /// <inheritdoc/>
    public IEnumerable<(string FunctionName, bool IsSupported)> GetConfiguredFunctions() =>
        ConfiguredFunctions.Select(it => (it.Key, it.Value));

    /// <inheritdoc/>
    public bool? IsFunctionSupported(string name) =>
        ConfiguredFunctions.TryGetValue(name, out var value) ? value : null;
}