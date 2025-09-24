# Sort

The `sort` parameter defines the ordering of results. Multiple sort expressions can be combined by separating them with
a comma (`,`).

## Syntax

- `#!funql sort(expressions: SortExpression[])`
- `SortExpression`:
    - `#!funql asc(field: FieldArgument)`
    - `#!funql desc(field: FieldArgument)`

Examples:

- `#!funql sort(asc(name))`
- `#!funql sort(desc(price), asc(lower(name)))`

## C# representation

The `sort` parameter is represented by the `Sort` node in C#. For example:

```funql
sort(desc(price))
```

corresponds to:

```csharp
new Sort([
    new Descending(new FieldPath([new NamedField("price")]))
]);
```

The `Sort` node has one or more `SortExpression` children. Each `SortExpression` is either an `Ascending` or 
`Descending` node, defining the field to sort by.

## Configuration

Enable the `sort` parameter for a request by calling the `SupportsSort()` method. Optionally, pass an action to 
configure the parameter further, for example, to set the default value:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsSort(config => 
            {
                // Sort by name by default
                config.HasDefaultValue(new Sort([
                    new Ascending(new FieldPath([new NamedField("name")]))
                ]));
            });
    }
}
```

Then configure which fields support sorting:

```csharp 
public sealed record Set(string Name, double Price, DateTime LaunchTime);

public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsSort(config => { .. })
            .ReturnsListOfObjects<Set>(set =>
            {
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    // Enable sorting by price
                    .SupportsSort(config => 
                    {
                        // Enable field functions like 'floor()' and 'ceiling()' 
                        config.SupportsDoubleFieldFunctions();
                    });
            });
    }
}
```

!!! note

    Sorting must be explicitly enabled per field. If `sort` is not provided when executing a request and no default 
    is set, the effective ordering depends on the underlying data source. Prefer using a stable default sort (e.g., by 
    `id` or `createTime`).

## Execution

When executing a query with the `sort` parameter, the `Sort` node is first validated against the following rules:

- Sort must be supported for request.
- Sort must be supported for the target field.

If valid, the `Sort` node can be applied to the result set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// 'sort(desc(price))' node (e.g., from parsing)
var sort = new Sort([
    new Descending(new FieldPath([new NamedField("price")])),
    new Ascending(new FieldPath([new NamedField("name")]))
]);

// Apply sort to data set
var results = sets.AsQueryable()
    // Equivalent LINQ:
    // sets.OrderByDescending(it => it.Price)
    //     .ThenBy(it => it.Name)
    .ApplySort(schema.SchemaConfig, "listSets", sort, alreadyOrdered: false)
    .ToList();

// Print the result
Console.WriteLine(string.Join("\n", results));
// Output:
// Set { Name = LEGO Star Wars Millennium Falcon, Price = 849.99, LaunchTime = 1-10-2017 00:00:00 }
// Set { Name = LEGO Star Wars The Razor Crest, Price = 599.99, LaunchTime = 3-10-2022 00:00:00 }
// Set { Name = LEGO Harry Potter Hogwarts Castle, Price = 469.99, LaunchTime = 1-9-2018 00:00:00 }
// Set { Name = LEGO DC Batman Batmobile Tumbler, Price = 269.99, LaunchTime = 1-11-2021 00:00:00 }
```