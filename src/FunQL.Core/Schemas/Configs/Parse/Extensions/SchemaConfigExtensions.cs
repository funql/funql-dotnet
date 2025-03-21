// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Inputting.Nodes;
using FunQL.Core.Lexers;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Requests.Parsers;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;
using FunQL.Core.Skipping.Nodes;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Schemas.Configs.Parse.Extensions;

/// <summary>Extensions related to <see cref="ISchemaConfig"/> and parsing requests.</summary>
public static class SchemaConfigExtensions
{
    /// <summary>
    /// Gets the <see cref="IParseConfigExtension"/> for <see cref="IParseConfigExtension.DefaultName"/> or <c>null</c>
    /// if not found.
    /// </summary>
    /// <param name="config">Config to get extension from.</param>
    /// <returns>The <see cref="IParseConfigExtension"/> or <c>null</c> if not found.</returns>
    public static IParseConfigExtension? FindParseConfigExtension(this ISchemaConfig config) =>
        config.FindExtension<IParseConfigExtension>(IParseConfigExtension.DefaultName);

    /// <summary>
    /// Creates an <see cref="IParserState"/> for given schema, parse config, request name, and input text.
    /// </summary>
    private static IParserState CreateParserState(
        ISchemaConfig schemaConfig,
        IParseConfigExtension parseConfig,
        string requestName,
        string text
    )
    {
        var requestConfig = schemaConfig.FindRequestConfig(requestName)
            ?? throw new InvalidOperationException("No RequestConfig found for given name");

        var parserState = parseConfig.ParserStateFactory(schemaConfig, new StringLexer(text));
        parserState.EnterContext(new RequestConfigParseContext(requestConfig));
        return parserState;
    }

    /// <summary>Parses given <paramref name="request"/>.</summary>
    /// <param name="schemaConfig">The schema config to parse request with.</param>
    /// <param name="request">The FunQL request text to parse.</param>
    /// <returns>A <see cref="Request"/> representing the parsed request.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no parse configuration is found.</exception>
    public static Request ParseRequest(this ISchemaConfig schemaConfig, string request)
    {
        var parseConfig = schemaConfig.FindParseConfigExtension()
            ?? throw new InvalidOperationException("No parse config found");

        var parser = parseConfig.RequestParserProvider(schemaConfig);
        var parserState = parseConfig.ParserStateFactory(schemaConfig, new StringLexer(request));
        return parser.ParseRequest(parserState);
    }

    /// <summary>Parses a <see cref="Request"/> with given <paramref name="requestName"/> for given parameters.</summary>
    /// <param name="schemaConfig">The schema config to parse request with.</param>
    /// <param name="requestName">The name of the request.</param>
    /// <param name="input">The input parameter to parse.</param>
    /// <param name="filter">The filter parameter to parse.</param>
    /// <param name="sort">The sort parameter to parse.</param>
    /// <param name="skip">The skip parameter to parse.</param>
    /// <param name="limit">The limit parameter to parse.</param>
    /// <param name="count">The count parameter to parse.</param>
    /// <returns>A <see cref="Request"/> containing the parsed components.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no parse configuration is found.</exception>
    /// <remarks>
    /// This is useful for e.g. REST APIs, where each FunQL parameter is passed as a separate query parameter. This will
    /// parse each separate parameter and create a <see cref="Request"/> out of it.
    /// </remarks>
    public static Request ParseRequestForParameters(
        this ISchemaConfig schemaConfig,
        string requestName,
        string? input = null,
        string? filter = null,
        string? sort = null,
        string? skip = null,
        string? limit = null,
        string? count = null
    )
    {
        var parseConfig = schemaConfig.FindParseConfigExtension()
            ?? throw new InvalidOperationException("No parse config found");

        var parameters = new List<Parameter>();

        if (!string.IsNullOrEmpty(input))
        {
            var parser = parseConfig.InputParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, input);
            var parameter = input.StartsWith(Input.FunctionName)
                ? parser.ParseInput(parserState)
                : new Input(parser.ParseConstant(parserState));
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        if (!string.IsNullOrEmpty(filter))
        {
            var parser = parseConfig.FilterParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, filter);
            var parameter = filter.StartsWith(Filter.FunctionName)
                ? parser.ParseFilter(parserState)
                : new Filter(parser.ParseBooleanExpression(parserState));
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            var parser = parseConfig.SortParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, sort);

            Sort ParseExpressions()
            {
                var expressions = new List<SortExpression>();
                do
                {
                    var expression = parser.ParseSortExpression(parserState);
                    expressions.Add(expression);
                } while (parserState.ExpectOptionalToken(TokenType.Comma));

                return new Sort(expressions);
            }
            
            var parameter = sort.StartsWith(Sort.FunctionName)
                ? parser.ParseSort(parserState)
                : ParseExpressions();
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        if (!string.IsNullOrEmpty(skip))
        {
            var parser = parseConfig.SkipParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, skip);
            var parameter = skip.StartsWith(Skip.FunctionName)
                ? parser.ParseSkip(parserState)
                : new Skip(parser.ParseConstant(parserState));
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        if (!string.IsNullOrEmpty(limit))
        {
            var parser = parseConfig.LimitParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, limit);
            var parameter = limit.StartsWith(Limit.FunctionName)
                ? parser.ParseLimit(parserState)
                : new Limit(parser.ParseConstant(parserState));
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        if (!string.IsNullOrEmpty(count))
        {
            var parser = parseConfig.CountParserProvider(schemaConfig);
            var parserState = CreateParserState(schemaConfig, parseConfig, requestName, count);
            var parameter = count.StartsWith(Count.FunctionName)
                ? parser.ParseCount(parserState)
                : new Count(parser.ParseConstant(parserState));
            parameters.Add(parameter);

            // Assure there's no other tokens
            parserState.ExpectToken(TokenType.Eof);
        }

        return new Request(requestName, parameters);
    }
}