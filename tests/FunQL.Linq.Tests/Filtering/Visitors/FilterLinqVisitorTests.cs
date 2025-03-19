// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Filtering.Nodes;
using FunQL.Linq.Common.Visitors;
using FunQL.Linq.Constants.Visitors;
using FunQL.Linq.Fields.Visitors;
using FunQL.Linq.Fields.Visitors.Fields;
using FunQL.Linq.Fields.Visitors.Functions;
using FunQL.Linq.Fields.Visitors.Functions.Translators;
using FunQL.Linq.Filtering.Visitors;
using FunQL.Linq.Filtering.Visitors.Translators;
using Xunit;
using LessThan = FunQL.Core.Filtering.Nodes.LessThan;

namespace FunQL.Linq.Tests.Filtering.Visitors;

public class FilterLinqVisitorTests
{
    private readonly IObjectTypeConfig _objectTypeConfig;

    public FilterLinqVisitorTests()
    {
        var builder = new ObjectTypeConfigBuilder<TestObject>(
            new ObjectTypeConfigBuilder(new MutableObjectTypeConfig(typeof(TestObject)))
        );
        builder.SimpleField(it => it.StringField);
        builder.SimpleField(it => it.IntField);
        builder.ListField(it => it.NestedFieldList!).Configure(nestedFieldList =>
        {
            nestedFieldList.ObjectItem().Configure(nestedField =>
            {
                nestedField.SimpleField(it => it!.StringField);
                nestedField.SimpleField(it => it!.IntField);
            });
        });
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
        BooleanExpression booleanExpression,
        bool handleNullPropagation,
        object source,
        object? expectedValue
    )
    {
        /* Arrange */
        var visitor = new FilterLinqVisitor(
            BinaryExpressionLinqTranslators.DefaultTranslators,
            new FieldArgumentLinqVisitor(
                new FieldFunctionLinqVisitor(
                    FieldFunctionLinqTranslators.DefaultTranslators,
                    new FieldPathLinqVisitor()
                )
            ),
            new ConstantLinqVisitor()
        );
        var sourceExpression = Expression.Parameter(source.GetType(), "it");
        var state = new LinqVisitorState(sourceExpression, handleNullPropagation);
        state.EnterContext(new FieldPathLinqVisitContext(_objectTypeConfig));

        /* Act */
        await visitor.Visit(booleanExpression, state, CancellationToken.None);
        var result = state.Result;

        /* Assert */
        Assert.NotNull(result);
        var value = Expression.Lambda(result, sourceExpression).Compile().DynamicInvoke(source);
        Assert.Equal(expectedValue, value);
    }

    public static TheoryData<BooleanExpression, bool, object, object?> GetNonNullTestData()
    {
        var testObject = new TestObject();
        var testData = BuildTestData(testObject, false);
        return testData;
    }

    public static TheoryData<BooleanExpression, bool, object, object?> GetNullableTestData()
    {
        var testObject = new TestObject
        {
            StringField = null,
            IntField = null,
            NestedFieldList = null,
            NestedField = null,
        };
        var testData = BuildTestData(testObject, true);
        return testData;
    }

    public static TheoryData<BooleanExpression, bool, object, object?> GetNestedNullableTestData()
    {
        var testObject = new TestObject
        {
            StringField = null,
            IntField = null,
            NestedFieldList = null,
            NestedField = new NestedObject
            {
                StringField = null,
                IntField = null,
            },
        };
        var testData = BuildTestData(testObject, true);
        return testData;
    }

