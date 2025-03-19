// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Schemas.Configs.Json.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Json.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json.Builders;

/// <summary>Default implementation of the <see cref="IJsonConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class JsonConfigBuilder(
    IMutableJsonConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IJsonConfigBuilder
{
    /// <inheritdoc cref="IJsonConfigBuilder.MutableConfig"/>
    public override IMutableJsonConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IJsonConfigBuilder WithJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions)
    {
        MutableConfig.JsonSerializerOptions = jsonSerializerOptions;
        return this;
    }

    /// <inheritdoc cref="IJsonConfigBuilder.Build"/>
    public override IJsonConfigExtension Build() => MutableConfig.ToConfig();
}