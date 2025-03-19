// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Counting.Parsers;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Parsers;
using FunQL.Core.Inputting.Nodes;
using FunQL.Core.Inputting.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;
using FunQL.Core.Limiting.Nodes;
using FunQL.Core.Limiting.Parsers;
using FunQL.Core.Requests.Nodes;
using FunQL.Core.Skipping.Nodes;
using FunQL.Core.Skipping.Parsers;
using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Parsers;

namespace FunQL.Core.Requests.Parsers;

/// <summary>Default implementation of <see cref="IParameterParser"/>.</summary>
/// <param name="inputParser">Parser for <see cref="Input"/> nodes.</param>
/// <param name="filterParser">Parser for <see cref="Filter"/> nodes.</param>
/// <param name="sortParser">Parser for <see cref="Sort"/> nodes.</param>
/// <param name="skipParser">Parser for <see cref="Skip"/> nodes.</param>
/// <param name="limitParser">Parser for <see cref="Limit"/> nodes.</param>
/// <param name="countParser">Parser for <see cref="Count"/> nodes.</param>
public class ParameterParser(
    IInputParser inputParser,
    IFilterParser filterParser,
    ISortParser sortParser,
    ISkipParser skipParser,
    ILimitParser limitParser,
    ICountParser countParser
) : IParameterParser
{
    /// <summary>Parser for <see cref="Input"/> nodes.</summary>
    private readonly IInputParser _inputParser = inputParser;

    /// <summary>Parser for <see cref="Filter"/> nodes.</summary>
    private readonly IFilterParser _filterParser = filterParser;

    /// <summary>Parser for <see cref="Sort"/> nodes.</summary>
    private readonly ISortParser _sortParser = sortParser;

    /// <summary>Parser for <see cref="Skip"/> nodes.</summary>
    private readonly ISkipParser _skipParser = skipParser;

    /// <summary>Parser for <see cref="Limit"/> nodes.</summary>
    private readonly ILimitParser _limitParser = limitParser;

    /// <summary>Parser for <see cref="Count"/> nodes.</summary>
    private readonly ICountParser _countParser = countParser;

    /// <inheritdoc/>
    public Parameter ParseParameter(IParserState state)
    {
        var token = state.CurrentToken();
        if (token.Type != TokenType.Identifier)
            throw state.Lexer.SyntaxException(
                SyntaxErrors.TokenExpected(TokenType.Identifier.GetDescription(), token.Position, token.Text)
            );

        return token.Text switch
        {
            Input.FunctionName => _inputParser.ParseInput(state),
            Filter.FunctionName => _filterParser.ParseFilter(state),
            Sort.FunctionName => _sortParser.ParseSort(state),
            Skip.FunctionName => _skipParser.ParseSkip(state),
            Limit.FunctionName => _limitParser.ParseLimit(state),
            Count.FunctionName => _countParser.ParseCount(state),
            _ => throw state.Lexer.SyntaxException(SyntaxErrors.UnknownIdentifier(token.Text, token.Position))
        };
    }
}