    [SuppressMessage("ReSharper", "EqualExpressionComparison")]
    private static TheoryData<BooleanExpression, bool, object, object?> BuildTestData(
        TestObject testObject,
        bool handleNullPropagation
    ) => new()
    {
        {
            new Equal(new FieldPath([new NamedField(nameof(TestObject.IntField))]), new Constant(testObject.IntField)),
            handleNullPropagation,
            testObject,
            testObject.IntField == testObject.IntField
        },
        {
            new Equal(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField == testObject.IntField + 1
        },
        {
            new NotEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField != testObject.IntField
        },
        {
            new NotEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField != testObject.IntField + 1
        },
        {
            new GreaterThan(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField > testObject.IntField
        },
        {
            new GreaterThan(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField - 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField > testObject.IntField - 1
        },
        {
            new GreaterThanOrEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField >= testObject.IntField
        },
        {
            new GreaterThanOrEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField >= testObject.IntField + 1
        },
        {
            new LessThan(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField < testObject.IntField
        },
        {
            new LessThan(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField < testObject.IntField + 1
        },
        {
            new LessThanOrEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField <= testObject.IntField
        },
        {
            new LessThanOrEqual(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField - 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField <= testObject.IntField - 1
        },
        {
            new StartsWith(new FieldPath([new NamedField(nameof(TestObject.StringField))]), new Constant("String")),
            handleNullPropagation,
            testObject,
            testObject.StringField?.StartsWith("String")
        },
        {
            new StartsWith(new FieldPath([new NamedField(nameof(TestObject.StringField))]), new Constant("abcdef")),
            handleNullPropagation,
            testObject,
            testObject.StringField?.StartsWith("abcdef")
        },
        {
            new EndsWith(new FieldPath([new NamedField(nameof(TestObject.StringField))]), new Constant("Value")),
            handleNullPropagation,
            testObject,
            testObject.StringField?.EndsWith("Value")
        },
        {
            new EndsWith(new FieldPath([new NamedField(nameof(TestObject.StringField))]), new Constant("abcdef")),
            handleNullPropagation,
            testObject,
            testObject.StringField?.EndsWith("abcdef")
        },
        {
            new RegexMatch(new FieldPath([new NamedField(nameof(TestObject.StringField))]), new Constant("[a-zA-Z]*")),
            handleNullPropagation,
            testObject,
            testObject.StringField != null ? Regex.IsMatch(testObject.StringField, "[a-zA-Z]*") : null
        },
        {
            new Not(new Equal(
                new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                new Constant(testObject.IntField)
            )),
            handleNullPropagation,
            testObject,
            !(testObject.IntField == testObject.IntField)
        },
        {
            new And(
                new Equal(
                    new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                    new Constant(testObject.IntField)
                ),
                new Equal(
                    new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                    new Constant(testObject.IntField - 1)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField == testObject.IntField && testObject.IntField == testObject.IntField - 1
        },
        {
            new Or(
                new Equal(
                    new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                    new Constant(testObject.IntField)
                ),
                new Equal(
                    new FieldPath([new NamedField(nameof(TestObject.IntField))]),
                    new Constant(testObject.IntField - 1)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.IntField == testObject.IntField || testObject.IntField == testObject.IntField - 1
        },
        {
            new Any(
                new FieldPath([new NamedField(nameof(TestObject.NestedFieldList))]),
                new GreaterThan(
                    new FieldPath([
                        new ListItemField(new FieldPath([new NamedField(nameof(TestObject.NestedFieldList))])),
                        new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(2)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedFieldList?.Any(it => it.IntField == 2)
        },
        {
            new All(
                new FieldPath([new NamedField(nameof(TestObject.NestedFieldList))]),
                new GreaterThan(
                    new FieldPath([
                        new ListItemField(new FieldPath([new NamedField(nameof(TestObject.NestedFieldList))])),
                        new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(2)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedFieldList?.All(it => it.IntField == 2)
        },
        // NestedObject
        {
            new Equal(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField == testObject.NestedField?.IntField
        },
        {
            new Equal(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField == testObject.NestedField?.IntField + 1
        },
        {
            new NotEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField != testObject.NestedField?.IntField
        },
        {
            new NotEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField != testObject.NestedField?.IntField + 1
        },
        {
            new GreaterThan(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField > testObject.NestedField?.IntField
        },
        {
            new GreaterThan(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField - 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField > testObject.NestedField?.IntField - 1
        },
        {
            new GreaterThanOrEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField >= testObject.NestedField?.IntField
        },
        {
            new GreaterThanOrEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField >= testObject.NestedField?.IntField + 1
        },
        {
            new LessThan(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField < testObject.NestedField?.IntField
        },
        {
            new LessThan(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField + 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField < testObject.NestedField?.IntField + 1
        },
        {
            new LessThanOrEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField <= testObject.NestedField?.IntField
        },
        {
            new LessThanOrEqual(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField - 1)
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField <= testObject.NestedField?.IntField - 1
        },
        {
            new StartsWith(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.StringField))
                ]),
                new Constant("String")
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.StartsWith("String")
        },
        {
            new StartsWith(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.StringField))
                ]),
                new Constant("abcdef")
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.StartsWith("abcdef")
        },
        {
            new EndsWith(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.StringField))
                ]),
                new Constant("Value")
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.EndsWith("Value")
        },
        {
            new EndsWith(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.StringField))
                ]),
                new Constant("abcdef")
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField?.EndsWith("abcdef")
        },
        {
            new RegexMatch(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.StringField))
                ]),
                new Constant("[a-zA-Z]*")
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.StringField != null
                ? Regex.IsMatch(testObject.NestedField.StringField, "[a-zA-Z]*")
                : null
        },
        {
            new Not(new Equal(
                new FieldPath([
                    new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                ]),
                new Constant(testObject.NestedField?.IntField))
            ),
            handleNullPropagation,
            testObject,
            !(testObject.NestedField?.IntField == testObject.NestedField?.IntField)
        },
        {
            new And(
                new Equal(
                    new FieldPath([
                        new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(testObject.NestedField?.IntField)
                ),
                new Equal(
                    new FieldPath([
                        new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(testObject.NestedField?.IntField - 1)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField == testObject.NestedField?.IntField &&
            testObject.NestedField?.IntField == testObject.NestedField?.IntField - 1
        },
        {
            new Or(
                new Equal(
                    new FieldPath([
                        new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(testObject.NestedField?.IntField)
                ),
                new Equal(
                    new FieldPath([
                        new NamedField(nameof(TestObject.NestedField)), new NamedField(nameof(NestedObject.IntField))
                    ]),
                    new Constant(testObject.NestedField?.IntField - 1)
                )
            ),
            handleNullPropagation,
            testObject,
            testObject.NestedField?.IntField == testObject.NestedField?.IntField ||
            testObject.NestedField?.IntField == testObject.NestedField?.IntField - 1
        },
    };

    private class TestObject
    {
        public string? StringField { get; set; } = "StringValue";
        public int? IntField { get; set; } = 10;

        public List<NestedObject>? NestedFieldList { get; set; } =
        [
            new NestedObject
            {
                IntField = 1,
            },
            new NestedObject
            {
                IntField = 2,
            },
            new NestedObject
            {
                IntField = 3,
            },
        ];

        public NestedObject? NestedField { get; set; } = new();
    }

    private class NestedObject
    {
        public string? StringField { get; set; } = "StringValue";
        public int? IntField { get; set; } = 10;
    }
}