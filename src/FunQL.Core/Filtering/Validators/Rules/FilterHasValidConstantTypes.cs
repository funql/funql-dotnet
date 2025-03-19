// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text;
using FunQL.Core.Common.Validators;
using FunQL.Core.Common.Validators.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Requests.Validators;
using FunQL.Core.Schemas.Validators;

namespace FunQL.Core.Filtering.Validators.Rules;

/// <summary>
/// Validates that <see cref="Filter"/> nodes have valid <see cref="Constant"/> type for their corresponding filters.
///
/// Requires <see cref="SchemaConfigValidateContext"/> and <see cref="RequestConfigValidateContext"/> to be available
/// via <see cref="IValidatorState"/>.
/// </summary>
public sealed class FilterHasValidConstantTypes() : CompositeValidationRule(
    new FilterValidationRule(),
    new BooleanExpressionValidationRule(),
    new FieldFunctionValidationRule(),
    new FieldPathValidationRule(),
    new ConstantValidationRule()
)
{
    /// <summary>Context shared by rules.</summary>
    private sealed class Context : IValidateContext
    {
        /// <summary>Filter function name for current filter.</summary>
        public string? FilterFunctionName { get; set; }

        /// <summary>Field function name in case there is one.</summary>
        public string? FieldFunctionName { get; set; }

        /// <summary>Expected type of <see cref="Constant"/> for current filter.</summary>
        public Type? ExpectedConstantType { get; set; }

        /// <summary>Whether <see cref="Constant"/> is expected to be nullable for current filter.</summary>
        public bool IsExpectedConstantNullable { get; set; }

        /// <summary>Current field.</summary>
        public FieldPath? FieldPath { get; set; }
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

        public override Task ValidateOnExit(
            BooleanExpression node, IValidatorState state, CancellationToken cancellationToken
        )
        {
            var context = state.RequireContext<Context>();
            // Reset FieldFunctionName for the next expression
            context.FieldFunctionName = null;

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

        // FieldFunctionName is reset in BooleanExpressionValidationRule.ValidateOnExit()
        // We don't reset upon exiting FieldFunction as FieldFunctionName is used upon entering Constant, which is after
        // we've exited FieldFunction: The FieldFunctionName scope should be bound to parent, the BooleanExpression
    }

    /// <summary>
    /// Resolves the <see cref="Context"/> <see cref="FieldPath"/> related values
    /// <see cref="Context.FieldFunctionName"/> if <see cref="Context"/> is available (meaning we are in a
    /// <see cref="Filter"/> node).
    /// </summary>
    private sealed class FieldPathValidationRule : AbstractValidationRule<FieldPath>
    {
        public override Task ValidateOnEnter(FieldPath node, IValidatorState state, CancellationToken cancellationToken)
        {
            var context = state.FindContext<Context>();
            // Early return if we're not in Filter's context
            if (context == null)
                return Task.CompletedTask;

            var schemaConfig = state.RequireContext<SchemaConfigValidateContext>().SchemaConfig;
            var requestConfig = state.RequireContext<RequestConfigValidateContext>().RequestConfig;
            var filterFunctionName = context.FilterFunctionName
                ?? throw new InvalidOperationException("Filter function must be set");
            var fieldFunctionName = context.FieldFunctionName;

            var expressionConstantTypeConfig = schemaConfig.FindFunctionConfig(filterFunctionName)
                ?.ArgumentTypeConfigs
                .ElementAtOrDefault(1);
            if (expressionConstantTypeConfig == null)
                return Task.CompletedTask;

            var constantTypeConfig = expressionConstantTypeConfig;
            if (constantTypeConfig.Type == typeof(object))
            {
                if (fieldFunctionName != null)
                {
                    constantTypeConfig = schemaConfig.FindFunctionConfig(fieldFunctionName)?.ReturnTypeConfig;
                }
                else
                {
                    foreach (var (_, _, typeConfig) in node.ResolveConfigs(requestConfig.ReturnTypeConfig))
                    {
                        constantTypeConfig = typeConfig;
                        if (constantTypeConfig == null)
                            break;
                    }
                }

                if (constantTypeConfig == null)
                    return Task.CompletedTask;
            }

            context.FieldPath = node;
            context.ExpectedConstantType = constantTypeConfig?.Type;
            context.IsExpectedConstantNullable = expressionConstantTypeConfig.IsNullable;

            return Task.CompletedTask;
        }
    }

    /// <summary>Validates that <see cref="Constant"/> type is valid for current <see cref="Context"/>.</summary>
    private sealed class ConstantValidationRule : AbstractValidationRule<Constant>
    {
        public override Task ValidateOnEnter(Constant node, IValidatorState state, CancellationToken cancellationToken)
        {
            var context = state.FindContext<Context>();
            // Early return if we're not in Filter's context
            if (context == null)
                return Task.CompletedTask;

            var expectedType = context.ExpectedConstantType;
            // Early return if we don't know what the expected type is
            if (expectedType == null)
                return Task.CompletedTask;
            // Get underlying type in case this is Nullable<T>
            expectedType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;

            var nullable = context.IsExpectedConstantNullable;
            var filterFunctionName = context.FilterFunctionName
                ?? throw new InvalidOperationException("Filter function must be set");
            var fieldFunctionName = context.FieldFunctionName;
            var fieldPath = context.FieldPath
                ?? throw new InvalidOperationException("Field path must be set");

            var value = node.Value;

            if (value == null)
            {
                if (!nullable)
                {
                    state.AddError(new ValidationError(
                        BuildErrorMessage(filterFunctionName, fieldFunctionName, fieldPath, "can not be 'null'"),
                        node
                    ));
                }
            }
            else if (value.GetType() != expectedType)
            {
                state.AddError(new ValidationError(
                    BuildErrorMessage(filterFunctionName, fieldFunctionName, fieldPath, "is invalid"),
                    node
                ));
            }

            return Task.CompletedTask;
        }

        private static string BuildErrorMessage(
            string filterFunctionName, string? fieldFunctionName, FieldPath fieldPath, string reason
        )
        {
            var errorMessageBuilder = new StringBuilder(
                $"Constant for filter '{filterFunctionName}' on field '{fieldPath.ToUnpackedPathString()}' "
            );
            if (fieldFunctionName != null)
                errorMessageBuilder.Append($"with field function '{fieldFunctionName}' ");
            errorMessageBuilder.Append(reason);
            errorMessageBuilder.Append('.');
            return errorMessageBuilder.ToString();
        }
    }
}