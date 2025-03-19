// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Configurators;

namespace FunQL.Core.Schemas;

/// <summary>Default implementation of <see cref="ISchemaOptions"/>.</summary>
public record SchemaOptions : ISchemaOptions
{
    /// <summary>Initializes properties.</summary>
    /// <param name="configureCoreFunctions">
    /// Whether to configure the <see cref="IFunctionConfig"/> for the core functions. Default <c>true</c>.
    /// </param>
    public SchemaOptions(bool configureCoreFunctions = true)
    {
        if (configureCoreFunctions)
            OnFinalizeConfigurators = [new CoreFunctionConfigsConfigurator()];
    }

    /// <inheritdoc/>
    public IImmutableList<ISchemaConfigurator> OnInitializeConfigurators { get; init; } =
        ImmutableList<ISchemaConfigurator>.Empty;

    /// <inheritdoc/>
    public IImmutableList<ISchemaConfigurator> OnFinalizeConfigurators { get; init; } =
        ImmutableList<ISchemaConfigurator>.Empty;
}