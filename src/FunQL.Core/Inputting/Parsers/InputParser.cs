// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Inputting.Nodes;
using FunQL.Core.Lexers;

namespace FunQL.Core.Inputting.Parsers;

/// <summary>Default implementation of <see cref="IInputParser"/>.</summary>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
public class InputParser(IInputConstantParser constantParser) : IInputParser
{
    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IInputConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Input ParseInput(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Input.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var constant = ParseConstant(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Input(constant, metadata);
    }

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state) => _constantParser.ParseConstant(state);
}