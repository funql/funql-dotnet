# Skip

The `skip` parameter specifies how many items to skip from the result set before returning data. It is commonly used for
pagination in combination with the [`limit`](limit.md) parameter.

## Syntax

- `#!funql skip(value: Integer)`

Examples:

- `#!funql skip(10)`
- `#!funql skip(20)`

## C# representation

The `skip` parameter is represented by the `Skip` node in C#. For example:

```funql
skip(20)
```

corresponds to:

```csharp
new Skip(new Constant(20));
```

The `Skip` node has a single child, a `Constant` node containing the number of items to skip.

## Configuration

Enable the `skip` parameter for a request by calling the `SupportsSkip()` method. Optionally, pass an action to
configure the parameter further, for example, to set the default value:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsSkip(config => 
            {
                // Skip first 20 items by default
                config.HasDefaultValue(new Skip(new Constant(20)));
            });
    }
}
```

!!! note

    If `skip` is not provided when executing a request and no default is set, the effective value is `0`.

## Execution

When executing a query with the `skip` parameter, the `Skip` node is first validated against the following rules:

- Value must be an integer.
- Value must be greater than or equal to 0.

If valid, the `Skip` node can be applied to the result set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// 'skip(3)' node (e.g., from parsing)
var skip = new Skip(new Constant(3));

// Apply skip to data set
var results = sets.AsQueryable()
    // Equivalent LINQ:
    // .Skip(3)
    .ApplySkip(skip)
    .ToList();

// Print the result
Console.WriteLine(string.Join(", ", results));
// Output:
// Set { Name = LEGO Harry Potter Hogwarts Castle, Price = 469.99, LaunchTime = 1-9-2018 00:00:00 }
```

## Pagination

Combine the `skip` and `limit` parameters to implement page/offset navigation. For example, to get the second page of
results with 10 items per page, you can use the following query:

=== "REST"

    ```funql
    GET http://localhost:5000/sets
      ?skip=10
      &limit=10
    ```

=== "QL"

    ```funql
    listSets(
      skip(10),
      limit(10)
    )
    ```

!!! tip

    Use a stable [sort](sort.md) with paging to avoid item drift between pages (e.g., sort by `createTime`).