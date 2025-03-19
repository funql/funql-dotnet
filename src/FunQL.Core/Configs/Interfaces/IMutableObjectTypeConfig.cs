// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IObjectTypeConfig"/>.</summary>
public interface IMutableObjectTypeConfig : IMutableTypeConfig, IObjectTypeConfig
{
    /// <summary>Gets list of all <see cref="IMutableFieldConfig"/>.</summary>
    /// <returns>List of all <see cref="IMutableFieldConfig"/>.</returns>
    public new IEnumerable<IMutableFieldConfig> GetFieldConfigs();

    /// <summary>
    /// Gets the <see cref="IMutableFieldConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IMutableFieldConfig"/>.</param>
    /// <returns>The <see cref="IMutableFieldConfig"/> or null if it does not exist.</returns>
    public new IMutableFieldConfig? FindFieldConfig(string name);

    /// <summary>Adds the <paramref name="fieldConfig"/>.</summary>
    /// <param name="fieldConfig">The <see cref="IMutableFieldConfig"/>.</param>
    public void AddFieldConfig(IMutableFieldConfig fieldConfig);

    /// <summary>Removes the <see cref="IMutableFieldConfig"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="IMutableFieldConfig"/>.</param>
    /// <returns>The <see cref="IMutableFieldConfig"/> that was removed or null if it did not exist.</returns>
    public IMutableFieldConfig? RemoveFieldConfig(string name);

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IObjectTypeConfig ToConfig();
}