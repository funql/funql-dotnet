// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IRequestConfigBuilder"/>.</summary>
/// <inheritdoc cref="ExtensibleConfigBuilder"/>
public sealed class RequestConfigBuilder(
    IMutableRequestConfig mutableConfig
) : ExtensibleConfigBuilder(mutableConfig), IRequestConfigBuilder
{
    /// <inheritdoc cref="IRequestConfigBuilder.MutableConfig"/>
    public override IMutableRequestConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IRequestConfigBuilder HasName(string name)
    {
        MutableConfig.Name = name;
        return this;
    }

    /// <inheritdoc/>
    public ISimpleTypeConfigBuilder SimpleReturn(Type type)
    {
        switch (MutableConfig.ReturnTypeConfig)
        {
            case MutableSimpleTypeConfig typedConfig:
                typedConfig.Type = type;
                return new SimpleTypeConfigBuilder(typedConfig);
            default:
                var config = new MutableSimpleTypeConfig(type);
                MutableConfig.ReturnTypeConfig = config;
                return new SimpleTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc/>
    public IObjectTypeConfigBuilder ObjectReturn(Type type)
    {
        switch (MutableConfig.ReturnTypeConfig)
        {
            case MutableObjectTypeConfig typedConfig:
                typedConfig.Type = type;
                return new ObjectTypeConfigBuilder(typedConfig);
            default:
                var config = new MutableObjectTypeConfig(type);
                MutableConfig.ReturnTypeConfig = config;
                return new ObjectTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc/>
    public IListTypeConfigBuilder ListReturn(Type type)
    {
        switch (MutableConfig.ReturnTypeConfig)
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
                MutableConfig.ReturnTypeConfig = config;
                return new ListTypeConfigBuilder(config);
        }
    }

    /// <inheritdoc cref="IRequestConfigBuilder.Build"/>
    public override IRequestConfig Build() => MutableConfig.ToConfig();
}