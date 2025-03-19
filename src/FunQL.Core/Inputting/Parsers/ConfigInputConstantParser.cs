// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Inputting.Configs.Extensions;
using FunQL.Core.Lexers;
using FunQL.Core.Requests.Parsers;

namespace FunQL.Core.Inputting.Parsers;

/// <summary>
/// Implementation of <see cref="IInputConstantParser"/> that uses the configured <see cref="IRequestConfig"/>, added as
/// context, to parse the <see cref="Constant"/> node.
/// </summary>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
/// <remarks>Requires <see cref="RequestConfigParseContext"/> to be available via <see cref="IParserState"/>.</remarks>
public class ConfigInputConstantParser(IConstantParser constantParser) : IInputConstantParser
{
    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        var typeConfig = state.RequireContext<RequestConfigParseContext>().RequestConfig
                .FindInputParameterConfig()
                ?.FindInputConfigExtension()
                ?.TypeConfig
            ?? throw UnresolvedException(state.CurrentToken(), "InputConfigExtension not found.");
        state.EnterContext(new ConstantParseContext(typeConfig.Type));
        var constant = _constantParser.ParseConstant(state);
        state.ExitContext();

        return constant;
    }

    /// <summary>Creates an 'unresolved' <see cref="ParseException"/>.</summary>
    private static ParseException UnresolvedException(Token token, string failReason) => new(
        ConstantParseErrors.FailedToParse(token.Text, token.Position),
        new InvalidOperationException(failReason)
    );
}