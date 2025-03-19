// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Schemas.Configs.Parse.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;

namespace FunQL.Core.Schemas.Configs.Parse.Builders;

/// <summary>Default implementation of the <see cref="IParseConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class ParseConfigBuilder(
    IMutableParseConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IParseConfigBuilder
{
    /// <inheritdoc cref="IParseConfigBuilder.MutableConfig"/>
    public override IMutableParseConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IParseConfigBuilder WithConstantParserProvider(Func<ISchemaConfig, IConstantParser> provider)
    {
        MutableConfig.ConstantParserProvider = provider;
        return this;
    }

    /// <inheritdoc cref="IParseConfigBuilder.Build"/>
    public override IParseConfigExtension Build() => MutableConfig.ToConfig();
}