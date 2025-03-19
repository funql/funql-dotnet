// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Validators.Rules;

/// <summary>
/// Validates that all <see cref="Request.Parameters"/> that are required for current <see cref="Request"/> are also
/// given.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class RequiredParameters : AbstractValidationRule<Request>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Request node, IValidatorState state, CancellationToken cancellationToken)
    {
        var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
        var givenParameterNames = node.Parameters
            .Select(it => it.Name)
            .ToHashSet();
        foreach (var parameterConfig in requestConfig.GetParameterConfigs())
        {
            if (parameterConfig.IsRequired && !givenParameterNames.Contains(parameterConfig.Name))
            {
                state.AddError(new ValidationError($"Parameter '{parameterConfig.Name}' is required."));
            }
        }

        return Task.CompletedTask;
    }
}