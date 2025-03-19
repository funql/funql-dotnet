// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Fields.Extensions;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Lexers;
using FunQL.Core.Requests.Parsers;

namespace FunQL.Core.Filtering.Parsers;

/// <summary>
/// Implementation of <see cref="IFilterConstantParser"/> that uses the configured <see cref="ISchemaConfig"/> and
/// <see cref="IRequestConfig"/>, added as context, to parse the <see cref="Constant"/> node.
///
/// Requires <see cref="FilterConstantParseContext"/> as context.
/// </summary>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
/// <remarks>
/// Requires <see cref="SchemaConfigParseContext"/> and <see cref="RequestConfigParseContext"/> to be available via
/// <see cref="IParserState"/>.
/// </remarks>
public class ConfigFilterConstantParser(IConstantParser constantParser) : IFilterConstantParser
{
    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        var (expressionName, fieldArgument) = state.RequireContext<FilterConstantParseContext>();
        var type = Resolve(state, expressionName, fieldArgument);
        state.EnterContext(new ConstantParseContext(type));
        var constant = _constantParser.ParseConstant(state);
        state.ExitContext();

        return constant;
    }

    private static Type Resolve(IParserState state, string expressionName, FieldArgument fieldArgument)
    {
        var expressionConstantType = Resolve(state, expressionName);
        // Early return if expression's constant type is specific (not object)
        if (expressionConstantType.Type != typeof(object))
            return expressionConstantType.Type;

        // No specific type, so try to resolve type from argument
        var fieldArgumentType = fieldArgument switch
        {
            FieldPath path => Resolve(state, path),
            FieldFunction function => Resolve(state, function),
            _ => throw new ArgumentOutOfRangeException(nameof(fieldArgument))
        };

        return fieldArgumentType.Type;
    }

    private static ITypeConfig Resolve(IParserState state, string expressionName)
    {
        var schemaConfig = state.RequireContext<SchemaConfigParseContext>().SchemaConfig;
        var config = schemaConfig.FindFunctionConfig(expressionName)
            ?? throw UnresolvedException(state.CurrentToken(), $"No config found for expression '{expressionName}'.");

        return config.ArgumentTypeConfigs.ElementAtOrDefault(1)
            ?? throw UnresolvedException(
                state.CurrentToken(),
                $"Missing argument config at index 1 for expression '{expressionName}'."
            );
    }

    private static ITypeConfig Resolve(IParserState state, FieldFunction fieldFunction)
    {
        var schemaConfig = state.RequireContext<SchemaConfigParseContext>().SchemaConfig;
        var config = schemaConfig.FindFunctionConfig(fieldFunction.Name)
            ?? throw UnresolvedException(
                state.CurrentToken(),
                $"No config found for field function '{fieldFunction.Name}'."
            );

        return config.ReturnTypeConfig;
    }

    private static ITypeConfig Resolve(IParserState state, FieldPath fieldPath)
    {
        var requestConfig = state.RequireContext<RequestConfigParseContext>().RequestConfig;
        ITypeConfig? resolvedTypeConfig = null;
        var currentFields = new List<Field>();
        foreach (var (field, _, typeConfig) in fieldPath.ResolveConfigs(requestConfig.ReturnTypeConfig))
        {
            currentFields.Add(field);
            resolvedTypeConfig = typeConfig
                ?? throw UnresolvedException(
                    state.CurrentToken(),
                    $"No config found for field '{new FieldPath(currentFields).ToUnpackedPathString()}'."
                );
        }

        return resolvedTypeConfig
            // Config could only be null at this point if FieldPath has no fields (otherwise exception will have been
            // thrown in loop above already), which should not happen as that would be an invalid FieldPath
            ?? throw UnresolvedException(state.CurrentToken(), "No config found.");
    }

    private static ParseException UnresolvedException(Token token, string failReason) => new(
        ConstantParseErrors.FailedToParse(token.Text, token.Position),
        new InvalidOperationException(failReason)
    );
}