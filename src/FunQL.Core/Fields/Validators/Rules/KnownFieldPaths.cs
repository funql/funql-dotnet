// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Requests.Validators;

namespace FunQL.Core.Fields.Validators.Rules;

/// <summary>
/// Validates that <see cref="FieldPath"/> nodes are known.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class KnownFieldPaths : AbstractValidationRule<FieldPath>
{
    /// <inheritdoc/>
    public override Task ValidateOnEnter(FieldPath node, IValidatorState state, CancellationToken cancellationToken)
    {
        var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
        var currentFields = new List<Field>();
        foreach (var (field, _, typeConfig) in node.ResolveConfigs(requestConfig.ReturnTypeConfig))
        {
            currentFields.Add(field);
            if (typeConfig != null)
                continue;

            var currentPath = new FieldPath(currentFields);
            state.AddError(new ValidationError($"Field '{currentPath.ToUnpackedPathString()}' is unknown.", field));
            break;
        }

        return Task.CompletedTask;
    }
}