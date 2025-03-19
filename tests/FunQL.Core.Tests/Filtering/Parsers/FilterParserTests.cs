// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections;
using System.Reflection;
using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Extensions;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Parsers;
using FunQL.Core.Lexers;
using Moq;
using Xunit;

namespace FunQL.Core.Tests.Filtering.Parsers;

public class FilterParserTests
{
    /// <summary>
    /// Creates the <see cref="FilterParser"/>, mocking <see cref="IFilterConstantParser"/> to simply always parse text
    /// as <see cref="int"/> to avoid relying on a specific implementation in these tests.
    /// </summary>
    private static FilterParser CreateFilterParser()
    {
        var constantParserMock = new Mock<IFilterConstantParser>();
        constantParserMock.Setup(it => it.ParseConstant(It.IsAny<IParserState>()))
            .Returns<IParserState>(it =>
            {
                var rawText = it.CurrentToken().Text;
                it.NextToken();
                return new Constant(int.Parse(rawText));
            });
        return new FilterParser(
            new FieldArgumentParser(new FieldFunctionParser(new FieldPathParser())),
            constantParserMock.Object
        );
    }

    [Theory]
    [MemberData(nameof(FilterTestData.BooleanExpressions), MemberType = typeof(FilterTestData))]
    [MemberData(nameof(FilterTestData.AndOrMoreThanTwoExpressions), MemberType = typeof(FilterTestData))]
    public void ParseBooleanExpression_ShouldReturnExpectedBooleanExpression(string text, BooleanExpression expected)
    {
        /* Arrange */
        var lexer = new StringLexer(text);
        var parserState = new ParserState(lexer);
        var parser = CreateFilterParser();

        /* Act */
        var result = parser.ParseBooleanExpression(parserState);

        /* Assert */
        Assert.Equal(expected.GetType(), result.GetType());
        
        // Compare properties, but exclude 'Metadata'
        AssertEqualIgnoringProperties(expected, result, [nameof(BooleanExpression.Metadata)]);
    }

    private static void AssertEqualIgnoringProperties(object? expected, object? actual, string[] ignoredProperties)
    {
        if (expected == null || actual == null)
        {
            Assert.Equal(expected, actual);
            return;
        }

        if (expected is IEnumerable expectedCollection && actual is IEnumerable actualCollection)
        {
            var expectedList = expectedCollection.Cast<object>().ToList();
            var actualList = actualCollection.Cast<object>().ToList();
            
            Assert.Equal(expectedList.Count, actualList.Count);
            Assert.All(expectedList, (it, index) => AssertEqualIgnoringProperties(it, actualList[index], ignoredProperties));
            return;
        }

        Assert.Equal(expected.GetType(), actual.GetType());

        var properties = expected.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => !ignoredProperties.Contains(p.Name));

        var nested = false;
        foreach (var property in properties)
        {
            var expectedValue = property.GetValue(expected);
            var actualValue = property.GetValue(actual);

            AssertEqualIgnoringProperties(expectedValue, actualValue, ignoredProperties);
            nested = true;
        }

        if (!nested)
        {
            Assert.Equal(expected, actual);
        }
    }
}