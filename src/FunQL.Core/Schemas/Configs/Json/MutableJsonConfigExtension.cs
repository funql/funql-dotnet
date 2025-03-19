// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Json.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json;

/// <summary>Default implementation of <see cref="IMutableJsonConfigExtension"/>.</summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableJsonConfigExtension(
    string name
) : MutableConfigExtension(name), IMutableJsonConfigExtension
{
    /// <inheritdoc cref="IMutableJsonConfigExtension.JsonSerializerOptions"/>
    public JsonSerializerOptions JsonSerializerOptions { get; set; } = JsonSerializerOptions.Default;

    /// <inheritdoc cref="IMutableJsonConfigExtension.JsonSerializerOptions"/>
    public override IJsonConfigExtension ToConfig() => new ImmutableJsonConfigExtension(Name, JsonSerializerOptions);

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}