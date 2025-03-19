// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Lexers;
using Xunit;

namespace FunQL.Core.Tests.Fields.Parsers.Fields;

public class FieldPathParserTests
{
    [Theory]
    [MemberData(nameof(FieldPathTestData.NamedFieldPaths), MemberType = typeof(FieldPathTestData))]
    [MemberData(nameof(FieldPathTestData.MapFieldPaths), MemberType = typeof(FieldPathTestData))]
    public void ParseFieldPath_ShouldReturnFieldPath_WhenTextIsFieldPath(string input, FieldPath expected, string _)
    {
        /* Arrange */
        var lexer = new StringLexer(input);
        var parserState = new ParserState(lexer);
        var parser = new FieldPathParser();

        /* Act */
        var result = parser.ParseFieldPath(parserState);

        /* Assert */
        Assert.IsType<FieldPath>(result);
        Assert.Equal(expected.Fields, result.Fields);
    }

    [Fact]
    public void ParseFieldPath_ShouldReturnListItemFieldPath_WhenTextIsListItemFieldPath()
    {
        /* Arrange */
        var lexer = new StringLexer("$it.field");
        var parserState = new ParserState(lexer);
        var parser = new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser()));
        var parent = new FieldPath(new List<Field> { new NamedField("list") });
        parserState.EnterContext(new ListItemParseContext(parent));
        var expected = new FieldPath(
            new List<Field>
            {
                new ListItemField(parent, new Metadata(0)),
                new NamedField("field", new Metadata(4)),
            }
        );

        /* Act */
        var result = parser.ParseFieldArgument(parserState);

        /* Assert */
        var typed = Assert.IsType<FieldPath>(result);
        Assert.Equal(expected.Fields, typed.Fields);
    }

    [Fact]
    public void ParseFieldPath_ShouldThrowInvalidOperationException_WhenListItemParseContextIsMissing()
    {
        /* Arrange */
        var lexer = new StringLexer("$it.field");
        var parserState = new ParserState(lexer);
        var parser = new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser()));

        /* Act */
        var action = () => parser.ParseFieldArgument(parserState);

        /* Assert */
        Assert.Throws<InvalidOperationException>(action);
    }
}