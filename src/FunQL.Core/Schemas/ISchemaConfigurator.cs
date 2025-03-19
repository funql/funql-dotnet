// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas;

/// <summary>
/// Represents a configurator to configure (part of) the <see cref="ISchemaConfig"/>.
///
/// This can be used to split up the configuration of <see cref="ISchemaConfig"/> in multiple smaller configurators. For
/// example, use it to configure a single <see cref="ISchemaConfigBuilder.Request"/>.
/// </summary>
public interface ISchemaConfigurator
{
    /// <summary>Configures the <see cref="ISchemaConfig"/> using given <paramref name="builder"/>.</summary>
    /// <param name="builder">Builder to configure <see cref="ISchemaConfig"/>.</param>
    public void Configure(ISchemaConfigBuilder builder);
}