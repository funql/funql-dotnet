// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Inputting.Configs.Extensions;
using FunQL.Core.Inputting.Nodes;
using FunQL.Core.Requests.Validators;

namespace FunQL.Core.Inputting.Validators.Rules;

/// <summary>
/// Validates that <see cref="Input"/> node has valid <see cref="Input.Constant"/> type.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class InputHasValidConstantType : AbstractValidationRule<Input>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Input node, IValidatorState state, CancellationToken cancellationToken)
    {
        var expectedTypeConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig
            .FindInputParameterConfig()
            ?.FindInputConfigExtension()
            ?.TypeConfig;

        // If config not found, input may not be supported, which should be validated by some other rule
        if (expectedTypeConfig == null)
            return Task.CompletedTask;

        var value = node.Constant.Value;

        if (value == null)
        {
            if (!expectedTypeConfig.IsNullable)
            {
                state.AddError(new ValidationError($"'{Input.FunctionName}' value can not be 'null'.", node));
            }
        }
        else if (value.GetType() != expectedTypeConfig.Type)
        {
            state.AddError(new ValidationError($"'{Input.FunctionName}' value is invalid.", node));
        }

        return Task.CompletedTask;
    }
}