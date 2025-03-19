// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="ISchemaConfig"/>.</summary>
public interface IMutableSchemaConfig : IMutableExtensibleConfig, ISchemaConfig
{
    /// <summary>Gets list of all <see cref="IMutableRequestConfig"/>.</summary>
    /// <returns>List of all <see cref="IMutableRequestConfig"/>.</returns>
    public new IEnumerable<IMutableRequestConfig> GetRequestConfigs();

    /// <summary>
    /// Gets the <see cref="IMutableRequestConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IMutableRequestConfig"/>.</param>
    /// <returns>The <see cref="IMutableRequestConfig"/> or null if it does not exist.</returns>
    public new IMutableRequestConfig? FindRequestConfig(string name);

    /// <summary>Adds the <paramref name="requestConfig"/>.</summary>
    /// <param name="requestConfig">The <see cref="IMutableRequestConfig"/>.</param>
    public void AddRequestConfig(IMutableRequestConfig requestConfig);

    /// <summary>Removes the <see cref="IMutableRequestConfig"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="IMutableRequestConfig"/>.</param>
    /// <returns>The <see cref="IMutableRequestConfig"/> that was removed or null if it did not exist.</returns>
    public IMutableRequestConfig? RemoveRequestConfig(string name);

    /// <summary>Gets list of all <see cref="IMutableFunctionConfig"/>.</summary>
    /// <returns>List of all <see cref="IMutableFunctionConfig"/>.</returns>
    public new IEnumerable<IMutableFunctionConfig> GetFunctionConfigs();

    /// <summary>
    /// Gets the <see cref="IMutableFunctionConfig"/> for given <paramref name="name"/> or null if it does not exist.
    /// </summary>
    /// <param name="name">Name of the <see cref="IMutableFunctionConfig"/>.</param>
    /// <returns>The <see cref="IMutableFunctionConfig"/> or null if it does not exist.</returns>
    public new IMutableFunctionConfig? FindFunctionConfig(string name);

    /// <summary>Adds the <paramref name="functionConfig"/>.</summary>
    /// <param name="functionConfig">The <see cref="IMutableFunctionConfig"/>.</param>
    public void AddFunctionConfig(IMutableFunctionConfig functionConfig);

    /// <summary>Removes the <see cref="IMutableFunctionConfig"/> for given <paramref name="name"/>.</summary>
    /// <param name="name">Name of the <see cref="IMutableFunctionConfig"/>.</param>
    /// <returns>The <see cref="IMutableFunctionConfig"/> that was removed or null if it did not exist.</returns>
    public IMutableFunctionConfig? RemoveFunctionConfig(string name);

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new ISchemaConfig ToConfig();
}