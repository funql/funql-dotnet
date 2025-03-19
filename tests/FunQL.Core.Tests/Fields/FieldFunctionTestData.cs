// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Nodes.Functions;
using Xunit;

namespace FunQL.Core.Tests.Fields;

public static class FieldFunctionTestData
{
    public static TheoryData<string, FieldFunction> AllFunctions() => new()
    {
        {
            "year(list)",
            new Year(
                new FieldPath([new NamedField("list", new Metadata(5))], new Metadata(5)),
                new Metadata(0)
            )
        },
        {
            "month(list)",
            new Month(
                new FieldPath([new NamedField("list", new Metadata(6))], new Metadata(6)),
                new Metadata(0)
            )
        },
        {
            "day(list)",
            new Day(
                new FieldPath([new NamedField("list", new Metadata(4))], new Metadata(4)),
                new Metadata(0)
            )
        },
        {
            "hour(list)",
            new Hour(
                new FieldPath([new NamedField("list", new Metadata(5))], new Metadata(5)),
                new Metadata(0)
            )
        },
        {
            "minute(list)",
            new Minute(
                new FieldPath([new NamedField("list", new Metadata(7))], new Metadata(7)),
                new Metadata(0)
            )
        },
        {
            "second(list)",
            new Second(
                new FieldPath([new NamedField("list", new Metadata(7))], new Metadata(7)),
                new Metadata(0)
            )
        },
        {
            "millisecond(list)",
            new Millisecond(
                new FieldPath([new NamedField("list", new Metadata(12))], new Metadata(12)),
                new Metadata(0)
            )
        },
        {
            "floor(list)",
            new Floor(
                new FieldPath([new NamedField("list", new Metadata(6))], new Metadata(6)),
                new Metadata(0)
            )
        },
        {
            "ceiling(list)",
            new Ceiling(
                new FieldPath([new NamedField("list", new Metadata(8))], new Metadata(8)),
                new Metadata(0)
            )
        },
        {
            "round(list)",
            new Round(
                new FieldPath([new NamedField("list", new Metadata(6))], new Metadata(6)),
                new Metadata(0)
            )
        },
        {
            "lower(list)",
            new Lower(
                new FieldPath([new NamedField("list", new Metadata(6))], new Metadata(6)),
                new Metadata(0)
            )
        },
        {
            "upper(list)",
            new Upper(
                new FieldPath([new NamedField("list", new Metadata(6))], new Metadata(6)),
                new Metadata(0)
            )
        },
        {
            "isNull(list)",
            new IsNull(
                new FieldPath([new NamedField("list", new Metadata(7))], new Metadata(7)),
                new Metadata(0)
            )
        },
    };
}