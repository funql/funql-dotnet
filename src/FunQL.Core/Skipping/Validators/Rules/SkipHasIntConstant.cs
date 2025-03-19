// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Exceptions;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Validators.Rules;

/// <summary>Validates that <see cref="Skip.Constant"/> value is an <see cref="int"/>.</summary>
public sealed class SkipHasIntConstant : AbstractValidationRule<Skip>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Skip node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node.Constant.Value is int)
            return Task.CompletedTask;

        // Not an int, so error
        state.AddError(new ValidationError($"'{Skip.FunctionName}' value must be an integer.", node.Constant));

        // Can't validate invalid value, so stop validation
        return Task.FromException(new ValidationException(state.Errors));
    }
}