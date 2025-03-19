// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Limiting.Configs.Extensions;
using FunQL.Core.Limiting.Configs.Interfaces;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Requests.Validators;

namespace FunQL.Core.Limiting.Validators.Rules;

/// <summary>
/// Validates that <see cref="Limit.Constant"/> value is less than or equal to configured
/// <see cref="ILimitConfigExtension.MaxLimit"/>.
/// </summary>
/// <remarks>Requires <see cref="RequestConfigValidateContext"/> to get <see cref="ILimitConfigExtension"/>.</remarks>
public sealed class LimitLessThanOrEqualToMax : AbstractValidationRule<Limit>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Limit node, IValidatorState state, CancellationToken cancellationToken)
    {
        var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
        if (node.Constant.Value is not int value)
            return Task.CompletedTask;

        var maxLimit = requestConfig.FindParameterConfig(Limit.FunctionName)?.FindLimitConfigExtension()?.MaxLimit
            ?? throw new InvalidOperationException("Can't validate limit without max limit defined");
        if (value > maxLimit)
        {
            state.AddError(new ValidationError(
                $"'{Limit.FunctionName}' value must be less than or equal to max value {maxLimit}.",
                node.Constant
            ));
        }

        return Task.CompletedTask;
    }
}