// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using Xunit;

namespace FunQL.Linq.Tests.Fields.Visitors.Fields;

public class FieldPathLinqVisitorTests
{
    private readonly IObjectTypeConfig _objectTypeConfig;

    public FieldPathLinqVisitorTests()
    {
        var builder = new ObjectTypeConfigBuilder<ParentObject>(
            new ObjectTypeConfigBuilder(new MutableObjectTypeConfig(typeof(ParentObject)))
        );
        builder.SimpleField(it => it.ParentString);
        builder.ObjectField(it => it.Child)
            .Configure(child => { child.SimpleField(it => it!.ChildString); });
        _objectTypeConfig = builder.Build();
    }

    [Theory]
    [MemberData(nameof(GetNonNullTestData))]
    public async Task Visit_ShouldSetMemberAccessResult_WhenFieldPathIsMember(
        FieldPath fieldPath,
        object source,
        object? expectedValue
    )
    {
        /* Arrange */
        var visitor = new FieldPathLinqVisitor();
        var sourceExpression = Expression.Parameter(source.GetType(), "it");
        var state = new LinqVisitorState(sourceExpression);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        await visitor.Visit(fieldPath, state, CancellationToken.None);
        var result = state.Result;

        /* Assert */
        Assert.NotNull(result);
        var value = Expression.Lambda(result, sourceExpression).Compile().DynamicInvoke(source);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [MemberData(nameof(GetNullableTestData))]
    public async Task Visit_ShouldHandleNullableMemberAccessResult_WhenFieldPathIsNullableMember(
        FieldPath fieldPath,
        object source,
        object? expectedValue
    )
    {
        /* Arrange */
        var visitor = new FieldPathLinqVisitor();
        var sourceExpression = Expression.Parameter(source.GetType(), "it");
        var state = new LinqVisitorState(sourceExpression, true);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        await visitor.Visit(fieldPath, state, CancellationToken.None);
        var result = state.Result;

        /* Assert */
        Assert.NotNull(result);
        var value = Expression.Lambda(result, sourceExpression).Compile().DynamicInvoke(source);
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public async Task Visit_ShouldThrowInvalidOperationException_WhenFieldPathInvalid()
    {
        /* Arrange */
        var fieldPath = new FieldPath([new NamedField("NonExistentField")]);
        var visitor = new FieldPathLinqVisitor();
        var sourceExpression = Expression.Parameter(typeof(ParentObject), "it");
        var state = new LinqVisitorState(sourceExpression);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        var action = () => visitor.Visit(fieldPath, state, CancellationToken.None);

        /* Assert */
        await Assert.ThrowsAsync<InvalidOperationException>(action);
    }

    public static TheoryData<FieldPath, object, object?> GetNonNullTestData() => BuildTestData(new ParentObject());

    public static TheoryData<FieldPath, object, object?> GetNullableTestData() => BuildTestData(new ParentObject
    {
        Child = null
    });

    private static TheoryData<FieldPath, object, object?> BuildTestData(ParentObject parentObject) => new()
    {
        {
            new FieldPath([new NamedField(nameof(ParentObject.ParentString))]),
            parentObject,
            parentObject.ParentString
        },
        {
            new FieldPath([new NamedField(nameof(ParentObject.Child))]),
            parentObject,
            parentObject.Child
        },
        {
            new FieldPath([
                new NamedField(nameof(ParentObject.Child)), new NamedField(nameof(ChildObject.ChildString))
            ]),
            parentObject,
            parentObject.Child?.ChildString
        },
    };

    private class ParentObject
    {
        public string ParentString { get; set; } = "ParentStringValue";
        public ChildObject? Child { get; set; } = new();
    }

    private class ChildObject
    {
        public string ChildString { get; set; } = "ChildStringValue";
    }
}