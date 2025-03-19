// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Base class of a <see cref="IMutableExtensibleConfig"/>.</summary>
public abstract class MutableExtensibleConfig : MutableConfig, IMutableExtensibleConfig
{
    /// <summary>Current list with <see cref="IMutableConfigExtension"/>.</summary>
    protected readonly List<IMutableConfigExtension> Extensions = [];

    /// <inheritdoc/>
    public IEnumerable<IMutableConfigExtension> GetExtensions() => Extensions;

    /// <inheritdoc/>
    public IMutableConfigExtension? FindExtension(string name) => Extensions.FirstOrDefault(it => it.Name == name);

    /// <inheritdoc/>
    public void AddExtension(IMutableConfigExtension extension) => Extensions.Add(extension);

    /// <inheritdoc/>
    public IMutableConfigExtension? RemoveExtension(string name)
    {
        var extension = FindExtension(name);
        if (extension != null)
            Extensions.Remove(extension);

        return extension;
    }

    /// <inheritdoc cref="IMutableExtensibleConfig.ToConfig"/>
    public abstract override IExtensibleConfig ToConfig();

    #region IExtensibleConfig

    /// <inheritdoc/>
    IEnumerable<IConfigExtension> IExtensibleConfig.GetExtensions() => GetExtensions();

    /// <inheritdoc/>
    IConfigExtension? IExtensibleConfig.FindExtension(string name) => FindExtension(name);

    #endregion
}