# Limit

The `limit` parameter restricts the maximum number of items returned in a single response. It is commonly used for
pagination in combination with the [`skip`](skip.md) parameter.

## Syntax

- `#!funql limit(value: Integer)`

Examples:

- `#!funql limit(10)`
- `#!funql limit(100)`

## C# representation

The `limit` parameter is represented by the `Limit` node in C#. For example:

```funql
limit(10)
```

corresponds to:

```csharp
new Limit(new Constant(10));
```

The `Limit` node has a single child, a `Constant` node containing the maximum number of items to return.

## Configuration

Enable the `limit` parameter for a request by calling the `SupportsLimit()` method. Optionally, pass an action to
configure the parameter further, for example, to set the default and maximum values:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsLimit(config => 
            {                
                // Return at most 20 items by default
                config.HasDefaultValue(new Limit(new Constant(20)))
                    // Reject values greater than 100
                    .HasMaxLimit(100);
            });
    }
}
```

!!! note

    If `limit` is not provided when executing a request and no default is set, the effective behavior is to return all 
    remaining items (depending on the data source used, like EF Core).

## Execution

When executing a query with the `limit` parameter, the `Limit` node is first validated against the following rules:

- Value must be an integer.
- Value must be greater than or equal to 0.
- Value must be less than or equal to the maximum limit value.

If valid, the `Limit` node can be applied to the result set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// 'limit(1)' node (e.g., from parsing)
var limit = new Limit(new Constant(1));

// Apply limit to data set
var results = sets.AsQueryable()
    // Equivalent LINQ:
    // .Take(1)
    .ApplyLimit(limit)
    .ToList();

// Print the result
Console.WriteLine(string.Join("\n", results));
// Output:
// Set { Name = LEGO Star Wars Millennium Falcon, Price = 849.99, LaunchTime = 1-10-2017 00:00:00 }
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