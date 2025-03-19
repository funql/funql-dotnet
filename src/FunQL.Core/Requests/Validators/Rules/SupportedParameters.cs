// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Validators.Rules;

/// <summary>
/// Validates that all <see cref="Request.Parameters"/> are supported for current <see cref="Request"/>.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class SupportedParameters : AbstractValidationRule<Parameter>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Parameter node, IValidatorState state, CancellationToken cancellationToken)
    {
        var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
        var parameterConfig = requestConfig.FindParameterConfig(node.Name);
        if (parameterConfig?.IsSupported == true)
            return Task.CompletedTask;

        // Null or not supported, so error
        state.AddError(new ValidationError($"Parameter '{node.Name}' is not supported for request.", node));

        // Can't validate an unsupported Parameter Request, so stop validation
        return Task.FromException(new ValidationException(state.Errors));
    }
}