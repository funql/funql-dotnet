# Getting started

This guide will help you get started with FunQL .NET quickly.

## Installation

### 1. Create a new Web API project

Create a new ASP.NET Core Web API project using the .NET CLI.

```shell title="Bash"
dotnet new webapi -n Demo
```

This will create a new directory called `Demo` containing your project's files.

You can now open the `Demo` directory or the `Demo.csproj` file in your favourite code editor.

### 2. Add the FunQL package

Add the [FunQL ![NuGet](https://img.shields.io/nuget/v/FunQL)](https://www.nuget.org/packages/FunQL) package to your 
project using the .NET CLI:

```shell
dotnet add package FunQL
```

!!! note

    This will install both [FunQL.Core](https://www.nuget.org/packages/FunQL.Core) and [FunQL.Linq](
    https://www.nuget.org/packages/FunQL.Linq), providing all the essential components to get you started quickly.

## Create the data model

For this example we'll create an API for querying LEGO sets, so create a `Set` data model.

```csharp title="Set.cs"
public sealed record Set(string Name, double Price, DateTime LaunchTime);
```

## Create the schema

TODO: Explain about schema

```csharp
public sealed class DemoSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // ===== Features =====
        // Add all core features: Parse, Print, Validate, Visit and Execute
        schema.AddCoreFeatures();
        // Add LINQ feature so FunQL can translate FunQL queries to LINQ expressions, e.g. for use with EFCore DbSet
        schema.AddLinqFeature();
        
        // ===== Requests =====
        // Add the 'listSets()' request
        schema.Request("listSets")
            // Enable support for the 'filter()' parameter
            .SupportsFilter()
            // Enable support for the 'sort()' parameter
            .SupportsSort()
            // Configure the 'listSets()' return type: 'List<Set>'
            .ReturnsListOfObjects<Set>(set =>
            {
                // Configure each field of the data model
                set.SimpleField(it => it.Name)
                    .HasName("name")
                    .SupportsFilter(it => it.SupportsStringFilterFunctions())
                    .SupportsSort(it => it.SupportsStringFieldFunctions());
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    .SupportsFilter(it => it.SupportsDoubleFilterFunctions())
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());
                set.SimpleField(it => it.LaunchTime)
                    .HasName("launchTime")
                    .SupportsFilter(it => it.SupportsDateTimeFilterFunctions())
                    .SupportsSort(it => it.SupportsDateTimeFieldFunctions());
            });
    }
}
```

## Add FunQL services

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DemoSchema>();
```

## Create a REST endpoint with FunQL support

```csharp
app.MapGet("/sets", async (string filter, string sort, DemoSchema schema) =>
{
    // We use demo data for this example — Normally you would e.g. use an Entity Framework Core DbContext to query the 
    // database directly
    IQueryable<Set> sets = new List<Set>
    {
        new("LEGO Star Wars Millennium Falcon", 849.99, DateTime.Parse("2017-10-01", styles: DateTimeStyles.AdjustToUniversal)),
        new("LEGO Star Wars The Razor Crest", 599.99, DateTime.Parse("2022-10-03", styles: DateTimeStyles.AdjustToUniversal)),
        new("LEGO DC Batman Batmobile Tumbler", 269.99, DateTime.Parse("2021-11-01", styles: DateTimeStyles.AdjustToUniversal)),
        new("LEGO Harry Potter Hogwarts Castle", 469.99, DateTime.Parse("2018-09-01", styles: DateTimeStyles.AdjustToUniversal)),
    }.AsQueryable();
    
    var result = await sets
        .ExecuteRequestForParameters(
            schema, 
            requestName: "listSets", 
            filter: filter, 
            sort: sort
        );
    
    return result.Data;
});
```

And that is it, you have now successfully added FunQL support to your API! 🚀

## Execute a query

Run the project using the .NET CLI:

```shell
dotnet run
```

If everything is set up correctly, you should be able to open [http://localhost:5000/sets](http://localhost:5000/sets) 
to query the LEGO sets. Looking for LEGO Star Wars sets that cost at least €500 and were launched after 2010, sorted by
price? Just ask:

```html
http://localhost:5000/sets
    ?filter=and(has(upper(name), "STAR WARS"), gte(price, 500), gt(year(launchTime), 2010))
    &sort=desc(price)
```

