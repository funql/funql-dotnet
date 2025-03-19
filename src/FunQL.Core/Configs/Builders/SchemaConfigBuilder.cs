// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="ISchemaConfigBuilder"/>.</summary>
/// <inheritdoc cref="ExtensibleConfigBuilder"/>
public sealed class SchemaConfigBuilder(
    IMutableSchemaConfig mutableConfig
) : ExtensibleConfigBuilder(mutableConfig), ISchemaConfigBuilder
{
    /// <inheritdoc cref="ISchemaConfigBuilder.MutableConfig"/>
    public override IMutableSchemaConfig MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IRequestConfigBuilder Request(string name)
    {
        var config = MutableConfig.FindRequestConfig(name);
        if (config == null)
        {
            config = new MutableRequestConfig(name, new MutableSimpleTypeConfig(typeof(void)));
            MutableConfig.AddRequestConfig(config);
        }

        return new RequestConfigBuilder(config);
    }

    /// <inheritdoc cref="ISchemaConfigBuilder.Build"/>
    public override ISchemaConfig Build() => MutableConfig.ToConfig();
}