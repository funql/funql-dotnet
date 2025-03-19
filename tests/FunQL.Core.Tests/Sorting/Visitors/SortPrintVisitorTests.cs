// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Visitors;
using Xunit;

namespace FunQL.Core.Tests.Sorting.Visitors;

public class SortPrintVisitorTests
{
    [Fact]
    public async Task Visit_ShouldPrintSort()
    {
        /* Arrange */
        var visitor = new SortPrintVisitor<IPrintVisitorState>(new FieldArgumentPrintVisitor<IPrintVisitorState>(
            new FieldFunctionPrintVisitor<IPrintVisitorState>(new FieldPathPrintVisitor<IPrintVisitorState>())
        ));
        var writer = new StringWriter();
        var state = new PrintVisitorState(writer);
        var sort = new Sort(
            new List<SortExpression>
            {
                new Ascending(new FieldPath(new List<Field> { new NamedField("test") })),
                new Descending(new Lower(new FieldPath(new List<Field> { new NamedField("test") })))
            }
        );

        /* Act */
        await visitor.Visit(sort, state, CancellationToken.None);
        var result = writer.ToString();

        /* Assert */
        Assert.Equal("""sort(asc(test),desc(lower(test)))""", result);
    }
}