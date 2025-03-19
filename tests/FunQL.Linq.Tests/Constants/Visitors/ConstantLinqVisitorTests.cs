// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Constants.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Constants.Visitors;
using Xunit;

namespace FunQL.Linq.Tests.Constants.Visitors;

public class ConstantLinqVisitorTests
{
    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task Visit_ShouldSetConstantExpressionResult(Constant constant)
    {
        /* Arrange */
        var visitor = new ConstantLinqVisitor();
        var state = new LinqVisitorState(Expression.Parameter(typeof(object), "it"));

        /* Act */
        await visitor.Visit(constant, state, CancellationToken.None);
        var result = state.Result;

        /* Assert */
        Assert.NotNull(result);
        var typed = Assert.IsType<ConstantExpression>(result);
        Assert.Equal(constant.Value, typed.Value);
    }

    public static TheoryData<Constant> GetTestData() =>
    [
        new("string"),
        new(1),
        new(new { Test = 5 }),
        new(null),
        // ReSharper disable once RedundantCast
        new((int?)null)
    ];
}