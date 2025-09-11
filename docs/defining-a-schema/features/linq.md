# LINQ

The LINQ feature translates FunQL requests into LINQ expressions so they can be applied to `IQueryable<T>` sources. This
enables seamless integration with in-memory collections and LINQ-based frameworks such as Entity Framework Core.

This page explains how to enable the LINQ feature, how to apply translations, and how to customize it.

## Adding the feature

Use `AddLinqFeature()` to register services that can translate FunQL nodes (filter, sort, skip, limit) into LINQ
expressions:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        // Adds translators for filter/sort/skip/limit 
        schema.AddLinqFeature(); 
    } 
}
```

This sets up the LINQ feature with default configurations. See [Advanced configuration](#advanced-configuration) on how 
to customize the feature.

!!! tip

    If you already added the [Execute feature](execute.md), adding the LINQ feature will automatically register the LINQ 
    execution handlers by default.

## Applying translations

Once the LINQ feature is added, you can apply FunQL parameters directly to an `IQueryable<T>`:

- **`IQueryable.ApplyFilter()`**: Applies a FunQL `Filter`.
- **`IQueryable.ApplySort()`**: Applies a FunQL `Sort`.
- **`IQueryable.ApplySkip()`**: Applies a FunQL `Skip`.
- **`IQueryable.ApplyLimit()`**: Applies a FunQL `Limit`.
- **`IQueryable.ApplyRequest()`**: Applies the FunQL `Filter`, `Sort`, `Skip`, and `Limit` for given `Request`.

!!! tip

    It's recommended to use the [Execute feature](execute.md) instead of manually applying FunQL parameters. See 
    [Executing requests](#executing-requests) for more information.

The easiest way to apply FunQL parameters to an `IQueryable<T>` is to use `ApplyRequest()`:

```csharp
// Define the data model
public sealed record Set(string Name, double Price, DateTime LaunchTime);

// Prepare in-memory data
var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// Create the configured schema
var schema = new ApiSchema();
// Parse a FunQL request to apply to the sets
var request = schema.ParseRequest(@"listSets(
    filter(gte(price, 500)),
    sort(desc(price)), 
    skip(1),
    limit(5)
)");

// Apply FunQL request to the IQueryable<T>
var results = sets.ApplyRequest(schema, request)
    .ToList();

// Print the result
Console.WriteLine(string.Join(", ", results));
// Output:
// Set { Name = LEGO Star Wars The Razor Crest, Price = 599,99, LaunchTime = 3-10-2022 00:00:00 }
```

You may also apply each parameter individually:

```csharp
// Define the data model
public sealed record Set(string Name, double Price, DateTime LaunchTime);

// Prepare in-memory data
var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// FunQL parameters to apply
// Example filter: filter(gte(price, 500))
var filter = new Filter(
    new GreaterThanOrEqual( 
        new FieldPath([new NamedField("price")]), 
        new Constant(500.00) 
    )
);
// Example sort: sort(desc(price))
var sort = new Sort([
    new Descending(new FieldPath([new NamedField("price")]))
]);
// Example skip: skip(1)
var skip = new Skip(new Constant(1));
// Example limit: limit(5)
var limit = new Limit(new Constant(5));

// Create the configured schema
var schema = new ApiSchema();

// Apply parameters
var results = sets.ApplyFilter(schema.SchemaConfig, "listSets", filter) 
    // Setting alreadyOrdered to false as the sets are not ordered yet; When 
    // manually calling `.OrderBy()`, you need to set alreadyOrdered to true, 
    // so FunQL knows to use `.ThenBy()` instead of `.OrderBy()` 
    .ApplySort(schema.SchemaConfig, "listSets", sort, alreadyOrdered: false)
    .ApplySkip(skip)
    .ApplyLimit(limit)
    .ToList();

// Print the result
Console.WriteLine(string.Join(", ", results));
// Output:    
// Set { Name = LEGO Star Wars The Razor Crest, Price = 599,99, LaunchTime = 3-10-2022 00:00:00 }
```

## Executing requests

While you can apply FunQL parameters directly to an `IQueryable<T>`, it's recommended to use the execution pipeline
instead. The execution pipeline will assure the FunQL parameters are applied in the correct order and the total count 
(`count(true)`) is calculated before applying `skip` and `limit` parameters.

- [Learn how to execute requests →](../../executing-queries/index.md)

## Advanced configuration

The `AddLinqFeature()` method has two optional arguments:

- `action`: An action to customize the feature.
- `withLinqExecutionHandlers`: Whether to add all LINQ execution handlers. Default `true`.

For example, add a custom field function LINQ translator (e.g., to support custom types like NodaTime's `Instant`):

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.AddLinqFeature(config => { 
            // Customize the feature here            
            config.MutableConfig
                .AddFieldFunctionLinqTranslator(new MyCustomLinqTranslator());
        });
    }
}
```

!!! tip

    Using NodaTime in your application and want to use field functions like `year()` and `month()`? Check out the 
    NodaTime integration on how to support this:

    [Learn how to integrate FunQL with NodaTime →](../../integrations/nodatime.md).

## What's next

After adding LINQ, it's time to execute queries on your database:

- [Learn more about executing queries →](../../executing-queries/index.md)
- [Learn more about integrating Entity Framework Core →](../../integrations/efcore.md)