// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Common.Validators;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Extensions;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;
using FunQL.Core.Schemas.Validators;

namespace FunQL.Core.Schemas.Configs.Validate;

/// <summary>
/// Default implementation of <see cref="IMutableValidateConfigExtension"/>.
///
/// Properties will have the following defaults:
///   - Providers will be initialized as singleton using their default implementation
///   - <see cref="IVisitConfigExtension.RequestVisitorProvider"/> will be used for <see cref="Validator"/>
///   - <see cref="ValidatorStateFactory"/> will enter <see cref="SchemaConfigValidateContext"/>
/// </summary>
/// <inheritdoc cref="MutableConfigExtension"/>
public sealed class MutableValidateConfigExtension : MutableConfigExtension, IMutableValidateConfigExtension
{
    /// <summary>Current list of rules.</summary>
    private readonly HashSet<IValidationRule> _rules = [];

    /// <summary>Initializes properties.</summary>
    /// <param name="name">Name of this extension.</param>
    public MutableValidateConfigExtension(string name) : base(name)
    {
        IValidator? validator = null;
        ValidatorProvider = schemaConfig => validator ??= new Validator(
            schemaConfig.FindVisitConfigExtension()?.RequestVisitorProvider(schemaConfig)
            ?? throw new InvalidOperationException(
                "IVisitConfigExtension is required when using the default ValidatorProvider"
            )
        );

        ValidatorStateFactory = schemaConfig =>
        {
            var state = new ValidatorState(_rules.ToArray());
            state.EnterContext(new SchemaConfigValidateContext(schemaConfig));
            return state;
        };
    }

    /// <inheritdoc cref="IMutableValidateConfigExtension.ValidatorProvider"/>
    public Func<ISchemaConfig, IValidator> ValidatorProvider { get; set; }

    /// <inheritdoc cref="IMutableValidateConfigExtension.ValidatorStateFactory"/>
    public Func<ISchemaConfig, IValidatorState> ValidatorStateFactory { get; set; }

    /// <inheritdoc/>
    public IEnumerable<IValidationRule> GetValidationRules() => _rules;

    /// <inheritdoc/>
    public void AddValidationRule(IValidationRule rule)
    {
        _rules.Add(rule);
    }

    /// <inheritdoc/>
    public void RemoveValidationRule(IValidationRule rule)
    {
        _rules.Remove(rule);
    }

    /// <inheritdoc cref="IMutableValidateConfigExtension.ToConfig"/>
    public override IValidateConfigExtension ToConfig() => new ImmutableValidateConfigExtension(
        Name, ValidatorProvider, ValidatorStateFactory, _rules.ToImmutableList()
    );

    #region IMutableConfigExtension

    /// <inheritdoc/>
    IConfigExtension IMutableConfigExtension.ToConfig() => ToConfig();

    #endregion
}