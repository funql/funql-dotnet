// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IObjectTypeConfigBuilder"/>.</summary>
/// <inheritdoc cref="TypeConfigBuilder"/>
public sealed class ObjectTypeConfigBuilder(
    IMutableObjectTypeConfig mutableConfig
) : TypeConfigBuilder(mutableConfig), IObjectTypeConfigBuilder
{
    /// <inheritdoc cref="IObjectTypeConfigBuilder.MutableConfig"/>
    public override IMutableObjectTypeConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IObjectTypeConfigBuilder.HasType"/>
    public override IObjectTypeConfigBuilder HasType(Type type) => (IObjectTypeConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="IObjectTypeConfigBuilder.IsNullable"/>
    public override IObjectTypeConfigBuilder IsNullable(bool nullable = true) =>
        (IObjectTypeConfigBuilder)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleFieldConfigBuilder SimpleField(string name, Type type)
    {
        var config = MutableConfig.FindFieldConfig(name);
        if (config == null)
        {
            config = new MutableFieldConfig(name, new MutableSimpleTypeConfig(type));
            MutableConfig.AddFieldConfig(config);
        }
        else if (config.TypeConfig is not MutableSimpleTypeConfig)
        {
            throw new InvalidOperationException($"Simple field '{name}' previously configured as different field type");
        }

        return new SimpleFieldConfigBuilder(config);
    }

    /// <inheritdoc/>
    public IObjectFieldConfigBuilder ObjectField(string name, Type type)
    {
        var config = MutableConfig.FindFieldConfig(name);
        if (config == null)
        {
            config = new MutableFieldConfig(name, new MutableObjectTypeConfig(type));
            MutableConfig.AddFieldConfig(config);
        }
        else if (config.TypeConfig is not MutableObjectTypeConfig)
        {
            throw new InvalidOperationException($"Simple field '{name}' previously configured as different field type");
        }

        return new ObjectFieldConfigBuilder(config);
    }

    /// <inheritdoc/>
    public IListFieldConfigBuilder ListField(string name, Type type)
    {
        var config = MutableConfig.FindFieldConfig(name);
        if (config == null)
        {
            var elementTypeConfig = type.IsCollectionType(out var elementType)
                ? new MutableSimpleTypeConfig(elementType)
                // Can't resolve element type, so fall back to void
                : new MutableSimpleTypeConfig(typeof(void));
            config = new MutableFieldConfig(name, new MutableListTypeConfig(type, elementTypeConfig));
            MutableConfig.AddFieldConfig(config);
        }
        else if (config.TypeConfig is not MutableListTypeConfig)
        {
            throw new InvalidOperationException($"Simple field '{name}' previously configured as different field type");
        }

        return new ListFieldConfigBuilder(config);
    }

    /// <inheritdoc cref="IObjectTypeConfigBuilder.Build"/>
    public override IObjectTypeConfig Build() => MutableConfig.ToConfig();
}