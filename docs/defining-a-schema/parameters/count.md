# Count

The `count` parameter requests the total number of items matching the current query (after filters, before paging). It's
commonly used alongside pagination to display the total available results.

## Syntax

- `#!funql count(value: Boolean)`

Examples:

- `#!funql count(true)`
- `#!funql count(false)`

## C# representation

The `count` function is represented by the `Count` node in C#. For example:

```funql 
count(true)
```

corresponds to:

```csharp 
new Count(new Constant(true));
```

The `Count` node has a single child, a `Constant` node containing a boolean that enables or disables counting.

## Configuration

Enable the `count` parameter for a request by calling the `SupportsCount()` method. Optionally, pass an action to 
configure the parameter further, for example, to set the default value:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsCount(config => 
            {
                // Return total count by default
                config.HasDefaultValue(new Count(new Constant(true))); 
            });
    }
}
```

!!! note

    If `count` isn't provided and no default is set, it's treated as `false`.

## Execution

When executing a query with the `count` parameter, the `Count` node is first validated against the following rules:

- Value must be a boolean.

If valid, the `Count` node can be applied to the result set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// 'count(true)' node (e.g., from parsing)
var count = new Count(new Constant(true));

// Only count if 'count' is true
if (count.Value() == true) 
{
    var totalCount = sets.Count();
    
    Console.WriteLine(totalCount);
    // Output:
    // 4
}
```

!!! note

    When combined with parameters such as `filter`, the `count` reflects the number of results after filtering. Make 
    sure to count the total number of results **before** applying `skip` and `limit` parameters, otherwise the counnt 
    will not reflect the total number of results.

    If you use the [execute feature](../features/execute.md), this is handled for you automatically.

## Pagination

When using pagination (with `skip` and `limit`), use `count(true)` to request the total matches:

=== "REST"
    
    ```urlencoded
    GET http://localhost:5000/sets
      ?skip=10
      &limit=10
      &count=true
    ```

    <div class="result" markdown>
    
    ```json hl_lines="3"
    HTTP/1.1 200 OK
    Content-Type: application/json
    Total-Count: 70

    [
      ..
    ]
    ```

    !!! note

        In REST responses, the total count is included in the `Total-Count` header.

    </div>

=== "QL"

    ```funql
    listSets(
      skip(10),
      limit(10),
      count(true)
    )
    ```

    <div class="result" markdown>

    ```json hl_lines="9"
    HTTP/1.1 200 OK
    Content-Type: application/json
    
    {
      "data": [
        ..
      ],
      "metadata": {
        "totalCount": 70
      }
    }
    ```