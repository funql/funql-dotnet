// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Extensions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Counting.Configs.Builders.Extensions;
using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Filtering.Configs.Builders.Extensions;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Extensions;
using FunQL.Core.Limiting.Configs.Builders.Extensions;
using FunQL.Core.Schemas;
using FunQL.Core.Skipping.Configs.Builders.Extensions;
using FunQL.Core.Sorting.Configs.Builders.Extensions;
using FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Extensions;
using WebApi.Models;

namespace WebApi.FunQL;

/// <summary>Configurator that configures the <see cref="Set"/> FunQL requests.</summary>
public class SetSchemaConfigurator : ISchemaConfigurator
{
    /// <summary>Name of the request to list all <see cref="Set"/>.</summary>
    public const string ListRequestName = "listSets";
    
    /// <inheritdoc/>
    public void Configure(ISchemaConfigBuilder schema)
    {
        // Configure the 'listSets' request
        schema.Request(ListRequestName)
            // Enable support for filtering
            .SupportsFilter()
            // Enable support for sorting
            .SupportsSort()
            // Enable support for skipping
            .SupportsSkip()
            // Enable support for limiting
            .SupportsLimit()
            // Enable support for counting
            .SupportsCount()
            // Configure the return type (List<Set>), used by FunQL to parse and validate queries
            .ReturnsListOfObjects<Set>(set =>
            {
                // Configure each property of Set
                set.SimpleField(it => it.Id)
                    // By default, the C# property name ('Id') is used, so override it to use camelCase naming
                    .HasName("id")
                    // Enable equality filter support (eq, neq) for this field
                    .SupportsFilter(it => it.SupportsEqualityFilterFunctions())
                    // Enable sort support for this field
                    .SupportsSort();
                set.SimpleField(it => it.Name)
                    .HasName("name")
                    // Enable string filter support (eq, neq, gt, gte, lt, lte, has, stw, enw, reg) and string field
                    // function support (lower, upper) for this field
                    .SupportsFilter(it => it.SupportsStringFilterFunctions())
                    // Enable sort support with string field function support (lower, upper) for this field
                    .SupportsSort(it => it.SupportsStringFieldFunctions());
                set.SimpleField(it => it.SetNumber)
                    .HasName("setNumber")
                    // Enable equality filter support (eq, neq) for this field
                    .SupportsFilter(it => it.SupportsEqualityFilterFunctions())
                    .SupportsSort();
                set.SimpleField(it => it.Pieces)
                    .HasName("pieces")
                    // Enable comparison filter support (eq, neq, gt, gte, lt, lte) for this field
                    .SupportsFilter(it => it.SupportsComparisonFilterFunctions())
                    .SupportsSort();
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    // Enable double filter support (eq, neq, gt, gte, lt, lte) and double field function support
                    // (floor, ceil, round) for this field
                    .SupportsFilter(it => it.SupportsDoubleFilterFunctions())
                    // Enable sort support with double field function support (floor, ceil, round) for this field
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());
                set.SimpleField(it => it.LaunchTime)
                    .HasName("launchTime")
                    // Enable DateTime filter support (eq, neq, gt, gte, lt, lte) and DateTime field function support
                    // (year, month, day, hour, minute, second, millisecond) for this field
                    .SupportsFilter(it => it.SupportsDateTimeFilterFunctions())
                    // Enable sort support with DateTime field function support (year, month, day, hour, minute, second,
                    // millisecond) for this field
                    .SupportsSort(it => it.SupportsDateTimeFieldFunctions());
            });
    }
}