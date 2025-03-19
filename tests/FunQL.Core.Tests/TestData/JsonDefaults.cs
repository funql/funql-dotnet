// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunQL.Core.Tests.TestData;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper, false) }
    };
}