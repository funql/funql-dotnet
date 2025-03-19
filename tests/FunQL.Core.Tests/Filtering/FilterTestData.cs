// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Filtering.Nodes;
using Xunit;

namespace FunQL.Core.Tests.Filtering;

public static class FilterTestData
{
    public static TheoryData<string, BooleanExpression> BooleanExpressions() => new()
    {
        { "eq(field, 0)", new Equal(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "neq(field, 0)", new NotEqual(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "gt(field, 0)", new GreaterThan(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "gte(field, 0)", new GreaterThanOrEqual(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "lt(field, 0)", new LessThan(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "lte(field, 0)", new LessThanOrEqual(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "stw(field, 0)", new StartsWith(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "enw(field, 0)", new EndsWith(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "has(field, 0)", new Has(new FieldPath([new NamedField("field")]), new Constant(0)) },
        { "reg(field, 0)", new RegexMatch(new FieldPath([new NamedField("field")]), new Constant(0)) },
        {
            "not(eq(field, 0))",
            new Not(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
            )
        },
        {
            "and(eq(field, 0), eq(field, 0))",
            new And(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
            )
        },
        {
            "and(eq(field, 0), and(eq(field, 0), eq(field, 0)))",
            new And(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                new And(
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
                )
            )
        },
        {
            "or(eq(field, 0), eq(field, 0))",
            new Or(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
            )
        },
        {
            "or(eq(field, 0), or(eq(field, 0), eq(field, 0)))",
            new Or(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                new Or(
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
                )
            )
        },
        {
            "any(list, eq($it.field, 0))",
            new Any(
                // list
                new FieldPath([new NamedField("list")]),
                new Equal(new FieldPath([
                    // $it
                    new ListItemField(new FieldPath([new NamedField("list")])),
                    // field
                    new NamedField("field")
                ]), new Constant(0))
            )
        },
        {
            "all(list, eq($it.field, 0))",
            new All(
                // list
                new FieldPath([new NamedField("list")]),
                new Equal(new FieldPath([
                    // $it
                    new ListItemField(new FieldPath([new NamedField("list")])),
                    // field
                    new NamedField("field")
                ]), new Constant(0))
            )
        },
    };

    /// <summary>
    /// 'and()' and 'or()' with more than two expressions. These are special as they don't translate 1:1 to nodes, but
    /// instead are only supported when parsing and translate to nested 'And' and 'Or' nodes instead.
    /// </summary>
    public static TheoryData<string, BooleanExpression> AndOrMoreThanTwoExpressions() => new()
    {
        {
            "and(eq(field, 0), eq(field, 0), eq(field, 0))",
            new And(
                new And(
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
                ),
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
            )
        },
        {
            "or(eq(field, 0), or(eq(field, 0), eq(field, 0)))",
            new Or(
                new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                new Or(
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0)),
                    new Equal(new FieldPath([new NamedField("field")]), new Constant(0))
                )
            )
        },
    };
}