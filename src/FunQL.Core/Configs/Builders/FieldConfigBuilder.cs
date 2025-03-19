// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Base class of the <see cref="IFieldConfigBuilder"/>.</summary>
/// <inheritdoc cref="ExtensibleConfigBuilder"/>
public abstract class FieldConfigBuilder(
    IMutableFieldConfig mutableConfig
) : ExtensibleConfigBuilder(mutableConfig), IFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder.MutableConfig"/>
    public override IMutableFieldConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IFieldConfigBuilder.HasName"/>
    public virtual IFieldConfigBuilder HasName(string name)
    {
        MutableConfig.Name = name;
        return this;
    }

    /// <inheritdoc cref="IFieldConfigBuilder.HasType"/>
    public virtual IFieldConfigBuilder HasType(Type type)
    {
        MutableConfig.TypeConfig.Type = type;
        return this;
    }

    /// <inheritdoc cref="IFieldConfigBuilder.IsNullable"/>
    public virtual IFieldConfigBuilder IsNullable(bool nullable = true)
    {
        MutableConfig.TypeConfig.IsNullable = nullable;
        return this;
    }

    /// <inheritdoc cref="IFieldConfigBuilder.Build"/>
    public override IFieldConfig Build() => MutableConfig.ToConfig();
}