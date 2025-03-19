// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IFieldConfigBuilder{T}"/>.</summary>
/// <inheritdoc cref="IFieldConfigBuilder{T}"/>
public abstract class FieldConfigBuilder<T> : IFieldConfigBuilder<T>
{
    /// <summary>Underlying <see cref="IFieldConfigBuilder"/> we can delegate build logic to.</summary>
    protected abstract IFieldConfigBuilder Builder { get; }

    /// <inheritdoc/>
    public IMutableFieldConfig MutableConfig => Builder.MutableConfig;

    /// <inheritdoc/>
    public virtual IFieldConfigBuilder<T> HasName(string name)
    {
        Builder.HasName(name);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldConfigBuilder<T> HasType(Type type)
    {
        Builder.HasType(type);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldConfigBuilder<T> IsNullable(bool nullable = true)
    {
        Builder.IsNullable(nullable);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldConfig Build() => Builder.Build();

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

    #region IFieldConfigBuilder

    /// <inheritdoc/>
    IFieldConfigBuilder IFieldConfigBuilder.HasName(string name) => HasName(name);

    /// <inheritdoc/>
    IFieldConfigBuilder IFieldConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    IFieldConfigBuilder IFieldConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    #endregion
}