// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IListTypeConfigBuilder"/>.</summary>
/// <inheritdoc cref="TypeConfigBuilder"/>
public sealed class ListTypeConfigBuilder(
    IMutableListTypeConfig mutableConfig
) : TypeConfigBuilder(mutableConfig), IListTypeConfigBuilder
{
    /// <inheritdoc cref="IListTypeConfigBuilder.MutableConfig"/>
    public override IMutableListTypeConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc cref="IListTypeConfigBuilder.HasType"/>
    public override IListTypeConfigBuilder HasType(Type type) => (IListTypeConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="IListTypeConfigBuilder.IsNullable"/>
    public override IListTypeConfigBuilder IsNullable(bool nullable = true) =>
        (IListTypeConfigBuilder)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleTypeConfigBuilder SimpleItem(Type type)
    {
        switch (MutableConfig.ElementTypeConfig)
        {
            case MutableSimpleTypeConfig typedConfig:
                typedConfig.Type = type;
                return new SimpleTypeConfigBuilder(typedConfig);
            default:
                var config = new MutableSimpleTypeConfig(type);
                MutableConfig.ElementTypeConfig = config;
                return new SimpleTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc/>
    public IObjectTypeConfigBuilder ObjectItem(Type type)
    {
        switch (MutableConfig.ElementTypeConfig)
        {
            case MutableObjectTypeConfig typedConfig:
                typedConfig.Type = type;
                return new ObjectTypeConfigBuilder(typedConfig);
            default:
                var config = new MutableObjectTypeConfig(type);
                MutableConfig.ElementTypeConfig = config;
                return new ObjectTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc/>
    public IListTypeConfigBuilder ListItem(Type type)
    {
        switch (MutableConfig.ElementTypeConfig)
        {
            case MutableListTypeConfig typedConfig:
                typedConfig.Type = type;
                return new ListTypeConfigBuilder(typedConfig);
            default:
                var elementTypeConfig = type.IsCollectionType(out var elementType)
                    ? new MutableSimpleTypeConfig(elementType)
                    // Can't resolve element type, so fall back to void
                    : new MutableSimpleTypeConfig(typeof(void));
                var config = new MutableListTypeConfig(type, elementTypeConfig);
                MutableConfig.ElementTypeConfig = config;
                return new ListTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc cref="IListTypeConfigBuilder.Build"/>
    public override IListTypeConfig Build() => MutableConfig.ToConfig();
}