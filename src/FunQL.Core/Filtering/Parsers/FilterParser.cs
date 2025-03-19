// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Lexers;
using FunQL.Core.Lexers.Extensions;

namespace FunQL.Core.Filtering.Parsers;

/// <summary>Default implementation of <see cref="IFilterParser"/>.</summary>
/// <param name="fieldArgumentParser">Parser for <see cref="FieldArgument"/> nodes.</param>
/// <param name="constantParser">Parser for <see cref="Constant"/> nodes.</param>
public class FilterParser(
    IFieldArgumentParser fieldArgumentParser,
    IFilterConstantParser constantParser
) : IFilterParser
{
    /// <summary>Parser for <see cref="FieldArgument"/> nodes.</summary>
    private readonly IFieldArgumentParser _fieldArgumentParser = fieldArgumentParser;

    /// <summary>Parser for <see cref="Constant"/> nodes.</summary>
    private readonly IFilterConstantParser _constantParser = constantParser;

    /// <inheritdoc/>
    public Filter ParseFilter(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Filter.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var expression = ParseBooleanExpression(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Filter(expression, metadata);
    }

    /// <inheritdoc/>
    public BooleanExpression ParseBooleanExpression(IParserState state)
    {
        var token = state.CurrentToken();
        if (token.Type != TokenType.Identifier)
            throw state.Lexer.SyntaxException(
                SyntaxErrors.TokenExpected(TokenType.Identifier.GetDescription(), token.Position, token.Text)
            );
        return token.Text switch
        {
            // Logical expressions
            And.FunctionName => ParseAnd(state),
            Or.FunctionName => ParseOr(state),
            Not.FunctionName => ParseNot(state),
            // Collection
            All.FunctionName => ParseAll(state),
            Any.FunctionName => ParseAny(state),
            // Boolean expressions
            Equal.FunctionName => ParseEqual(state),
            NotEqual.FunctionName => ParseNotEqual(state),
            GreaterThan.FunctionName => ParseGreaterThan(state),
            GreaterThanOrEqual.FunctionName => ParseGreaterThanOrEqual(state),
            LessThan.FunctionName => ParseLessThan(state),
            LessThanOrEqual.FunctionName => ParseLessThanOrEqual(state),
            Has.FunctionName => ParseHas(state),
            StartsWith.FunctionName => ParseStartsWith(state),
            EndsWith.FunctionName => ParseEndsWith(state),
            RegexMatch.FunctionName => ParseRegexMatch(state),
            _ => throw state.Lexer.SyntaxException(SyntaxErrors.UnknownIdentifier(token.Text, token.Position))
        };
    }

    /// <inheritdoc/>
    public And ParseAnd(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(And.FunctionName);
        state.ExpectToken(TokenType.OpenParen);

        var left = ParseBooleanExpression(state);
        state.ExpectToken(TokenType.Comma);
        do
        {
            var right = ParseBooleanExpression(state);
            // Even though nodes can be nested, they share the same metadata as the start node
            left = new And(left, right, metadata);
        } while (state.ExpectOptionalToken(TokenType.Comma));

        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return (And)left;
    }

    /// <inheritdoc/>
    public Or ParseOr(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Or.FunctionName);
        state.ExpectToken(TokenType.OpenParen);

        var left = ParseBooleanExpression(state);
        state.ExpectToken(TokenType.Comma);
        do
        {
            var right = ParseBooleanExpression(state);
            // Even though nodes can be nested, they share the same metadata as the start node
            left = new Or(left, right, metadata);
        } while (state.ExpectOptionalToken(TokenType.Comma));

        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return (Or)left;
    }

    /// <inheritdoc/>
    public Not ParseNot(IParserState state)
    {
        state.IncreaseDepth();

        var metadata = state.CreateMetadata();
        state.ExpectIdentifier(Not.FunctionName);
        state.ExpectToken(TokenType.OpenParen);
        var result = ParseBooleanExpression(state);
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return new Not(result, metadata);
    }

    /// <summary>
    /// Parses collection expression with given <paramref name="identifier"/>, creating the
    /// <see cref="BooleanExpression"/> using the <paramref name="creator"/>.
    /// </summary>
    /// <param name="state">State of the parser.</param>
    /// <param name="identifier">Identifier of the expression.</param>
    /// <param name="creator">Function to create the parsed node.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <returns>The parsed node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    private T ParseAnyAll<T>(
        IParserState state,
        string identifier,
        Func<FieldPath, BooleanExpression, Metadata, T> creator
    )
    {
        state.IncreaseDepth();
        // any(someList, eq($it.someField, "string"))

        var metadata = state.CreateMetadata();
        // any
        state.ExpectIdentifier(identifier);
        // (
        state.ExpectToken(TokenType.OpenParen);
        // someList
        var fieldPath = ParseFieldPath(state);
        // ,
        state.ExpectToken(TokenType.Comma);
        // eq($it.someField, "string")
        // Collection predicates reference a list item, so require the parent field as context
        state.EnterContext(new ListItemParseContext(fieldPath));
        var expression = ParseBooleanExpression(state);
        state.ExitContext();
        // )
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return creator(fieldPath, expression, metadata);
    }

    /// <inheritdoc/>
    public All ParseAll(IParserState state) => ParseAnyAll(
        state,
        All.FunctionName,
        (fieldPath, expression, metadata) => new All(fieldPath, expression, metadata)
    );

    /// <inheritdoc/>
    public Any ParseAny(IParserState state) => ParseAnyAll(
        state,
        Any.FunctionName,
        (fieldPath, expression, metadata) => new Any(fieldPath, expression, metadata)
    );

    /// <summary>
    /// Parses comparison expression with given <paramref name="identifier"/>, creating the
    /// <see cref="BooleanExpression"/> using the <paramref name="creator"/>.
    /// </summary>
    /// <param name="state">State of the parser.</param>
    /// <param name="identifier">Identifier of the expression.</param>
    /// <param name="creator">Function to create the parsed node.</param>
    /// <typeparam name="T">Type of the node.</typeparam>
    /// <returns>The parsed node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    private T ParseComparison<T>(
        IParserState state,
        string identifier,
        Func<FieldArgument, Constant, Metadata, T> creator
    )
    {
        state.IncreaseDepth();
        // eq(someField, "string")

        var metadata = state.CreateMetadata();
        // eq
        state.ExpectIdentifier(identifier);
        // (
        state.ExpectToken(TokenType.OpenParen);
        // someField
        var fieldArgument = ParseFieldArgument(state);
        // ,
        state.ExpectToken(TokenType.Comma);
        // "string"
        // Constant parser may require context to determine how to parse constant
        state.EnterContext(new FilterConstantParseContext(identifier, fieldArgument));
        var constant = ParseConstant(state);
        state.ExitContext();
        // )
        state.ExpectToken(TokenType.CloseParen);

        state.DecreaseDepth();
        return creator(fieldArgument, constant, metadata);
    }

    /// <inheritdoc/>
    public Equal ParseEqual(IParserState state) => ParseComparison(
        state,
        Equal.FunctionName,
        (function, constant, metadata) => new Equal(function, constant, metadata)
    );

    /// <inheritdoc/>
    public NotEqual ParseNotEqual(IParserState state) => ParseComparison(
        state,
        NotEqual.FunctionName,
        (function, constant, metadata) => new NotEqual(function, constant, metadata)
    );

    /// <inheritdoc/>
    public GreaterThan ParseGreaterThan(IParserState state) => ParseComparison(
        state,
        GreaterThan.FunctionName,
        (function, constant, metadata) => new GreaterThan(function, constant, metadata)
    );

    /// <inheritdoc/>
    public GreaterThanOrEqual ParseGreaterThanOrEqual(IParserState state) => ParseComparison(
        state,
        GreaterThanOrEqual.FunctionName,
        (function, constant, metadata) => new GreaterThanOrEqual(function, constant, metadata)
    );

    /// <inheritdoc/>
    public LessThan ParseLessThan(IParserState state) => ParseComparison(
        state,
        LessThan.FunctionName,
        (function, constant, metadata) => new LessThan(function, constant, metadata)
    );

    /// <inheritdoc/>
    public LessThanOrEqual ParseLessThanOrEqual(IParserState state) => ParseComparison(
        state,
        LessThanOrEqual.FunctionName,
        (function, constant, metadata) => new LessThanOrEqual(function, constant, metadata)
    );

    /// <inheritdoc/>
    public Has ParseHas(IParserState state) => ParseComparison(
        state,
        Has.FunctionName,
        (function, constant, metadata) => new Has(function, constant, metadata)
    );

    /// <inheritdoc/>
    public StartsWith ParseStartsWith(IParserState state) => ParseComparison(
        state,
        StartsWith.FunctionName,
        (function, constant, metadata) => new StartsWith(function, constant, metadata)
    );

    /// <inheritdoc/>
    public EndsWith ParseEndsWith(IParserState state) => ParseComparison(
        state,
        EndsWith.FunctionName,
        (function, constant, metadata) => new EndsWith(function, constant, metadata)
    );

    /// <inheritdoc/>
    public RegexMatch ParseRegexMatch(IParserState state) => ParseComparison(
        state,
        RegexMatch.FunctionName,
        (function, constant, metadata) => new RegexMatch(function, constant, metadata)
    );

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

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state) => _constantParser.ParseConstant(state);
}