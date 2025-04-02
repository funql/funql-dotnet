// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas;
using FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Json.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Parse.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Validate.Builders.Extensions;
using FunQL.Core.Schemas.Extensions;
using FunQL.Linq.Schemas.Configs.Linq.Builders.Extensions;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using WebApi.FunQL.EFCore;
using WebApi.FunQL.NodaTime;

namespace WebApi.FunQL;

/// <summary>The FunQL schema used by the application.</summary>
/// <param name="jsonOptions">
/// JSON options used by the ASP.NET HTTP pipeline, so FunQL can use the exact same <see cref="JsonSerializerOptions"/>.
/// </param>
/// <remarks>
/// The default <see cref="SchemaOptions"/> is passed to <see cref="Schema"/>, which configures certain core configs
/// used when parsing.
/// </remarks>
public class ApiSchema(IOptions<JsonOptions> jsonOptions) : Schema(new SchemaOptions())
{
    /// <summary>The <see cref="JsonSerializerOptions"/> to use with FunQL.</summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions = jsonOptions.Value.SerializerOptions;
    
    /// <inheritdoc/>
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // ===== Features =====
        // Add parse feature for parsing requests
        schema.AddParseFeature();
        // Add validate feature so requests can be validated using this configured schema
        schema.AddValidateFeature();
        // Add execute feature so requests can be executed using the execution pipeline
        schema.AddExecuteFeature(it =>
        {
            // Add the EntityFrameworkCoreExecuteLinqExecutionHandler so the specific EFCore methods are used 
            it.WithEntityFrameworkCoreExecuteLinqExecutionHandler();
        });
        // Add LINQ feature for translating requests to LINQ
        schema.AddLinqFeature(it =>
        {
            // Add the InstantFunctionLinqTranslator required for translating DateTime functions to LINQ for Instant
            // properties
            it.WithInstantFunctionLinqTranslator();
        });
        // Configure the JsonSerializerOptions FunQL uses
        schema.JsonConfig()
            .WithJsonSerializerOptions(_jsonSerializerOptions);
        
        // ===== Requests =====
        // Configure the Set requests
        schema.ApplyConfigurator(new SetSchemaConfigurator());
    }
}