// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Requests.Validators;
using FunQL.Core.Sorting.Configs.FunctionSupport.Extensions;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Validators.Rules;

/// <summary>
/// Validates that <see cref="Sort"/> nodes are supported for given <see cref="FieldPath"/>.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class SortSupportedForFields() : CompositeValidationRule(
    new SortValidationRule(),
    new FieldFunctionValidationRule(),
    new FieldPathValidationRule()
)
{
    /// <summary>Context shared by rules.</summary>
    private sealed class Context : IValidateContext
    {
        /// <summary>Field function name in case there is one.</summary>
        public string? FieldFunctionName { get; set; }
    }

    /// <summary>Enters <see cref="Context"/> for nested rules.</summary>
    private sealed class SortValidationRule : AbstractValidationRule<Sort>
    {
        public override Task ValidateOnEnter(Sort node, IValidatorState state, CancellationToken cancellationToken)
        {
            state.EnterContext(new Context());
            return Task.CompletedTask;
        }

        public override Task ValidateOnExit(Sort node, IValidatorState state, CancellationToken cancellationToken)
        {
            state.ExitContext();
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Sets <see cref="Context.FieldFunctionName"/> if <see cref="Context"/> is available (meaning we are in a
    /// <see cref="Sort"/> node).
    /// </summary>
    private sealed class FieldFunctionValidationRule : AbstractValidationRule<FieldFunction>
    {
        public override Task ValidateOnEnter(
            FieldFunction node, IValidatorState state, CancellationToken cancellationToken
        )
        {
            var context = state.FindContext<Context>();
            if (context != null)
                context.FieldFunctionName = node.Name;

            return Task.CompletedTask;
        }

        public override Task ValidateOnExit(
            FieldFunction node, IValidatorState state, CancellationToken cancellationToken
        )
        {
            var context = state.FindContext<Context>();
            // Make sure to clear FieldFunctionName so the next nested rule does not use it for validation
            if (context != null)
                context.FieldFunctionName = null;

            return Task.CompletedTask;
        }
    }

    /// <summary>Validates that <see cref="FieldPath"/> supports sorting.</summary>
    private sealed class FieldPathValidationRule : AbstractValidationRule<FieldPath>
    {
        public override Task ValidateOnEnter(FieldPath node, IValidatorState state, CancellationToken cancellationToken)
        {
            var context = state.FindContext<Context>();
            // Early return if we're not in Sort's context
            if (context == null)
                return Task.CompletedTask;

            var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
            var fieldFunctionName = context.FieldFunctionName;
            ITypeConfig? foundTypeConfig = null;
            foreach (var (_, _, typeConfig) in node.ResolveConfigs(requestConfig.ReturnTypeConfig))
            {
                foundTypeConfig = typeConfig;
                if (foundTypeConfig == null)
                    break;
            }

            // If type config not found, the field may be unknown, which should be validated by some other rule
            if (foundTypeConfig == null)
                return Task.CompletedTask;

            var extension = foundTypeConfig.FindSortSupportConfigExtension();
            if (extension == null || extension.IsFunctionSupported(Sort.FunctionName) != true)
            {
                state.AddError(new ValidationError(
                    $"Field '{node.ToUnpackedPathString()}' does not support sorting.",
                    node
                ));
            }
            else if (fieldFunctionName != null && extension.IsFunctionSupported(fieldFunctionName) != true)
            {
                state.AddError(new ValidationError(
                    $"Field '{node.ToUnpackedPathString()}' does not support sort field function '{fieldFunctionName}'.",
                    node
                ));
            }

            return Task.CompletedTask;
        }
    }
}