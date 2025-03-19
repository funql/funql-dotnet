// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Validators.Rules;

/// <summary>Validates that <see cref="Limit.Constant"/> value is an <see cref="int"/>.</summary>
public sealed class LimitHasIntConstant : AbstractValidationRule<Limit>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Limit node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node.Constant.Value is int)
            return Task.CompletedTask;

        // Not an int, so error
        state.AddError(new ValidationError($"'{Limit.FunctionName}' value must be an integer.", node.Constant));

        // Can't validate invalid value, so stop validation
        return Task.FromException(new ValidationException(state.Errors));
    }
}