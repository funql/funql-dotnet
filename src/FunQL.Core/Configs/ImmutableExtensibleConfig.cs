// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class for an immutable <see cref="IExtensibleConfig"/>.</summary>
/// <param name="Extensions">The extensions for this config.</param>
public abstract record ImmutableExtensibleConfig(
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableConfig, IExtensibleConfig
{
    /// <inheritdoc/>
    public IEnumerable<IConfigExtension> GetExtensions() => Extensions.Values;

    /// <inheritdoc/>
    public IConfigExtension? FindExtension(string name) => Extensions.GetValueOrDefault(name);
}