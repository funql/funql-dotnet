// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Interface for a config with support for <see cref="IConfigExtension"/>.</summary>
public interface IExtensibleConfig : IConfig
{
    /// <summary>Gets list of all <see cref="IConfigExtension"/>.</summary>
    /// <returns>List of all <see cref="IConfigExtension"/>.</returns>
    public IEnumerable<IConfigExtension> GetExtensions();

    /// <summary>
    /// Gets the <see cref="IConfigExtension"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IConfigExtension"/>.</param>
    /// <returns>The <see cref="IConfigExtension"/> or null if it does not exist.</returns>
    public IConfigExtension? FindExtension(string name);
}