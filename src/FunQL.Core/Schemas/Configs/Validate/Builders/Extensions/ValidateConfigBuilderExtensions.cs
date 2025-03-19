// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Common.Validators;
using FunQL.Core.Counting.Validators.Rules;
using FunQL.Core.Fields.Validators.Rules;
using FunQL.Core.Filtering.Validators.Rules;
using FunQL.Core.Inputting.Validators.Rules;
using FunQL.Core.Limiting.Validators.Rules;
using FunQL.Core.Requests.Validators.Rules;
using FunQL.Core.Schemas.Configs.Validate.Builders.Interfaces;
using FunQL.Core.Skipping.Validators.Rules;
using FunQL.Core.Sorting.Validators.Rules;

namespace FunQL.Core.Schemas.Configs.Validate.Builders.Extensions;

/// <summary>Extensions related to <see cref="IValidateConfigBuilder"/>.</summary>
public static class ValidateConfigBuilderExtensions
{
    /// <summary>Core validation rules.</summary>
    private static readonly IImmutableList<IValidationRule> CoreRules = ImmutableList.Create<IValidationRule>(
        // Requests
        new KnownRequests(),
        // Parameters
        new SupportedParameters(),
        new UniqueParameters(),
        new RequiredParameters(),
        // Skip
        new SkipHasIntConstant(),
        new SkipNotNegative(),
        // Limit
        new LimitHasIntConstant(),
        new LimitNotNegative(),
        new LimitLessThanOrEqualToMax(),
        // Count
        new CountHasBoolConstant(),
        // Filter
        new FilterSupportedForFields(),
        new FilterHasValidConstantTypes(),
        // Sort
        new SortSupportedForFields(),
        // Input
        new InputHasValidConstantType(),
        // Fields
        new KnownFieldPaths()
    );

    /// <summary>Adds all core <see cref="IValidationRule"/> to given <paramref name="builder"/>.</summary>
    /// <param name="builder">Builder to add all core <see cref="IValidationRule"/> to.</param>
    /// <returns>The builder to continue building.</returns>
    public static IValidateConfigBuilder WithCoreValidationRules(this IValidateConfigBuilder builder)
    {
        foreach (var rule in CoreRules)
        {
            builder.WithValidationRule(rule);
        }

        return builder;
    }
}