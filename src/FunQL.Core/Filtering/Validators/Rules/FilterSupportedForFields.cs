// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Filtering.Configs.FunctionSupport.Extensions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Requests.Validators;

namespace FunQL.Core.Filtering.Validators.Rules;

/// <summary>
/// Validates that <see cref="Filter"/> nodes are supported for given <see cref="FieldPath"/>.
///
/// Requires <see cref="RequestConfigValidateContext"/> to be available via <see cref="IValidatorState"/>.
/// </summary>
public sealed class FilterSupportedForFields() : CompositeValidationRule(
    new FilterValidationRule(),
    new BooleanExpressionValidationRule(),
    new FieldFunctionValidationRule(),
    new FieldPathValidationRule()
)
{
    /// <summary>Context shared by rules.</summary>
    private sealed class Context : IValidateContext
    {
        /// <summary>Filter function name for current filter.</summary>
        public string? FilterFunctionName { get; set; }

        /// <summary>Field function name in case there is one.</summary>
        public string? FieldFunctionName { get; set; }
    }

    /// <summary>Enters <see cref="Context"/> for nested rules.</summary>
    private sealed class FilterValidationRule : AbstractValidationRule<Filter>
    {
        public override Task ValidateOnEnter(Filter node, IValidatorState state, CancellationToken cancellationToken)
        {
            state.EnterContext(new Context());
            return Task.CompletedTask;
        }

        public override Task ValidateOnExit(Filter node, IValidatorState state, CancellationToken cancellationToken)
        {
            state.ExitContext();
            return Task.CompletedTask;
        }
    }

    /// <summary>Sets <see cref="Context.FilterFunctionName"/> for nested rules to use.</summary>
    private sealed class BooleanExpressionValidationRule : AbstractValidationRule<BooleanExpression>
    {
        public override Task ValidateOnEnter(
            BooleanExpression node, IValidatorState state, CancellationToken cancellationToken
        )
        {
            var context = state.RequireContext<Context>();
            context.FilterFunctionName = node.Name;

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Sets <see cref="Context.FieldFunctionName"/> if <see cref="Context"/> is available (meaning we are in a
    /// <see cref="Filter"/> node).
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
            if (context != null)
                // Make sure to clear FieldFunctionName so the next nested rule does not use it for validation
                context.FieldFunctionName = null;

            return Task.CompletedTask;
        }
    }

    /// <summary>Validates that <see cref="FieldPath"/> supports filtering.</summary>
    private sealed class FieldPathValidationRule : AbstractValidationRule<FieldPath>
    {
        public override Task ValidateOnEnter(FieldPath node, IValidatorState state, CancellationToken cancellationToken)
        {
            var context = state.FindContext<Context>();
            // Early return if we're not in Filter's context
            if (context == null)
                return Task.CompletedTask;

            var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
            var filterFunctionName = context.FilterFunctionName
                ?? throw new InvalidOperationException("Filter function must be set");
            var fieldFunctionName = context.FieldFunctionName;
            ITypeConfig? foundTypeConfig = null;
            foreach (var (_, _, typeConfig) in node.ResolveConfigs(requestConfig.ReturnTypeConfig))
            {
                foundTypeConfig = typeConfig;
                if (foundTypeConfig == null)
                    break;
            }

            if (foundTypeConfig == null)
                return Task.CompletedTask;

            var extension = foundTypeConfig.FindFilterSupportConfigExtension();
            if (extension == null || extension.IsFunctionSupported(Filter.FunctionName) != true)
            {
                state.AddError(new ValidationError(
                    $"Field '{node.ToUnpackedPathString()}' does not support filtering.",
                    node
                ));
            }
            else if (extension.IsFunctionSupported(filterFunctionName) != true)
            {
                state.AddError(new ValidationError(
                    $"Field '{node.ToUnpackedPathString()}' does not support filter '{filterFunctionName}'.",
                    node
                ));
            }
            else if (fieldFunctionName != null && extension.IsFunctionSupported(fieldFunctionName) != true)
            {
                state.AddError(new ValidationError(
                    $"Field '{node.ToUnpackedPathString()}' does not support filter field function '{fieldFunctionName}'.",
                    node
                ));
            }

            return Task.CompletedTask;
        }
    }
}