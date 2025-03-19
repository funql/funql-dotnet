// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Schemas.Configs.Validate.Interfaces;

namespace FunQL.Core.Schemas.Configs.Validate.Extensions;

/// <summary>Extensions related to <see cref="Schema"/> and validating requests.</summary>
public static class SchemaExtensions
{
    /// <summary>
    /// Validates the given <paramref name="request"/> using the configured <see cref="IValidateConfigExtension"/>,
    /// throwing <see cref="ValidationException"/> if <paramref name="request"/> is not valid.
    /// </summary>
    /// <param name="schema">The schema to validate request with.</param>
    /// <param name="request">The request to validate.</param>
    /// <param name="cancellationToken">Token to cancel async tasks.</param>
    /// <exception cref="InvalidOperationException">
    /// If the <see cref="IValidateConfigExtension"/> is not found.
    /// </exception>
    /// <exception cref="ValidationException">If the <paramref name="request"/> is not valid.</exception>
    public static Task ValidateRequest(
        this Schema schema,
        Request request,
        CancellationToken cancellationToken = default
    ) => schema.SchemaConfig.ValidateRequest(request, cancellationToken);
}