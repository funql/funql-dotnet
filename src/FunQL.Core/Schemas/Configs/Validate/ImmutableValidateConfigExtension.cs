// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Common.Validators;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate;

/// <summary>Immutable implementation of <see cref="IValidateConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="ValidatorProvider">The <see cref="IValidateConfigExtension.ValidatorProvider"/>.</param>
/// <param name="ValidatorStateFactory">The <see cref="IValidateConfigExtension.ValidatorStateFactory"/>.</param>
/// <param name="ValidationRules">The <see cref="IValidateConfigExtension.GetValidationRules"/>.</param>
public sealed record ImmutableValidateConfigExtension(
    string Name,
    Func<ISchemaConfig, IValidator> ValidatorProvider,
    Func<ISchemaConfig, IValidatorState> ValidatorStateFactory,
    IImmutableList<IValidationRule> ValidationRules
) : ImmutableConfigExtension(Name), IValidateConfigExtension
{
    /// <inheritdoc/>
    public IEnumerable<IValidationRule> GetValidationRules() => ValidationRules;
}