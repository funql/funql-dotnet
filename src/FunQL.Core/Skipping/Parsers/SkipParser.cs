// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Parsers;

/// <summary>Default implementation of <see cref="ISkipParser"/>.</summary>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
/// <inheritdoc/>
public class SkipParser(
    IConstantParser constantParser
) : ISkipParser
{
    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Skip ParseSkip(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Skip.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var constant = ParseConstant(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Skip(constant, metadata);
    }

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        state.EnterContext(new ConstantParseContext(typeof(int)));
        var constant = _constantParser.ParseConstant(state);
        state.ExitContext();
        return constant;
    }
}