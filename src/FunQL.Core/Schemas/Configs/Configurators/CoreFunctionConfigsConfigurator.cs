// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Configs.Configurators;
using FunQL.Core.Filtering.Configs.Configurators;
using FunQL.Core.Schemas.Extensions;

namespace FunQL.Core.Schemas.Configs.Configurators;

/// <summary>Configures the core <see cref="ISchemaConfig.GetFunctionConfigs"/>.</summary>
public sealed class CoreFunctionConfigsConfigurator : ISchemaConfigurator
{
    /// <inheritdoc/>
    public void Configure(ISchemaConfigBuilder builder)
    {
        builder.ApplyConfigurator(new FieldFunctionSchemaConfigurator());
        builder.ApplyConfigurator(new FilterFunctionSchemaConfigurator());
    }
}