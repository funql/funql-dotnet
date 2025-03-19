// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs;
using FunQL.Core.Schemas.Configs.Json.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json;

/// <summary>Immutable implementation of <see cref="IJsonConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="JsonSerializerOptions">Serializer options to use for JSON.</param>
public sealed record ImmutableJsonConfigExtension(
    string Name,
    JsonSerializerOptions JsonSerializerOptions
) : ImmutableConfigExtension(Name), IJsonConfigExtension;