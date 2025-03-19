// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Visitors;
using Moq;
using Xunit;

namespace FunQL.Core.Tests.Filtering.Visitors;

public class FilterPrintVisitorTests
{
    /// <summary>
    /// Creates the <see cref="FilterPrintVisitor{IPrintVisitorState}"/>, mocking
    /// <see cref="IConstantVisitor{IPrintVisitorState}"/> to simply always write <see cref="Constant.Value"/> to avoid
    /// relying on a specific implementation in these tests.
    /// </summary>
    private static FilterPrintVisitor<IPrintVisitorState> CreateFilterPrintVisitor()
    {
        var constantVisitorMock = new Mock<IConstantVisitor<IPrintVisitorState>>();
        constantVisitorMock
            .Setup(it => it.Visit(
                It.IsAny<Constant>(),
                It.IsAny<IPrintVisitorState>(),
                It.IsAny<CancellationToken>()
            ))
            .Returns<Constant, IPrintVisitorState, CancellationToken>((it1, it2, it3) =>
            {
                it2.TextWriter.Write(it1.Value);
                return Task.CompletedTask;
            });
        return new FilterPrintVisitor<IPrintVisitorState>(
            new FieldArgumentPrintVisitor<IPrintVisitorState>(
                new FieldFunctionPrintVisitor<IPrintVisitorState>(
                    new FieldPathPrintVisitor<IPrintVisitorState>()
                )
            ),
            constantVisitorMock.Object
        );
    }

    [Theory]
    [MemberData(nameof(FilterTestData.BooleanExpressions), MemberType = typeof(FilterTestData))]
    public async Task Visit_ShouldPrintExpectedBooleanExpression(string text, BooleanExpression expression)
    {
        /* Arrange */
        // Test data has spaces for readability purposes, but current TextWriter does not write spaces, so remove them
        var expected = text.Replace(" ", "");
        var visitor = CreateFilterPrintVisitor();
        var writer = new StringWriter();
        var state = new PrintVisitorState(writer);

        /* Act */
        await visitor.Visit(expression, state, CancellationToken.None);
        var result = writer.ToString();

        /* Assert */
        Assert.Equal(expected, result);
    }
}