// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Interfaces;

/// <summary>Mutable version of <see cref="IValidateConfigExtension"/>.</summary>
public interface IMutableValidateConfigExtension : IValidateConfigExtension, IMutableConfigExtension
{
    /// <inheritdoc cref="IValidateConfigExtension.ValidatorProvider"/>
    public new Func<ISchemaConfig, IValidator> ValidatorProvider { get; set; }

    /// <inheritdoc cref="IValidateConfigExtension.ValidatorStateFactory"/>
    public new Func<ISchemaConfig, IValidatorState> ValidatorStateFactory { get; set; }

    /// <summary>Adds given <paramref name="rule"/>.</summary>
    /// <param name="rule">Rule to add.</param>
    public void AddValidationRule(IValidationRule rule);

    /// <summary>Removes given <paramref name="rule"/>.</summary>
    /// <param name="rule">Rule to remove.</param>
    public void RemoveValidationRule(IValidationRule rule);

    /// <inheritdoc cref="IMutableConfigExtension.ToConfig"/>
    public new IValidateConfigExtension ToConfig();
}