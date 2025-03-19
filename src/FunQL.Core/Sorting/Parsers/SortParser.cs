// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Parsers;

/// <summary>Default implementation of <see cref="ISortParser"/>.</summary>
/// <param name="fieldArgumentParser">Parser for <see cref="FieldArgument"/> nodes.</param>
/// <inheritdoc/>
public class SortParser(IFieldArgumentParser fieldArgumentParser) : ISortParser
{
    /// <summary>Parser for <see cref="FieldArgument"/> nodes.</summary>
    private readonly IFieldArgumentParser _fieldArgumentParser = fieldArgumentParser;

    /// <inheritdoc/>
    public Sort ParseSort(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Sort.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var expressions = ParseSortExpressions(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Sort(expressions, metadata);
    }

    /// <summary>Tries to parse list of <see cref="SortExpression"/> nodes for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed list of <see cref="SortExpression"/> nodes.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    private List<SortExpression> ParseSortExpressions(IParserState state)
    {
        var expressions = new List<SortExpression>();
        do
        {
            var expression = ParseSortExpression(state);
            expressions.Add(expression);
        } while (state.ExpectOptionalToken(TokenType.Comma));

        return expressions;
    }

    /// <inheritdoc/>
    public SortExpression ParseSortExpression(IParserState state)
    {
        var token = state.CurrentToken();
        return token.Text switch
        {
            Ascending.FunctionName => ParseAscending(state),
            Descending.FunctionName => ParseDescending(state),
            _ => throw state.Lexer.SyntaxException(SyntaxErrors.UnknownIdentifier(token.Text, token.Position))
        };
    }

    /// <inheritdoc/>
    public Ascending ParseAscending(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Ascending.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var result = ParseFieldArgument(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Ascending(result, metadata);
    }

    /// <inheritdoc/>
    public Descending ParseDescending(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Descending.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var result = ParseFieldArgument(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Descending(result, metadata);
    }

    /// <inheritdoc/>
    public FieldPath ParseFieldPath(IParserState state) => _fieldArgumentParser.ParseFieldPath(state);

    /// <inheritdoc/>
    public Field ParseField(IParserState state) => _fieldArgumentParser.ParseField(state);

    /// <inheritdoc/>
    public NamedField ParseNamedField(IParserState state) => _fieldArgumentParser.ParseNamedField(state);

    /// <inheritdoc/>
    public ListItemField ParseListItemField(IParserState state) => _fieldArgumentParser.ParseListItemField(state);

    /// <inheritdoc/>
    public FieldFunction ParseFieldFunction(IParserState state) => _fieldArgumentParser.ParseFieldFunction(state);

    /// <inheritdoc/>
    public Year ParseYear(IParserState state) => _fieldArgumentParser.ParseYear(state);

    /// <inheritdoc/>
    public Month ParseMonth(IParserState state) => _fieldArgumentParser.ParseMonth(state);

    /// <inheritdoc/>
    public Day ParseDay(IParserState state) => _fieldArgumentParser.ParseDay(state);

    /// <inheritdoc/>
    public Hour ParseHour(IParserState state) => _fieldArgumentParser.ParseHour(state);

    /// <inheritdoc/>
    public Minute ParseMinute(IParserState state) => _fieldArgumentParser.ParseMinute(state);

    /// <inheritdoc/>
    public Second ParseSecond(IParserState state) => _fieldArgumentParser.ParseSecond(state);

    /// <inheritdoc/>
    public Millisecond ParseMillisecond(IParserState state) => _fieldArgumentParser.ParseMillisecond(state);

    /// <inheritdoc/>
    public Floor ParseFloor(IParserState state) => _fieldArgumentParser.ParseFloor(state);

    /// <inheritdoc/>
    public Ceiling ParseCeiling(IParserState state) => _fieldArgumentParser.ParseCeiling(state);

    /// <inheritdoc/>
    public Round ParseRound(IParserState state) => _fieldArgumentParser.ParseRound(state);

    /// <inheritdoc/>
    public Lower ParseLower(IParserState state) => _fieldArgumentParser.ParseLower(state);

    /// <inheritdoc/>
    public Upper ParseUpper(IParserState state) => _fieldArgumentParser.ParseUpper(state);

    /// <inheritdoc/>
    public IsNull ParseIsNull(IParserState state) => _fieldArgumentParser.ParseIsNull(state);

    /// <inheritdoc/>
    public FieldArgument ParseFieldArgument(IParserState state) => _fieldArgumentParser.ParseFieldArgument(state);
}