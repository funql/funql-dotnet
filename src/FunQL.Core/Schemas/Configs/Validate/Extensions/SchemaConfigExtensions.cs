// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/>.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IValidateConfigExtension"/> for <see cref="IValidateConfigExtension.DefaultName"/> or
    /// <c>null</c> if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="IValidateConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IValidateConfigExtension? FindValidateConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<IValidateConfigExtension>(IValidateConfigExtension.DefaultName);

    /// <summary>
    /// Validates the given <paramref name="request"/> using the configured <see cref="IValidateConfigExtension"/>,
    /// throwing <see cref="ValidationException"/> if <paramref name="request"/> is not valid.
    /// </summary>
    /// <param name="schemaConfig">The schema config to validate request with.</param>
    /// <param name="request">The request to validate.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <exception cref="InvalidOperationException">
    /// If the <see cref="IValidateConfigExtension"/> is not found.
    /// </exception>
    /// <exception cref="ValidationException">If the <paramref name="request"/> is not valid.</exception>
    public static async Task ValidateRequest(
        this ISchemaConfig schemaConfig,
        Request request, 
        CancellationToken cancellationToken = default
    )
    {
        var validateConfig = schemaConfig.FindValidateConfigExtension()
            ?? throw new InvalidOperationException("No validate config found");

        // Prepare validation
        var validator = validateConfig.ValidatorProvider(schemaConfig);
        var validatorState = validateConfig.ValidatorStateFactory(schemaConfig);

        // Validate
        await validator.Validate(request, validatorState, cancellationToken);

        // Throw on errors
        if (validatorState.HasErrors)
            throw new ValidationException(validatorState.Errors);
    }
}