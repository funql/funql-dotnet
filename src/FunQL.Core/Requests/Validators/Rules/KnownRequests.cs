// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Schemas.Validators;

namespace FunQL.Core.Requests.Validators.Rules;

/// <summary>
/// Validates that <see cref="Request"/> is known for configured <see cref="SchemaConfigValidateContext.SchemaConfig"/>.
/// 
/// Requires <see cref="SchemaConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
/// <remarks>Enters <see cref="RequestConfigValidateContext"/> for nested rules to use.</remarks>
public sealed class KnownRequests : AbstractValidationRule<Request>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Request node, IValidatorState state, CancellationToken cancellationToken)
    {
        var schemaConfig = state.RequireContext<SchemaConfigValidateContext>().SchemaConfig;
        var requestConfig = schemaConfig.FindRequestConfig(node.Name);
        if (requestConfig == null)
        {
            state.AddError(new ValidationError($"Request '{node.Name}' is unknown.", node));

            // Can't validate an unknown Request, so stop validation
            return Task.FromException(new ValidationException(state.Errors));
        }

        // Add RequestConfig as context for nested nodes
        state.EnterContext(new RequestConfigValidateContext(requestConfig));
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public override Task ValidateOnExit(Request node, IValidatorState state, CancellationToken cancellationToken)
    {
        // Exit RequestConfigValidateContext, which is safe to do as we don't get here if OnEnter was not valid
        state.ExitContext();
        return Task.CompletedTask;
    }
}