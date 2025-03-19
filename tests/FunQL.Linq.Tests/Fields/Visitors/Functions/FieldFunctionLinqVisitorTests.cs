// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Fields.Visitors.Functions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using Xunit;

namespace FunQL.Linq.Tests.Fields.Visitors.Functions;

public class FieldFunctionLinqVisitorTests
{
    private readonly IObjectTypeConfig _objectTypeConfig;

    public FieldFunctionLinqVisitorTests()
    {
        var builder = new ObjectTypeConfigBuilder<TestObject>(
            new ObjectTypeConfigBuilder(new MutableObjectTypeConfig(typeof(TestObject)))
        );
        builder.SimpleField(it => it.DateTimeField);
        builder.SimpleField(it => it.DecimalField);
        builder.SimpleField(it => it.DoubleField);
        builder.SimpleField(it => it.StringField);
        builder.ObjectField(it => it.NestedField).Configure(nestedObject =>
        {
            nestedObject.SimpleField(it => it!.DateTimeField);
            nestedObject.SimpleField(it => it!.DecimalField);
            nestedObject.SimpleField(it => it!.DoubleField);
            nestedObject.SimpleField(it => it!.StringField);
        });
        _objectTypeConfig = builder.Build();
    }

    [Theory]
    [MemberData(nameof(GetNonNullTestData))]
    [MemberData(nameof(GetNullableTestData))]
    [MemberData(nameof(GetNestedNullableTestData))]
    public async Task Visit_ShouldSetFunctionCallResultForFieldFunction(
        FieldFunction fieldFunction, bool handleNullPropagation, object source, object? expectedValue
    )
    {
        /* Arrange */
        var visitor = new FieldFunctionLinqVisitor(
            FieldFunctionLinqTranslators.DefaultTranslators,
            new FieldPathLinqVisitor()
        );
        var sourceExpression = Expression.Parameter(source.GetType(), "it");
        var state = new LinqVisitorState(sourceExpression, handleNullPropagation);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        await visitor.Visit(fieldFunction, state, CancellationToken.None);
        var result = state.Result;

        /* Assert */
        Assert.NotNull(result);
        var value = Expression.Lambda(result, sourceExpression).Compile().DynamicInvoke(source);
        Assert.Equal(expectedValue, value);
    }

