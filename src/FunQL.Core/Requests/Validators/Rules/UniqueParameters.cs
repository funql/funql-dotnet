// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Validators.Rules;

/// <summary>Validates that all <see cref="Request.Parameters"/> are unique.</summary>
public sealed class UniqueParameters : AbstractValidationRule<Request>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(Request node, IValidatorState state, CancellationToken cancellationToken)
    {
        var foundParameterNames = new HashSet<string>();
        foreach (var parameter in node.Parameters)
        {
            if (!foundParameterNames.Add(parameter.Name))
            {
                state.AddError(new ValidationError($"Parameter '{node.Name}' was defined more than once.", node));
            }
        }

        return Task.CompletedTask;
    }
}