// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;

namespace FunQL.Core.Schemas.Configs.Parse.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IParseConfigExtension"/>.</summary>
public interface IParseConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableParseConfigExtension MutableConfig { get; }

    /// <summary>Configures the <see cref="IMutableParseConfigExtension.ConstantParserProvider"/>.</summary>
    /// <param name="provider">The provider to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public IParseConfigBuilder WithConstantParserProvider(Func<ISchemaConfig, IConstantParser> provider);

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IParseConfigExtension Build();
}