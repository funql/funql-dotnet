// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IRequestConfig"/>.</summary>
public interface IMutableRequestConfig : IMutableExtensibleConfig, IRequestConfig
{
    /// <summary>Name of the request.</summary>
    public new string Name { get; set; }

    /// <summary>Config of the return type for this request.</summary>
    public new IMutableTypeConfig ReturnTypeConfig { get; set; }

    /// <summary>Gets list of all <see cref="IMutableParameterConfig"/>.</summary>
    /// <returns>List of all <see cref="IMutableParameterConfig"/>.</returns>
    public new IEnumerable<IMutableParameterConfig> GetParameterConfigs();

    /// <summary>
    /// Gets the <see cref="IMutableParameterConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IMutableParameterConfig"/>.</param>
    /// <returns>The <see cref="IMutableParameterConfig"/> or null if it does not exist.</returns>
    public new IMutableParameterConfig? FindParameterConfig(string name);

    /// <summary>Adds the <paramref name="parameterConfig"/>.</summary>
    /// <param name="parameterConfig">The <see cref="IMutableParameterConfig"/>.</param>
    public void AddParameterConfig(IMutableParameterConfig parameterConfig);

    /// <summary>Removes the <see cref="IMutableParameterConfig"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="IMutableParameterConfig"/>.</param>
    /// <returns>The <see cref="IMutableParameterConfig"/> that was removed or null if it did not exist.</returns>
    public IMutableParameterConfig? RemoveParameterConfig(string name);

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IRequestConfig ToConfig();
}