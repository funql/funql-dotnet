// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Lexers;

namespace FunQL.Core.Counting.Parsers;

/// <summary>Default implementation of <see cref="ICountParser"/>.</summary>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
/// <inheritdoc/>
public class CountParser(IConstantParser constantParser) : ICountParser
{
    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Count ParseCount(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Count.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var constant = ParseConstant(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Count(constant, metadata);
    }

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        state.EnterContext(new ConstantParseContext(typeof(bool)));
        var constant = _constantParser.ParseConstant(state);
        state.ExitContext();
        return constant;
    }
}