// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;

namespace FunQL.Core.Fields.Parsers.Functions;

/// <summary>Default implementation of <see cref="IFieldFunctionParser"/>.</summary>
/// <param name="fieldPathParser">Parser for <see cref="FieldPath"/> nodes.</param>
public class FieldFunctionParser(IFieldPathParser fieldPathParser) : IFieldFunctionParser
{
    /// <summary>Parser for <see cref="FieldPath"/> nodes.</summary>
    private readonly IFieldPathParser _fieldPathParser = fieldPathParser;

    /// <inheritdoc/>
    public FieldFunction ParseFieldFunction(IParserState state)
    {
        var token = state.CurrentToken();
        if (token.Type != TokenType.Identifier)
            throw state.Lexer.SyntaxException(
                SyntaxErrors.TokenExpected(TokenType.Identifier.GetDescription(), token.Position, token.Text)
            );
        return token.Text switch
        {
            Year.FunctionName => ParseYear(state),
            Month.FunctionName => ParseMonth(state),
            Day.FunctionName => ParseDay(state),
            Hour.FunctionName => ParseHour(state),
            Minute.FunctionName => ParseMinute(state),
            Second.FunctionName => ParseSecond(state),
            Millisecond.FunctionName => ParseMillisecond(state),
            Floor.FunctionName => ParseFloor(state),
            Ceiling.FunctionName => ParseCeiling(state),
            Round.FunctionName => ParseRound(state),
            Lower.FunctionName => ParseLower(state),
            Upper.FunctionName => ParseUpper(state),
            IsNull.FunctionName => ParseIsNull(state),
            _ => throw state.Lexer.SyntaxException(SyntaxErrors.UnknownIdentifier(token.Text, token.Position))
        };
    }

    /// <summary>
    /// Parses field function with given <paramref name="identifier"/>, creating the <see cref="FieldFunction"/> using
    /// the <paramref name="creator"/>.
    /// </summary>
    /// <param name="state">State of the parser.</param>
    /// <param name="identifier">Identifier of the field function.</param>
    /// <param name="creator">Function to create the parsed node.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <returns>The parsed node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    private T ParseFieldFunction<T>(IParserState state, string identifier, Func<FieldPath, Metadata, T> creator)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(identifier);
        state.ExpectToken(TokenType.OpenParen);
        var fieldPath = ParseFieldPath(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return creator(fieldPath, metadata);
    }

    /// <inheritdoc/>
    public Year ParseYear(IParserState state) =>
        ParseFieldFunction(state, Year.FunctionName, (fieldPath, metadata) => new Year(fieldPath, metadata));

    /// <inheritdoc/>
    public Month ParseMonth(IParserState state) =>
        ParseFieldFunction(state, Month.FunctionName, (fieldPath, metadata) => new Month(fieldPath, metadata));

    /// <inheritdoc/>
    public Day ParseDay(IParserState state) =>
        ParseFieldFunction(state, Day.FunctionName, (fieldPath, metadata) => new Day(fieldPath, metadata));

    /// <inheritdoc/>
    public Hour ParseHour(IParserState state) =>
        ParseFieldFunction(state, Hour.FunctionName, (fieldPath, metadata) => new Hour(fieldPath, metadata));

    /// <inheritdoc/>
    public Minute ParseMinute(IParserState state) =>
        ParseFieldFunction(state, Minute.FunctionName, (fieldPath, metadata) => new Minute(fieldPath, metadata));

    /// <inheritdoc/>
    public Second ParseSecond(IParserState state) =>
        ParseFieldFunction(state, Second.FunctionName, (fieldPath, metadata) => new Second(fieldPath, metadata));

    /// <inheritdoc/>
    public Millisecond ParseMillisecond(IParserState state) =>
        ParseFieldFunction(
            state,
            Millisecond.FunctionName,
            (fieldPath, metadata) => new Millisecond(fieldPath, metadata)
        );

    /// <inheritdoc/>
    public Floor ParseFloor(IParserState state) =>
        ParseFieldFunction(state, Floor.FunctionName, (fieldPath, metadata) => new Floor(fieldPath, metadata));

    /// <inheritdoc/>
    public Ceiling ParseCeiling(IParserState state) =>
        ParseFieldFunction(state, Ceiling.FunctionName, (fieldPath, metadata) => new Ceiling(fieldPath, metadata));

    /// <inheritdoc/>
    public Round ParseRound(IParserState state) =>
        ParseFieldFunction(state, Round.FunctionName, (fieldPath, metadata) => new Round(fieldPath, metadata));

    /// <inheritdoc/>
    public Lower ParseLower(IParserState state) =>
        ParseFieldFunction(state, Lower.FunctionName, (fieldPath, metadata) => new Lower(fieldPath, metadata));

    /// <inheritdoc/>
    public Upper ParseUpper(IParserState state) =>
        ParseFieldFunction(state, Upper.FunctionName, (fieldPath, metadata) => new Upper(fieldPath, metadata));

    /// <inheritdoc/>
    public IsNull ParseIsNull(IParserState state) =>
        ParseFieldFunction(state, IsNull.FunctionName, (fieldPath, metadata) => new IsNull(fieldPath, metadata));

    /// <inheritdoc/>
    public FieldPath ParseFieldPath(IParserState state) => _fieldPathParser.ParseFieldPath(state);

    /// <inheritdoc/>
    public Field ParseField(IParserState state) => _fieldPathParser.ParseField(state);

    /// <inheritdoc/>
    public NamedField ParseNamedField(IParserState state) => _fieldPathParser.ParseNamedField(state);

    /// <inheritdoc/>
    public ListItemField ParseListItemField(IParserState state) => _fieldPathParser.ParseListItemField(state);
}