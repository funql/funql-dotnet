// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Parse.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Print.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Validate.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Visit.Builders.Extensions;

namespace FunQL.Core.Schemas.Configs.Core.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfigBuilder"/>.</summary>
public static class SchemaConfigBuilderExtensions
{
    /// <summary>
    /// Adds all core features with their core configuration: Parse, Print, Validate, Visit and Execute.
    /// </summary>
    /// <param name="builder">Builder to configure the core features for.</param>
    /// <returns>The builder to continue building.</returns>
    public static ISchemaConfigBuilder AddCoreFeatures(this ISchemaConfigBuilder builder) => builder
        .AddParseFeature()
        .AddPrintFeature()
        .AddVisitFeature()
        .AddValidateFeature()
        .AddExecuteFeature();
}