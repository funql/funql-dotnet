// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace WebApi.Json;

/// <summary>Extensions related to <see cref="JsonSerializerOptions"/>.</summary>
public static class JsonSerializerOptionsExtensions
{
    /// <summary>Configures the <paramref name="options"/> for this <see cref="WebApi"/> project.</summary>
    /// <param name="options">Options to configure.</param>
    /// <returns>The <paramref name="options"/> instance to continue building.</returns>
    public static JsonSerializerOptions ConfigureForWebApi(this JsonSerializerOptions options)
    {
        // Use camelCasing
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // Pretty-print by default 
        options.WriteIndented = true;
        // Configure NodaTime for serializing Instant types
        options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        return options;
    }
}