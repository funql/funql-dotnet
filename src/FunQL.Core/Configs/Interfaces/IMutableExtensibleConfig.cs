// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IExtensibleConfig"/>.</summary>
public interface IMutableExtensibleConfig : IMutableConfig, IExtensibleConfig
{
    /// <summary>Gets list of all <see cref="IMutableConfigExtension"/>.</summary>
    /// <returns>List of all <see cref="IMutableConfigExtension"/>.</returns>
    public new IEnumerable<IMutableConfigExtension> GetExtensions();

    /// <summary>
    /// Gets the <see cref="IMutableConfigExtension"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IMutableConfigExtension"/>.</param>
    /// <returns>The <see cref="IMutableConfigExtension"/> or null if it does not exist.</returns>
    public new IMutableConfigExtension? FindExtension(string name);

    /// <summary>Adds the <paramref name="extension"/>.</summary>
    /// <param name="extension">The <see cref="IMutableConfigExtension"/>.</param>
    public void AddExtension(IMutableConfigExtension extension);

    /// <summary>Removes the <see cref="IMutableConfigExtension"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="IMutableConfigExtension"/>.</param>
    /// <returns>The <see cref="IMutableConfigExtension"/> that was removed or null if it did not exist.</returns>
    public IMutableConfigExtension? RemoveExtension(string name);

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IExtensibleConfig ToConfig();
}