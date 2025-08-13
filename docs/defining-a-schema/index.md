# Defining a Schema

A `Schema` in FunQL .NET serves as the core configuration and entry point for processing FunQL queries. It defines what 
data can be queried and which query options are supported (filtering, sorting, pagination). 

This guide walks you through building a schema and configuring it to handle FunQL requests.

## Implementing the Schema

To get started, create a class that inherits from FunQL's `Schema` and override the `OnInitializeSchema()` method. This 
method is where you configure your schema, including supported features and requests:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // Schema configuration goes here
    }
}
```

!!! note

    While it's usually enough to implement `OnInitializeSchema(ISchemaConfigBuilder)`, you may also implement 
    `OnFinalizeSchema(ISchemaConfigBuilder)`, which can be used for additional customizations, like modifying default 
    settings or applying conventions.

FunQL schemas prioritize explicit configurations, providing developers with complete control and predictability. There 
are no hidden behaviors or 'automagic' configurations — every feature and setting must be explicitly defined.

## Adding features

Features define the capabilities of your schema, from parsing and validating FunQL queries to translating them into 
LINQ expressions for execution.

To get started quickly, add the most commonly used features:

- **Core features**: Enables common operations like parsing, query validation, and execution.
- **LINQ feature**: Translates FunQL queries into LINQ expressions, which is useful for querying in-memory collections 
  or external databases (e.g., using Entity Framework (EF) Core).

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        // Add core parsing, validation and execution features
        schema.AddCoreFeatures();
        // Enable LINQ query translation
        schema.AddLinqFeature();
    }
}
```

By adding only the features you need, you ensure that your schema remains lightweight and optimized for your use case.

[Learn more about features →](features/index.md)

## Adding requests

In FunQL, requests define the entry points to your data. They represent the operations that users can query, specifying 
what data can be fetched and how it can be filtered or sorted.

### Key concepts of requests

1. **Request name**: Each request has a unique identifier, like `listSets`, used in queries to specify the operation to 
   execute.
2. **Parameters**: Requests specify which parameters they support, like the `filter()` and `sort()` parameters to 
   provide advanced querying capabilities.
3. **Return type**: Requests define the type of data returned, such as a list of objects, along with the fields that can 
   be queried, filtered, and sorted.

### Example request

As an example, we'll configure a `listSets()` request with support for filtering and sorting a list of LEGO sets. Once 
configured, the schema is ready to handle advanced FunQL queries like:

=== "REST"

    ```funql
    GET http://localhost:5000/sets
      ?filter=and(
        has(upper(name), "STAR WARS"),
        gte(price, 500),
        gt(year(launchTime), 2010)
      )
      &sort=desc(price)
    ```

=== "QL"

    ```funql
    listSets(
      filter(
        and(
          has(upper(name), "STAR WARS"),
          gte(price, 500),
          gt(year(launchTime), 2010)
        )
      ),
      sort(
        desc(price)
      )
    )
    ```

#### Define the data model

Start by defining the `Set` data model, which represents the LEGO sets and serves as the return type of the `listSets()` 
request:


```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);
```

The fields `Name`, `Price`, and `LaunchTime` will later be configured to support filtering and sorting.

#### Add the request

Define the `listSets` request in the schema:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        // Add the 'listSets' request 
        schema.Request("listSets")
            // Enable support for the 'filter()' parameter 
            .SupportsFilter()
            // Enable support for the 'sort()' parameter 
            .SupportsSort()            
            // Define the return type as a list of 'Set' objects (List<Set>)
            .ReturnsListOfObjects<Set>(set =>
            {
                // Field configurations go here
            });
    }
}
```

At this point, the `listSets` request is defined and supports the `filter()` and `sort()` parameters. However, the 
fields must be explicitly configured before they can be filtered and sorted on, so let's configure this next.

[Learn more about requests →](requests.md)

#### Configure fields

Fields define which properties of the data model can be queried, filtered, and sorted. Expose the `Set` properties 
(`Name`, `Price`, and `LaunchTime`) for filtering and sorting:

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {
        schema.Request("listSets")
            .SupportsFilter()
            .SupportsSort()            
            .ReturnsListOfObjects<Set>(set =>
            {
                // Configure the 'Name' field
                set.SimpleField(it => it.Name)
                    // Override the default name ('Name') to use its JSON name
                    .HasName("name")
                    // Enable support for filtering on this field
                    .SupportsFilter(it => 
                        // Enable String functions like:
                        // - has(name, "War")
                        // - stw(upper(name), "STAR")
                        it.SupportsStringFilterFunctions()
                    )
                    // Enable support for sorting on this field
                    .SupportsSort(it => 
                        // Enable String functions like:
                        // - asc(lower(name))
                        // - desc(upper(name))
                        it.SupportsStringFieldFunctions()
                    );

                set.SimpleField(it => it.Price)
                    .HasName("price")
                    .SupportsFilter(it => 
                        // Enable Double functions like:
                        // - eq(round(price), 100)
                        it.SupportsDoubleFilterFunctions()
                    )
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());

                set.SimpleField(it => it.LaunchTime)
                    .HasName("launchTime")
                    .SupportsFilter(it =>
                        // Enable DateTime functions like:
                        // - gte(year(launchTime), 2010)
                        it.SupportsDateTimeFilterFunctions()
                    )
                    .SupportsSort(it => it.SupportsDateTimeFieldFunctions());
            });
    }
}
```

The `Schema` is now fully configured and ready to handle `listSets` requests.

[Learn more about fields →](fields/index.md)

---

## Next steps

Now that you've learned how to define a schema, let's learn how to [execute a query](../executing-queries/index.md).