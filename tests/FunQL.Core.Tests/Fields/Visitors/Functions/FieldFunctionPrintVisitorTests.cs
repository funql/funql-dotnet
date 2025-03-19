// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Core.Fields.Visitors.Functions;
using Xunit;

namespace FunQL.Core.Tests.Fields.Visitors.Functions;

public class FieldFunctionPrintVisitorTests
{
    [Theory]
    [MemberData(nameof(FieldFunctionTestData.AllFunctions), MemberType = typeof(FieldFunctionTestData))]
    public async Task Visit_ShouldPrintExpectedFieldFunction(string expected, FieldFunction fieldFunction)
    {
        /* Arrange */
        var visitor = new FieldFunctionPrintVisitor<IPrintVisitorState>(
            new FieldPathPrintVisitor<IPrintVisitorState>()
        );
        var writer = new StringWriter();
        var state = new PrintVisitorState(writer);

        /* Act */
        await visitor.Visit(fieldFunction, state, CancellationToken.None);
        var result = writer.ToString();

        /* Assert */
        Assert.Equal(expected, result);
    }
}