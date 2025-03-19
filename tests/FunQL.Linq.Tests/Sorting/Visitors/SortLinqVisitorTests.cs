// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Sorting.Nodes;
using FunQL.Linq.Fields.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Fields.Visitors.Functions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using FunQL.Linq.Sorting.Visitors;
using FunQL.Linq.Utils;
using Xunit;

namespace FunQL.Linq.Tests.Sorting.Visitors;

public class SortLinqVisitorTests
{
    private readonly IObjectTypeConfig _objectTypeConfig;

    public SortLinqVisitorTests()
    {
        var builder = new ObjectTypeConfigBuilder<TestObject>(
            new ObjectTypeConfigBuilder(new MutableObjectTypeConfig(typeof(TestObject)))
        );
        builder.SimpleField(it => it.StringField);
        builder.SimpleField(it => it.IntField);
        builder.ObjectField(it => it.NestedField).Configure(nestedField =>
        {
            nestedField.SimpleField(it => it!.StringField);
            nestedField.SimpleField(it => it!.IntField);
        });
        _objectTypeConfig = builder.Build();
    }

    [Theory]
    [MemberData(nameof(GetNonNullTestData))]
    [MemberData(nameof(GetNullableTestData))]
    [MemberData(nameof(GetNestedNullableTestData))]
    public async Task Visit_ShouldSetFilterResultForFilter(
        Sort sort,
        bool handleNullPropagation,
        List<TestObject> source,
        List<TestObject> expectedValue
    )
    {
        /* Arrange */
        var visitor = new SortLinqVisitor(
            new FieldArgumentLinqVisitor(
                new FieldFunctionLinqVisitor(
                    FieldFunctionLinqTranslators.DefaultTranslators,
                    new FieldPathLinqVisitor()
                )
            )
        );
        var sourceExpression = Expression.Parameter(typeof(TestObject), "it");
        var state = new SortLinqVisitorState(sourceExpression, handleNullPropagation);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        await visitor.Visit(sort, state, CancellationToken.None);
        var result = state.SortExpressions;

        /* Assert */
        Assert.NotNull(result);
        var sourceQueryable = source.AsQueryable();
        IQueryable orderedResult = sourceQueryable;
        foreach (var sortLinqExpression in result)
        {
            orderedResult = sortLinqExpression.Direction == SortDirection.Ascending
                ? QueryableMethodInfoUtil.InvokeOrderBy(orderedResult, sortLinqExpression.KeySelectorExpression)
                : QueryableMethodInfoUtil.InvokeOrderByDescending(
                    orderedResult,
                    sortLinqExpression.KeySelectorExpression
                );
        }

        var typedResult = (IOrderedQueryable<TestObject>)orderedResult;
        var value = typedResult.ToList();
        Assert.Equal(expectedValue, value);
    }

    public static TheoryData<Sort, bool, List<TestObject>, List<TestObject>> GetNonNullTestData()
    {
        var testObjects = new List<TestObject>
        {
            new()
            {
                IntField = 10,
                NestedField = new NestedObject
                {
                    IntField = 20
                }
            },
            new()
            {
                IntField = 20,
                NestedField = new NestedObject
                {
                    IntField = 10
                }
            }
        };
        var testData = BuildTestData(testObjects, false);
        return testData;
    }

    public static TheoryData<Sort, bool, List<TestObject>, List<TestObject>> GetNullableTestData()
    {
        var testObjects = new List<TestObject>
        {
            new()
            {
                IntField = null,
                NestedField = null
            },
            new()
            {
                IntField = 20,
                NestedField = new NestedObject
                {
                    IntField = 10
                }
            }
        };
        var testData = BuildTestData(testObjects, true);
        return testData;
    }

    public static TheoryData<Sort, bool, List<TestObject>, List<TestObject>> GetNestedNullableTestData()
    {
        var testObjects = new List<TestObject>
        {
            new()
            {
                IntField = null,
                NestedField = new NestedObject
                {
                    IntField = null
                }
            },
            new()
            {
                IntField = 20,
                NestedField = new NestedObject
                {
                    IntField = 10
                }
            }
        };
        var testData = BuildTestData(testObjects, true);
        return testData;
    }

    private static TheoryData<Sort, bool, List<TestObject>, List<TestObject>> BuildTestData(
        List<TestObject> testObjects,
        bool handleNullPropagation
    ) => new()
    {
        {
            new Sort([new Ascending(new FieldPath([new NamedField(nameof(TestObject.IntField))]))]),
            handleNullPropagation,
            testObjects,
            testObjects.OrderBy(it => it.IntField).ToList()
        },
        {
            new Sort([new Descending(new FieldPath([new NamedField(nameof(TestObject.IntField))]))]),
            handleNullPropagation,
            testObjects,
            testObjects.OrderByDescending(it => it.IntField).ToList()
        },
        // NestedObject
        {
            new Sort([
                new Ascending(new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]))
            ]),
            handleNullPropagation,
            testObjects,
            testObjects.OrderBy(it => it.NestedField?.IntField).ToList()
        },
        {
            new Sort([
                new Descending(new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]))
            ]),
            handleNullPropagation,
            testObjects,
            testObjects.OrderByDescending(it => it.NestedField?.IntField).ToList()
        },
    };

    public class TestObject
    {
        public string? StringField { get; set; } = "StringValue";
        public int? IntField { get; set; } = 10;
        public NestedObject? NestedField { get; set; } = new();
    }

    public class NestedObject
    {
        public string? StringField { get; set; } = "StringValue";
        public int? IntField { get; set; } = 10;
    }
}