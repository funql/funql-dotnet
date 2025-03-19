// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for a request.</summary>
public interface IRequestConfig : IExtensibleConfig
{
    /// <summary>Name of the request.</summary>
    public string Name { get; }

    /// <summary>Config of the return type for this request.</summary>
    public ITypeConfig ReturnTypeConfig { get; }

    /// <summary>Gets list of all <see cref="IParameterConfig"/>.</summary>
    /// <returns>List of all <see cref="IParameterConfig"/>.</returns>
    public IEnumerable<IParameterConfig> GetParameterConfigs();

    /// <summary>
    /// Gets the <see cref="IParameterConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IParameterConfig"/>.</param>
    /// <returns>The <see cref="IParameterConfig"/> or null if it does not exist.</returns>
    public IParameterConfig? FindParameterConfig(string name);
}