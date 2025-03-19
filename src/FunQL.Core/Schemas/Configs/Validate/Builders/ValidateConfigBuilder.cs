// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Schemas.Configs.Validate.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Builders;

/// <summary>Default implementation of the <see cref="IValidateConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public sealed class ValidateConfigBuilder(
    IMutableValidateConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IValidateConfigBuilder
{
    /// <inheritdoc cref="IValidateConfigBuilder.MutableConfig"/>
    public override IMutableValidateConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public IValidateConfigBuilder WithValidationRule(IValidationRule rule)
    {
        MutableConfig.AddValidationRule(rule);
        return this;
    }

    /// <inheritdoc cref="IValidateConfigBuilder.Build"/>
    public override IValidateConfigExtension Build() => MutableConfig.ToConfig();
}