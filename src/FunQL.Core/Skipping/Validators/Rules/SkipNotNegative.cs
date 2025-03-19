// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Validators.Rules;

/// <summary>Validates that <see cref="Skip.Constant"/> value is not negative.</summary>
public sealed class SkipNotNegative : AbstractValidationRule<Skip>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Skip node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node.Constant.Value is not int value)
            return Task.CompletedTask;

        if (value < 0)
        {
            state.AddError(new ValidationError(
                $"'{Skip.FunctionName}' value must be greater than or equal to 0.",
                node.Constant
            ));
        }

        return Task.CompletedTask;
    }
}