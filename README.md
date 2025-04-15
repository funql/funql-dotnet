![FunQL logo](https://raw.githubusercontent.com/funql/funql-dotnet/main/assets/logo.png)

# FunQL .NET - API Query Library

[![License: GPLv2/Commercial](https://img.shields.io/badge/license-GPLv2%20or%20Commercial-orange.svg)](https://github.com/funql/funql-dotnet/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/FunQL.Core)](https://www.nuget.org/packages/FunQL)
[![Latest version](https://img.shields.io/nuget/v/FunQL.Core)](https://www.nuget.org/packages/FunQL)
[![Build status](https://github.com/funql/funql-dotnet/workflows/build/badge.svg)](https://github.com/funql/funql-dotnet/actions/workflows/build.yml)

FunQL .NET lets you easily expose filtering, sorting, and pagination in your API — using a clean, functional query
language that works out of the box.

It is the official .NET implementation of [FunQL](https://funql.io/), the open-source Functional Query Language. Use
FunQL .NET to enhance your existing REST API with ready-to-use components for filtering, sorting, pagination, and more
— or build a new FunQL API from scratch.

FunQL .NET integrates seamlessly with LINQ and Entity Framework Core (EF Core), allowing you to translate FunQL queries
directly into efficient database expressions. It is non-invasive and drop-in ready: you can plug it into your existing
endpoints without rewriting your API logic.

To learn more about FunQL, visit [funql.io](https://funql.io/).

## Packages

| Package      | Latest version                                                                                   | About                                                                                   |
|--------------|--------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| `FunQL`      | [![NuGet](https://img.shields.io/nuget/v/FunQL)](https://www.nuget.org/packages/FunQL)           | Bundles `FunQL.Core` and `FunQL.Linq` — just one install to get started.                |
| `FunQL.Core` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Core)](https://www.nuget.org/packages/FunQL.Core) | Provides the core functionality, including parsing, validating, and executing requests. |
| `FunQL.Linq` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Linq)](https://www.nuget.org/packages/FunQL.Linq) | Translates FunQL queries into LINQ expressions.                                         |

## Documentation

This README provides a quick overview of FunQL .NET and its capabilities. For detailed documentation, check out
[dotnet.funql.io](https://dotnet.funql.io/) and [funql.io](https://funql.io/). For hands-on experience, explore the
[samples](https://github.com/funql/funql-dotnet/tree/main/samples) directory.

## Quick start

To use FunQL .NET, you need two things: a queryable collection of data and a FunQL schema. The schema serves as the main
entry point for handling FunQL requests. It defines the configuration for fields, available functions like filtering and
sorting, and features such as LINQ support.

To get started, first add the [FunQL](https://www.nuget.org/packages/FunQL) package to your project by running the
following command:

```shell
dotnet add package FunQL
```

Next, define your data model and configure a FunQL schema that describes the structure of your data and which fields can
be filtered and sorted:

```csharp
// Data model representing the objects you want to query
public sealed record Set(string Name, double Price, DateTime LaunchTime);

// FunQL schema configuration for the 'listSets()' request
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // Add core parsing, validation, and execution features
        schema.AddCoreFeatures();
        
        // Add LINQ feature for translating FunQL queries into LINQ expressions
        schema.AddLinqFeature();
        
        // Define the listSets() request, enable filter and sort, and configure its available fields
        schema.Request("listSets")
            .SupportsFilter()
            .SupportsSort()
            .ReturnsListOfObjects<Set>(set =>
            {
                // Configure the Name field to support String filter and sort functions (like eq, gt, has, lower)
                set.SimpleField(it => it.Name)
                    .HasName("name")
                    .SupportsFilter(it => it.SupportsStringFilterFunctions())
                    .SupportsSort(it => it.SupportsStringFieldFunctions());
                
                // Configure the Price field to support Double filter and sort functions (like eq, gt, floor)
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    .SupportsFilter(it => it.SupportsDoubleFilterFunctions())
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());
                
                // Configure the LaunchTime field to support DateTime filter and sort functions (like eq, gt, year)
                set.SimpleField(it => it.LaunchTime)
                    .HasName("launchTime")
                    .SupportsFilter(it => it.SupportsDateTimeFilterFunctions())
                    .SupportsSort(it => it.SupportsDateTimeFieldFunctions());
            });
    }
}
```

Then, prepare a collection of data that you want to query and define the parameters of the request. In this example, we
define a list of LEGO sets:

```csharp
// Prepare the data source
// In real-world scenarios this would e.g. be an EF Core DbSet<Set> so FunQL can directly query the database
IQueryable<Set> sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, DateTime.Parse("2017-10-01")),
    new("LEGO Star Wars The Razor Crest", 599.99, DateTime.Parse("2022-10-03")),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, DateTime.Parse("2021-11-01")),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, DateTime.Parse("2018-09-01")),
}.AsQueryable();

// Define the FunQL filter and sort parameters
// This filter selects Star Wars sets with a price >= 500 and launch year after 2010
const string filter = "and(has(upper(name), \"STAR WARS\"), gte(price, 500), gt(year(launchTime), 2010))";

// Sort results by price in descending order
const string sort = "desc(price)";
```

Finally, create the FunQL schema and execute a filter and sort request using FunQL .NET:

```csharp
// Create the FunQL schema that defines the available requests and fields
var schema = new ApiSchema();

// Execute the FunQL request using the schema and parameters
var result = await sets
    .ExecuteRequestForParameters(schema, requestName: "listSets", filter: filter, sort: sort);

// Print the filtered and sorted result as JSON
var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(result.Data, jsonSerializerOptions));
```

In this case, the following sets are included in the result:

```json
[
  {
    "name": "LEGO Star Wars Millennium Falcon",
    "price": 849.99,
    "launchTime": "2017-10-01T00:00:00"
  },
  {
    "name": "LEGO Star Wars The Razor Crest",
    "price": 599.99,
    "launchTime": "2022-10-03T00:00:00"
  }
]
```

For more detailed examples, check the [samples](https://github.com/funql/funql-dotnet/tree/main/samples) directory.

## Features

- Drop-in ready — Add FunQL .NET to your existing endpoints without rewriting your API logic.
- It just works — FunQL .NET processes raw text into functional queries, handling lexical analysis, tokenization, syntax
  parsing, and Abstract Syntax Tree (AST) generation so you don't have to.
- Supports LINQ and EF Core — Translates FunQL queries into LINQ expressions for seamless database interaction with EF
  Core.
- Zero dependencies — Requires only .NET Core; no additional frameworks or external libraries needed.
- Built-in validation — Enforces query parameter constraints, ensuring valid API queries.
- Explicit by default — No hidden behaviors or 'automagic' configurations. FunQL .NET enforces explicit schema
  definitions, giving developers full control and clarity over API behavior.
- Highly extensible — Every component is customizable: override, extend, or replace parts to fit your project's needs.
- Configurable JSON serialization — Works out of the box with `System.Text.Json` and supports external JSON libraries
  like `NewtonSoft.Json` for greater flexibility.

## Samples

The [samples](https://github.com/funql/funql-dotnet/tree/main/samples) directory contains example projects that
demonstrate how to use FunQL .NET in different scenarios.

- [`Basic`](https://github.com/funql/funql-dotnet/tree/main/samples/Basic) - A minimal, self-contained console
  application that demonstrates how to configure a FunQL schema, use LINQ to apply queries, and filter and sort data
  using FunQL query parameters.
- [`WebApi`](https://github.com/funql/funql-dotnet/tree/main/samples/WebApi) - A REST API example that integrates FunQL
  .NET with Entity Framework Core (EF Core), Noda Time, System.Text.Json, and minimal APIs.

## License

- FunQL software is dual-licensed:
    - Under the [GNU GPLv2](https://github.com/funql/funql-dotnet/blob/main/LICENSE-GPL) for open-source use.
    - Under a [commercial license](https://funql.io/code/licensing/) for proprietary, closed-source use.
- FunQL [documentation](https://github.com/funql/funql-dotnet/tree/main/docs) is licensed under [CC BY-SA 4.0](
  https://github.com/funql/funql-dotnet/blob/main/docs/LICENSE).
    - **Note:** Source code examples in the documentation are covered by the same license as FunQL software.
- For full licensing details, refer to:
    - [LICENSE](https://github.com/funql/funql-dotnet/blob/main/LICENSE)
    - [COMPLIANCE](https://github.com/funql/funql-dotnet/blob/main/COMPLIANCE.md)