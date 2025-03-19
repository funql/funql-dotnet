// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Counting.Nodes;

namespace FunQL.Core.Counting.Validators.Rules;

/// <summary>Validates that <see cref="Count.Constant"/> value is a <see cref="bool"/>.</summary>
public sealed class CountHasBoolConstant : AbstractValidationRule<Count>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Count node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node.Constant.Value is bool)
            return Task.CompletedTask;

        // Not a bool, so error
        state.AddError(new ValidationError($"'{Count.FunctionName}' value must be a boolean.", node.Constant));

        // Can't validate invalid value, so stop validation
        return Task.FromException(new ValidationException(state.Errors));
    }
}