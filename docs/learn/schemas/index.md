# Defining a Schema

A `Schema` in FunQL .NET serves as the core configuration and entry point for processing FunQL queries. It defines what 
data can be queried and which query options are supported (filtering, sorting, pagination). This section goes over how 
to build a FunQL Schema.

## Implementing the Schema

To get started with FunQL, you must implement the FunQL `Schema` class and override the 
`OnInitializeSchema(ISchemaConfigBuilder)` method:

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
    `OnFinalizeSchema(ISchemaConfigBuilder)`, which can be used to e.g. edit settings that may have been set by 
    conventions.

Schemas are entirely developer-controlled, with explicit configuration to ensure clarity and predictability: No hidden
behaviors or 'automagic' configurations without explicitly configuring them. This means you need to explicitly add 
features like parsing, validation, and execution.

## Adding features

Features define what your schema can do: from parsing and validating FunQL queries to translating and executing those 
queries using LINQ expressions. By requiring features to be explicitly added, FunQL empowers developers to only include 
the functionalities they need, ensuring optimal performance and clarity.

To get started quickly, add the most commonly used features:

- **Core features**: Provides common functionalities like parsing, query validation, and execution.
- **LINQ feature**: Allows FunQL queries to be translated into LINQ expressions, enabling integration with in-memory 
  `IQueryable` collections or databases like Entity Framework (EF) Core.

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

## Adding requests

In FunQL, requests define the entry points to your schema. They represent the operations that users can query, 
specifying what data can be fetched and how it can be filtered or sorted.

### Key concepts of requests

1. **Request name**: Each request has a unique identifier, like `listSets`, used in queries to specify the operation to 
   execute.
2. **Parameters**: Requests specify which parameters they support, like the `filter()` and `sort()` parameters to 
   provide advanced querying capabilities.
3. **Return type**: Requests define the type of data returned, such as a list of objects, specifying which of the 
   returned fields can be queried, filtered, and sorted.

### Example request

As an example, we'll configure a `listSets` request with support for filtering and sorting a list of LEGO sets. Once 
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

Start by defining the `Set` data model that represents the LEGO sets:

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