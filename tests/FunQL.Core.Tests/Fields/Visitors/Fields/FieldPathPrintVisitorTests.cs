// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Visitors.Fields;
using Xunit;

namespace FunQL.Core.Tests.Fields.Visitors.Fields;

public class FieldPathPrintVisitorTests
{
    [Theory]
    [MemberData(nameof(FieldPathTestData.NamedFieldPaths), MemberType = typeof(FieldPathTestData))]
    [MemberData(nameof(FieldPathTestData.MapFieldPaths), MemberType = typeof(FieldPathTestData))]
    public async Task Visit_ShouldPrintExpectedFieldPath(string _, FieldPath fieldPath, string expected)
    {
        /* Arrange */
        var visitor = new FieldPathPrintVisitor<IPrintVisitorState>();
        var writer = new StringWriter();
        var state = new PrintVisitorState(writer);

        /* Act */
        await visitor.Visit(fieldPath, state, CancellationToken.None);
        var result = writer.ToString();

        /* Assert */
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task Visit_ShouldPrintListItemFieldPath_WhenVisitingListItemFieldPath()
    {
        /* Arrange */
        var visitor = new FieldPathPrintVisitor<IPrintVisitorState>();
        var writer = new StringWriter();
        var state = new PrintVisitorState(writer);
        var fieldPath = new FieldPath(new List<Field>
        {
            new ListItemField(new FieldPath(new List<Field> { new NamedField("list") })),
            new NamedField("field"),
        });
        const string expected = "$it.field";

        /* Act */
        await visitor.Visit(fieldPath, state, CancellationToken.None);
        var result = writer.ToString();

        /* Assert */
        Assert.Equal(expected, result);
    }
}