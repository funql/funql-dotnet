// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json.Interfaces;

/// <summary>Mutable version of <see cref="IJsonConfigExtension"/>.</summary>
public interface IMutableJsonConfigExtension : IJsonConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IJsonConfigExtension.JsonSerializerOptions"/>
    public new JsonSerializerOptions JsonSerializerOptions { get; set; }

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IJsonConfigExtension ToConfig();
}