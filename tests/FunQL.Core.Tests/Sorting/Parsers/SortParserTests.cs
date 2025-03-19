// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Lexers;
using FunQL.Core.Schemas;
using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Parsers;
using Xunit;

namespace FunQL.Core.Tests.Sorting.Parsers;

public class SortParserTests
{
    [Fact]
    public void ParseSort_ShouldReturnSortNode_WhenTextIsValidSort()
    {
        /* Arrange */
        var lexer = new StringLexer("sort(asc(test),desc(lower(test)))");
        var parserState = new ParserState(lexer);
        var parser = new SortParser(new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser())));

        /* Act */
        var result = parser.ParseSort(parserState);

        /* Assert */
        Assert.Equal(2, result.Expressions.Count);
        
        // Assert first expression
        var first = Assert.IsType<Ascending>(result.Expressions[0]);
        var firstPath = Assert.IsType<FieldPath>(first.FieldArgument);
        Assert.Single(firstPath.Fields);
        var firstField = Assert.IsType<NamedField>(firstPath.Fields.Single());
        Assert.Equal("test", firstField.Name);
        
        // Assert second expression
        var second = Assert.IsType<Descending>(result.Expressions[1]);
        var secondLower = Assert.IsType<Lower>(second.FieldArgument);
        var secondPath = secondLower.FieldPath;
        Assert.Single(secondPath.Fields);
        var secondField = Assert.IsType<NamedField>(secondPath.Fields.Single());
        Assert.Equal("test", secondField.Name);
    }
}