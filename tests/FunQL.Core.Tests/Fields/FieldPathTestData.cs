// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;
using Xunit;

namespace FunQL.Core.Tests.Fields;

public static class FieldPathTestData
{
    public static TheoryData<string, FieldPath, string> NamedFieldPaths() => new()
    {
        {
            "object",
            new FieldPath([
                new NamedField("object", new Metadata(0))
            ], new Metadata(0)),
            "object"
        },
        {
            "object.nested",
            new FieldPath([
                new NamedField("object", new Metadata(0)),
                new NamedField("nested", new Metadata(7))
            ], new Metadata(0)),
            "object.nested"
        },
        {
            "object.nested.field",
            new FieldPath([
                new NamedField("object", new Metadata(0)),
                new NamedField("nested", new Metadata(7)),
                new NamedField("field", new Metadata(14))
            ], new Metadata(0)),
            "object.nested.field"
        },
    };

    public static TheoryData<string, FieldPath, string> MapFieldPaths() => new()
    {
        {
            """object["key"]""",
            new FieldPath([
                new NamedField("object", new Metadata(0)),
                new NamedField("key", new Metadata(6))
            ], new Metadata(0)),
            // Path should be simplified to dot-notation
            "object.key"
        },
        {
            """object.nested["key"]""",
            new FieldPath([
                new NamedField("object", new Metadata(0)),
                new NamedField("nested", new Metadata(7)),
                new NamedField("key", new Metadata(13)),
            ], new Metadata(0)),
            // Path should be simplified to dot-notation
            "object.nested.key"
        },
        {
            """object.nested["\"quoted\"Key\""]""",
            new FieldPath([
                new NamedField("object", new Metadata(0)),
                new NamedField("nested", new Metadata(7)),
                new NamedField("\"quoted\"Key\"", new Metadata(13))
            ], new Metadata(0)),
            // Path requires bracket-notation
            """object.nested["\"quoted\"Key\""]"""
        },
    };
}