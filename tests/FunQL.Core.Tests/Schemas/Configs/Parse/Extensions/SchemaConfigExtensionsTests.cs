// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Counting.Configs.Builders.Extensions;
using FunQL.Core.Filtering.Configs.Builders.Extensions;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Inputting.Configs.Builders.Extensions;
using FunQL.Core.Limiting.Configs.Builders.Extensions;
using FunQL.Core.Schemas;
using FunQL.Core.Schemas.Configs.Core.Extensions;
using FunQL.Core.Schemas.Configs.Parse.Extensions;
using FunQL.Core.Skipping.Configs.Builders.Extensions;
using FunQL.Core.Sorting.Configs.Builders.Extensions;
using FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Tests.TestData.Models;
using Xunit;

namespace FunQL.Core.Tests.Schemas.Configs.Parse.Extensions;

public class SchemaConfigExtensionsTests
{
    // Create a Schema to build the SchemaConfig used for testing
    private class TestSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder builder)
        {
            builder.AddCoreFeatures();

            builder.Request("test")
                .SupportsFilter()
                .SupportsSort()
                .SupportsSkip()
                .SupportsLimit()
                .SupportsCount()
                .SupportsInput<Designer>()
                .ReturnsListOfObjects<Designer>(designer =>
                {
                    designer.SimpleField(it => it.Id)
                        .HasName("id")
                        .SupportsFilter(it => it.SupportsEqualityFilterFunctions())
                        .SupportsSort();
                    designer.SimpleField(it => it.Name)
                        .HasName("name")
                        .SupportsFilter(it => it.SupportsStringFilterFunctions())
                        .SupportsSort();
                });
        }
    }
    
    [Theory]
    [InlineData("{  }invalid")]
    [InlineData(null, "eq(name, \"test\")invalid")]
    [InlineData(null, null, "asc(id)invalid")]
    [InlineData(null, null, null, "10invalid")]
    [InlineData(null, null, null, null, "10invalid")]
    [InlineData(null, null, null, null, null, "true,invalid")]
    public void ParseRequestForParameters_ShouldThrowSyntaxException_WhenParameterTextContainsMoreTokens(
        string? input = null,
        string? filter = null,
        string? sort = null,
        string? skip = null,
        string? limit = null,
        string? count = null
    )
    {
        /* Arrange */
        var config = new TestSchema().SchemaConfig;

        /* Act */
        var action = () => config.ParseRequestForParameters(
            "test",
            input,
            filter,
            sort,
            skip,
            limit,
            count
        );

        /* Assert */
        Assert.Throws<SyntaxException>(action);
    }
}