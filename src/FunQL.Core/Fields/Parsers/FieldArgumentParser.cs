// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Lexers;

namespace FunQL.Core.Fields.Parsers;

/// <summary>Default implementation of <see cref="IFieldArgumentParser"/>.</summary>
/// <param name="fieldFunctionParser">Parser for <see cref="FieldFunction"/> nodes.</param>
public class FieldArgumentParser(IFieldFunctionParser fieldFunctionParser) : IFieldArgumentParser
{
    /// <summary>Parser for <see cref="FieldFunction"/> nodes.</summary>
    private readonly IFieldFunctionParser _fieldFunctionParser = fieldFunctionParser;

    /// <inheritdoc/>
    public FieldArgument ParseFieldArgument(IParserState state)
    {
        return state.CurrentToken().Type == TokenType.Identifier && state.PeekNextToken().Type == TokenType.OpenParen
            ? ParseFieldFunction(state)
            : ParseFieldPath(state);
    }

    /// <inheritdoc/>
    public FieldPath ParseFieldPath(IParserState state) => _fieldFunctionParser.ParseFieldPath(state);

    /// <inheritdoc/>
    public Field ParseField(IParserState state) => _fieldFunctionParser.ParseField(state);

    /// <inheritdoc/>
    public NamedField ParseNamedField(IParserState state) => _fieldFunctionParser.ParseNamedField(state);

    /// <inheritdoc/>
    public ListItemField ParseListItemField(IParserState state) => _fieldFunctionParser.ParseListItemField(state);

    /// <inheritdoc/>
    public FieldFunction ParseFieldFunction(IParserState state) => _fieldFunctionParser.ParseFieldFunction(state);

    /// <inheritdoc/>
    public Year ParseYear(IParserState state) => _fieldFunctionParser.ParseYear(state);

    /// <inheritdoc/>
    public Month ParseMonth(IParserState state) => _fieldFunctionParser.ParseMonth(state);

    /// <inheritdoc/>
    public Day ParseDay(IParserState state) => _fieldFunctionParser.ParseDay(state);

    /// <inheritdoc/>
    public Hour ParseHour(IParserState state) => _fieldFunctionParser.ParseHour(state);

    /// <inheritdoc/>
    public Minute ParseMinute(IParserState state) => _fieldFunctionParser.ParseMinute(state);

    /// <inheritdoc/>
    public Second ParseSecond(IParserState state) => _fieldFunctionParser.ParseSecond(state);

    /// <inheritdoc/>
    public Millisecond ParseMillisecond(IParserState state) => _fieldFunctionParser.ParseMillisecond(state);

    /// <inheritdoc/>
    public Floor ParseFloor(IParserState state) => _fieldFunctionParser.ParseFloor(state);

    /// <inheritdoc/>
    public Ceiling ParseCeiling(IParserState state) => _fieldFunctionParser.ParseCeiling(state);

    /// <inheritdoc/>
    public Round ParseRound(IParserState state) => _fieldFunctionParser.ParseRound(state);

    /// <inheritdoc/>
    public Lower ParseLower(IParserState state) => _fieldFunctionParser.ParseLower(state);

    /// <inheritdoc/>
    public Upper ParseUpper(IParserState state) => _fieldFunctionParser.ParseUpper(state);

    /// <inheritdoc/>
    public IsNull ParseIsNull(IParserState state) => _fieldFunctionParser.ParseIsNull(state);
}