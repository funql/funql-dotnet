// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for an object type.</summary>
public interface IObjectTypeConfig : ITypeConfig
{
    /// <summary>Gets list of all <see cref="IFieldConfig"/>.</summary>
    /// <returns>List of all <see cref="IFieldConfig"/>.</returns>
    public IEnumerable<IFieldConfig> GetFieldConfigs();

    /// <summary>
    /// Gets the <see cref="IFieldConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IFieldConfig"/>.</param>
    /// <returns>The <see cref="IFieldConfig"/> or null if it does not exist.</returns>
    public IFieldConfig? FindFieldConfig(string name);
}