# Filter

The `filter` parameter specifies one or more predicates to filter the data with. Predicates can be combined using 
logical operators like `#!funql and()` and `#!funql or()` to create more advanced filters.

## Syntax

- `#!funql filter(predicate: BooleanExpression)`
- Comparison `BooleanExpression`:
    - `#!funql eq(field: FieldArgument, value: Constant)`
    - `#!funql ne(field: FieldArgument, value: Constant)`
    - `#!funql gt(field: FieldArgument, value: Constant)`
    - `#!funql gte(field: FieldArgument, value: Constant)`
    - `#!funql lt(field: FieldArgument, value: Constant)`
    - `#!funql lte(field: FieldArgument, value: Constant)`
- Logical `BooleanExpression`:
    - `#!funql and(left: BooleanExpression, right: BooleanExpression)`
    - `#!funql or(left: BooleanExpression, right: BooleanExpression)`
    - `#!funql not(predicate: BooleanExpression)`
- String `BooleanExpression`:
    - `#!funql has(field: FieldArgument, value: Constant)`
    - `#!funql stw(field: FieldArgument, value: Constant)`
    - `#!funql enw(field: FieldArgument, value: Constant)`
    - `#!funql reg(field: FieldArgument, value: Constant)`
- List `BooleanExpression`:
    - `#!funql any(field: FieldPath, predicate: BooleanExpression)`
    - `#!funql all(field: FieldPath, predicate: BooleanExpression)`

Examples:

- `#!funql filter(gte(price, 500))`
- `#!funql filter(and(gte(price, 500), has(upper(name), "STAR")))`

## C# representation

The `filter` parameter is represented by the `Filter` node in C#. For example:

```funql
filter(gte(price, 500))
```

corresponds to:

```csharp
new Filter(
    new GreaterThanOrEqual(
        new FieldPath([new NamedField("price")]), 
        new Constant(500)
    )
);
```

The `Filter` node contains a single predicate expression defining how to filter the results.

## Configuration

Enable the `filter` parameter for a request by calling the `SupportsFilter()` method. Optionally, pass an action to
configure the parameter further, for example, to set the default value:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsFilter(config => 
            {
                // Only include sets worth 10 or more by default
                config.HasDefaultValue(new Filter(
                    new GreaterThanOrEqual(
                        new FieldPath([new NamedField("price")]), 
                        new Constant(10)
                    )
                ));
            });
    }
}
```

Then configure which fields support filtering:

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
                    // Enable filtering by price
                    .SupportsFilter(config => 
                    {
                        // Enable functions like 'eq', 'gt', 'lte', 'floor'
                        config.SupportsDoubleFilterFunctions();
                    });
            });
    }
}
```

!!! note

    Filtering must be explicitly enabled per field. If `filter` is not provided when executing a request and no default 
    is set, no filtering is applied.

## Execution

When executing a query with the `filter` parameter, the `Filter` node is first validated against the following rules:

- Filter must be supported for the request.
- Filter must be supported for the target fields.
- Filter `Constant` values must be valid for the target fields.

If valid, the `Filter` node can be applied to the result set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
    new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
    new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
    new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
};

// 'filter(and(gte(price, 500), has(upper, "STAR")))' node (e.g., from parsing)
var filter = new Filter(
    new And(
        new GreaterThanOrEqual(
            new FieldPath([new NamedField("price")]), 
            new Constant(500)
        ),
        new Has(
            new Upper(new FieldPath([new NamedField("name")])),
            new Constant("STAR")
        )
    )
);

// Apply filter to data set
var results = sets.AsQueryable()
    // Equivalent LINQ:
    // sets.Where(it => it.Price >= 500)
    //     .Where(it => it.Name.ToUpper().Contains("STAR"))
    .ApplyFilter(schema.SchemaConfig, "listSets", filter)
    .ToList();

// Print the result
Console.WriteLine(string.Join("\n", results));
// Output:
// Set { Name = LEGO Star Wars Millennium Falcon, Price = 849.99, LaunchTime = 1-10-2017 00:00:00 }
// Set { Name = LEGO Star Wars The Razor Crest, Price = 599.99, LaunchTime = 3-10-2022 00:00:00 }
```