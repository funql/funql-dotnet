// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Json.Interfaces;

/// <summary>Extension for specifying config values related to JSON.</summary>
public interface IJsonConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.JsonConfigExtension";

    /// <summary>Serializer options to use for JSON.</summary>
    public JsonSerializerOptions JsonSerializerOptions { get; }
}