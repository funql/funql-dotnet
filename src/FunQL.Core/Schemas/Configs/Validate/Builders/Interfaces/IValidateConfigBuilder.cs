// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IValidateConfigExtension"/>.</summary>
public interface IValidateConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableValidateConfigExtension MutableConfig { get; }

    /// <summary>Adds <paramref name="rule"/> to <see cref="IValidateConfigExtension.GetValidationRules"/>.</summary>
    /// <param name="rule">The rule to add.</param>
    /// <returns>The builder to continue building.</returns>
    public IValidateConfigBuilder WithValidationRule(IValidationRule rule);

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IValidateConfigExtension Build();
}