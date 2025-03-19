// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Lexers;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Parsers;

/// <summary>Default implementation of <see cref="IRequestParser"/>.</summary>
/// <param name="parameterParser">Parser for <see cref="Parameter"/> nodes.</param>
/// <remarks>
/// In case <see cref="SchemaConfigParseContext"/> is found and a request exists for given <see cref="Request.Name"/>,
/// this parser will add <see cref="RequestConfigParseContext"/> for nested parsers to use.
/// </remarks>
public class RequestParser(IParameterParser parameterParser) : IRequestParser
{
    /// <summary>Parser for <see cref="Parameter"/> nodes.</summary>
    private readonly IParameterParser _parameterParser = parameterParser;

    /// <inheritdoc/>
    public Request ParseRequest(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        var requestName = state.ExpectToken(TokenType.Identifier).Text;
        state.ExpectToken(TokenType.OpenParen);

        // Add RequestConfigParseContext if given request name is known (and SchemaConfig is set)
        var requestConfig = state.FindContext<SchemaConfigParseContext>()?.SchemaConfig.FindRequestConfig(requestName);
        if (requestConfig != null)
            state.EnterContext(new RequestConfigParseContext(requestConfig));

        var parameters = new List<Parameter>();
        // Parse arguments if given (function not closed)
        if (state.CurrentToken().Type != TokenType.CloseParen)
        {
            do
            {
                var parameter = ParseParameter(state);
                parameters.Add(parameter);
            } while (state.ExpectOptionalToken(TokenType.Comma));
        }

        // Exit RequestConfigParseContext if previously entered
        if (requestConfig != null)
            state.ExitContext();

        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Request(requestName, parameters, metadata);
    }

    /// <inheritdoc/>
    public Parameter ParseParameter(IParserState state) => _parameterParser.ParseParameter(state);
}