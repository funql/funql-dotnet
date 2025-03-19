// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Json.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IJsonConfigExtension"/>.</summary>
public interface IJsonConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableJsonConfigExtension MutableConfig { get; }

    /// <summary>Configures the <see cref="IMutableJsonConfigExtension.JsonSerializerOptions"/>.</summary>
    /// <param name="jsonSerializerOptions">The options to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public IJsonConfigBuilder WithJsonSerializerOptions(JsonSerializerOptions jsonSerializerOptions);

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IJsonConfigExtension Build();
}