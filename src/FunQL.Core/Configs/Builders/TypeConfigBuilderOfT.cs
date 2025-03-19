// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="ITypeConfigBuilder{T}"/>.</summary>
/// <inheritdoc cref="ITypeConfigBuilder{T}"/>
public abstract class TypeConfigBuilder<T> : ITypeConfigBuilder<T>
{
    /// <summary>Underlying <see cref="ITypeConfigBuilder"/> we can delegate build logic to.</summary>
    protected abstract ITypeConfigBuilder Builder { get; }

    /// <inheritdoc/>
    public abstract IMutableTypeConfig MutableConfig { get; }

    /// <inheritdoc/>
    public virtual ITypeConfigBuilder<T> HasType(Type type)
    {
        Builder.HasType(type);
        return this;
    }

    /// <inheritdoc/>
    public virtual ITypeConfigBuilder<T> IsNullable(bool nullable = true)
    {
        Builder.IsNullable(nullable);
        return this;
    }

    /// <inheritdoc/>
    public virtual ITypeConfig Build() => Builder.Build();

    #region IConfigBuilder

    /// <inheritdoc/>
    IMutableConfig IConfigBuilder.MutableConfig => MutableConfig;

    /// <inheritdoc/>
    IConfig IConfigBuilder.Build() => Build();

    #endregion

    #region IExtensibleConfigBuilder

    /// <inheritdoc/>
    IMutableExtensibleConfig IExtensibleConfigBuilder.MutableConfig => MutableConfig;

    /// <inheritdoc/>
    IExtensibleConfig IExtensibleConfigBuilder.Build() => Build();

    #endregion

    #region ITypeConfigBuilder

    /// <inheritdoc/>
    ITypeConfigBuilder ITypeConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    ITypeConfigBuilder ITypeConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    #endregion
}