    public static TheoryData<FieldFunction, bool, object, object?> GetNonNullTestData()
    {
        var testObject = new TestObject();
        var testData = BuildTestData(testObject, false);
        testData.Add(
            new IsNull(new FieldPath([new NamedField(nameof(TestObject.StringField))])),
            false,
            testObject,
            false
        );
        testData.Add(
            new IsNull(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.StringField))
            ])),
            false,
            testObject,
            false
        );
        return testData;
    }

    public static TheoryData<FieldFunction, bool, object, object?> GetNullableTestData()
    {
        var testObject = new TestObject
        {
            DateTimeField = null,
            DecimalField = null,
            DoubleField = null,
            StringField = null,
            NestedField = null,
        };
        var testData = BuildTestData(testObject, true);
        testData.Add(
            new IsNull(new FieldPath([new NamedField(nameof(TestObject.StringField))])),
            true,
            testObject,
            true
        );
        testData.Add(
            new IsNull(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.StringField))
            ])),
            true,
            testObject,
            true
        );
        return testData;
    }

    public static TheoryData<FieldFunction, bool, object, object?> GetNestedNullableTestData()
    {
        var testObject = new TestObject
        {
            NestedField = new NestedObject
            {
                DateTimeField = null,
                DecimalField = null,
                DoubleField = null,
                StringField = null,
            },
        };
        var testData = BuildTestData(testObject, true);
        testData.Add(
            new IsNull(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.StringField))
            ])),
            true,
            testObject,
            true
        );
        return testData;
    }

    private static TheoryData<FieldFunction, bool, object, object?> BuildTestData(
        TestObject testObject,
        bool handleNullPropagation
    ) => new()
    {
        {
            new Year(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Year
        },
        {
            new Month(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Month
        },
        {
            new Day(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Day
        },
        {
            new Hour(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Hour
        },
        {
            new Minute(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Minute
        },
        {
            new Second(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Second
        },
        {
            new Millisecond(new FieldPath([new NamedField(nameof(TestObject.DateTimeField))])),
            handleNullPropagation,
            testObject,
            testObject.DateTimeField?.Millisecond
        },
        {
            new Floor(new FieldPath([new NamedField(nameof(TestObject.DecimalField))])),
            handleNullPropagation,
            testObject,
            testObject.DecimalField != null ? Math.Floor(testObject.DecimalField!.Value) : null
        },
        {
            new Ceiling(new FieldPath([new NamedField(nameof(TestObject.DecimalField))])),
            handleNullPropagation,
            testObject,
            testObject.DecimalField != null ? Math.Ceiling(testObject.DecimalField!.Value) : null
        },
        {
            new Round(new FieldPath([new NamedField(nameof(TestObject.DecimalField))])),
            handleNullPropagation,
            testObject,
            testObject.DecimalField != null ? Math.Round(testObject.DecimalField!.Value) : null
        },
        {
            new Floor(new FieldPath([new NamedField(nameof(TestObject.DoubleField))])),
            handleNullPropagation,
            testObject,
            testObject.DoubleField != null ? Math.Floor(testObject.DoubleField!.Value) : null
        },
        {
            new Ceiling(new FieldPath([new NamedField(nameof(TestObject.DoubleField))])),
            handleNullPropagation,
            testObject,
            testObject.DoubleField != null ? Math.Ceiling(testObject.DoubleField!.Value) : null
        },
        {
            new Round(new FieldPath([new NamedField(nameof(TestObject.DoubleField))])),
            handleNullPropagation,
            testObject,
            testObject.DoubleField != null ? Math.Round(testObject.DoubleField!.Value) : null
        },
        {
            new Lower(new FieldPath([new NamedField(nameof(TestObject.StringField))])),
            handleNullPropagation,
            testObject,
            testObject.StringField?.ToLower()
        },
        {
            new Upper(new FieldPath([new NamedField(nameof(TestObject.StringField))])),
            handleNullPropagation,
            testObject,
            testObject.StringField?.ToUpper()
        },
        // NestedObject
        {
            new Year(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Year
        },
        {
            new Month(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Month
        },
        {
            new Day(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Day
        },
        {
            new Hour(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Hour
        },
        {
            new Minute(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Minute
        },
        {
            new Second(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Second
        },
        {
            new Millisecond(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DateTimeField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DateTimeField?.Millisecond
        },
        {
            new Floor(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DecimalField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DecimalField != null
                ? Math.Floor(testObject.NestedField!.DecimalField!.Value)
                : null
        },
        {
            new Ceiling(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DecimalField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DecimalField != null
                ? Math.Ceiling(testObject.NestedField!.DecimalField!.Value)
                : null
        },
        {
            new Round(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DecimalField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DecimalField != null
                ? Math.Round(testObject.NestedField!.DecimalField!.Value)
                : null
        },
        {
            new Floor(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DoubleField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DoubleField != null ? Math.Floor(testObject.DoubleField!.Value) : null
        },
        {
            new Ceiling(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DoubleField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DoubleField != null
                ? Math.Ceiling(testObject.NestedField!.DoubleField!.Value)
                : null
        },
        {
            new Round(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.DoubleField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.DoubleField != null ? Math.Round(testObject.NestedField!.DoubleField!.Value) : null
        },
        {
            new Lower(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.StringField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.ToLower()
        },
        {
            new Upper(new FieldPath([
                new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(TestObject.StringField))
            ])),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.ToUpper()
        },
    };

    private class TestObject
    {
        public DateTime? DateTimeField { get; set; } = DateTime.Parse("2024-03-16T12:30:45.1234000Z");
        public decimal? DecimalField { get; set; } = new(1.7);
        public double? DoubleField { get; set; } = 1.7;
        public string? StringField { get; set; } = "StringValue";
        public NestedObject? NestedField { get; set; } = new();
    }

    private class NestedObject
    {
        public DateTime? DateTimeField { get; set; } = DateTime.Parse("2024-03-16T12:30:45.1234000Z");
        public decimal? DecimalField { get; set; } = new(1.7);
        public double? DoubleField { get; set; } = 1.7;
        public string? StringField { get; set; } = "StringValue";
    }
}