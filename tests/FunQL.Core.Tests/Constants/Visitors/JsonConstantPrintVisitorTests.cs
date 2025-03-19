// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Text.Json;
using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Core.Tests.TestData;
using FunQL.Core.Tests.TestData.Models;
using Moq;
using Xunit;

namespace FunQL.Core.Tests.Constants.Visitors;

public class JsonConstantPrintVisitorTests
{
    public static TheoryData<object?, string> GetTestData() => new()
    {
        { "value", "\"value\"" },
        { 123, "123" },
        { null, "null" },
        {
            new Designer
            {
                Id = Guid.Parse("9535bfa7-341e-4608-93a4-0cd8c5055acd"),
                Name = "Hans Burkhard Schlömer"
            },
            // Object to JSON equality checks are tricky as indentation might differ, so we'll just use the same
            // serializer to assure we get the same string
            JsonSerializer.Serialize(new Designer
            {
                Id = Guid.Parse("9535bfa7-341e-4608-93a4-0cd8c5055acd"),
                Name = "Hans Burkhard Schlömer"
            }, JsonDefaults.JsonSerializerOptions)
        },
        { Theme.StarWars, "\"STAR_WARS\"" }
    };

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task Visit_ShouldPrintConstant(object? input, string expected)
    {
        /* Arrange */
        var node = new Constant(input);
        var cancellationToken = CancellationToken.None;
        var visitor = new JsonConstantPrintVisitor<IPrintVisitorState>(JsonDefaults.JsonSerializerOptions);
        var textWriter = new StringWriter();

        // Mock state to return the StringWriter
        var stateMock = new Mock<IPrintVisitorState>();
        stateMock.Setup(it => it.TextWriter).Returns(textWriter);

        /* Act */
        await visitor.Visit(node, stateMock.Object, cancellationToken);
        var actual = textWriter.ToString();

        /* Assert */
        Assert.Equal(expected, actual);
    }
}