// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas;

/// <summary>
/// The schema represents the entrypoint for FunQL, defining the configuration needed by FunQL to parse, validate and
/// execute requests. Implement this class and configure FunQL using <see cref="OnInitializeSchema"/> and
/// <see cref="OnFinalizeSchema"/>.
/// </summary>
public abstract class Schema
{
    /// <summary>Schema options to use for configuring the config.</summary>
    private readonly ISchemaOptions _schemaOptions;

    /// <summary>The <see cref="ISchemaConfig"/> that's lazily created when accessed.</summary>
    /// <remarks>
    /// Using <see cref="Lazy{T}"/> here, which uses double-checked locking, to make sure <see cref="ISchemaConfig"/> is
    /// only created once and is thread safe.
    /// </remarks>
    /// <seealso cref="CreateSchemaConfig"/>
    private readonly Lazy<ISchemaConfig> _lazySchemaConfig;

    /// <summary>Initializes properties with default <see cref="SchemaOptions"/>.</summary>
    protected Schema() : this(new SchemaOptions())
    {
    }

    /// <summary>Initializes properties.</summary>
    /// <param name="schemaOptions">The schema options to use for configuring the config.</param>
    protected Schema(ISchemaOptions schemaOptions)
    {
        _schemaOptions = schemaOptions;
        _lazySchemaConfig = new Lazy<ISchemaConfig>(CreateSchemaConfig);
    }

    /// <summary>Configured schema config.</summary>
    public ISchemaConfig SchemaConfig => _lazySchemaConfig.Value;

    /// <summary>Creates and configures the <see cref="ISchemaConfig"/>.</summary>
    /// <returns>The configured <see cref="ISchemaConfig"/>.</returns>
    private ISchemaConfig CreateSchemaConfig()
    {
        var builder = new SchemaConfigBuilder(new MutableSchemaConfig());
        foreach (var convention in _schemaOptions.OnInitializeConfigurators)
        {
            convention.Configure(builder);
        }

        OnInitializeSchema(builder);

        foreach (var convention in _schemaOptions.OnFinalizeConfigurators)
        {
            convention.Configure(builder);
        }

        OnFinalizeSchema(builder);

        return builder.Build();
    }

    /// <summary>
    /// Initialize the <see cref="ISchemaConfig"/> using given <paramref name="builder"/>. This can be used to configure
    /// your requests and custom settings.
    /// </summary>
    /// <param name="builder">Builder to configure config with.</param>
    /// <remarks>This is called after <see cref="ISchemaOptions.OnInitializeConfigurators"/>.</remarks>
    protected virtual void OnInitializeSchema(ISchemaConfigBuilder builder)
    {
    }

    /// <summary>
    /// Finalize the <see cref="ISchemaConfig"/> using given <paramref name="builder"/>. This can e.g. be used to edit
    /// settings that may have been set by conventions.
    /// </summary>
    /// <param name="builder">Builder to configure config with.</param>
    /// <remarks>
    /// This is called after <see cref="OnInitializeSchema"/> and <see cref="ISchemaOptions.OnFinalizeConfigurators"/>.
    /// </remarks>
    protected virtual void OnFinalizeSchema(ISchemaConfigBuilder builder)
    {
    }
}