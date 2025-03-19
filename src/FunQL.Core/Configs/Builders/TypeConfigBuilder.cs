// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="ITypeConfigBuilder"/>.</summary>
/// <inheritdoc cref="ExtensibleConfigBuilder"/>
public abstract class TypeConfigBuilder(
    IMutableTypeConfig mutableConfig
) : ExtensibleConfigBuilder(mutableConfig), ITypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder.MutableConfig"/>
    public override IMutableTypeConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public virtual ITypeConfigBuilder HasType(Type type)
    {
        MutableConfig.Type = type;
        return this;
    }

    /// <inheritdoc/>
    public virtual ITypeConfigBuilder IsNullable(bool nullable = true)
    {
        MutableConfig.IsNullable = nullable;
        return this;
    }

    /// <inheritdoc cref="ITypeConfigBuilder.MutableConfig"/>
    public abstract override ITypeConfig Build();
}