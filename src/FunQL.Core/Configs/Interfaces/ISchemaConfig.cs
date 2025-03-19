// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for a schema: The root configuration for all requests.</summary>
public interface ISchemaConfig : IExtensibleConfig
{
    /// <summary>Gets list of all <see cref="IRequestConfig"/>.</summary>
    /// <returns>List of all <see cref="IRequestConfig"/>.</returns>
    public IEnumerable<IRequestConfig> GetRequestConfigs();

    /// <summary>
    /// Gets the <see cref="IRequestConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IRequestConfig"/>.</param>
    /// <returns>The <see cref="IRequestConfig"/> or null if it does not exist.</returns>
    public IRequestConfig? FindRequestConfig(string name);

    /// <summary>Gets list of all <see cref="IFunctionConfig"/>.</summary>
    /// <returns>List of all <see cref="IFunctionConfig"/>.</returns>
    public IEnumerable<IFunctionConfig> GetFunctionConfigs();

    /// <summary>
    /// Gets the <see cref="IFunctionConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IFunctionConfig"/>.</param>
    /// <returns>The <see cref="IFunctionConfig"/> or null if it does not exist.</returns>
    public IFunctionConfig? FindFunctionConfig(string name);
}