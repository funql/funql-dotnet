// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Interfaces;

/// <summary>Extension specifying the configuration for validating requests.</summary>
public interface IValidateConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.ValidateConfigExtension";

    /// <summary>Provider for the <see cref="IValidator"/>.</summary>
    public Func<ISchemaConfig, IValidator> ValidatorProvider { get; }

    /// <summary>Factory to create the <see cref="IValidatorState"/>.</summary>
    public Func<ISchemaConfig, IValidatorState> ValidatorStateFactory { get; }

    /// <summary>The list of <see cref="IValidationRule"/> to use for validating.</summary>
    public IEnumerable<IValidationRule> GetValidationRules();
}