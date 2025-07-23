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

In FunQL, requests define the entry points to your data. They represent the operations or endpoints that users can 
query, specifying what kind of data can be fetched and how it can be filtered or sorted.

### Key concepts of requests

1. **Request name**: Each request is uniquely identified by a name, like `listSets`. This name is used in queries to 
   specify the operation to execute.
2. **Parameters**: Requests can support parameters like `filter()` and `sort()` to provide advanced querying 
   capabilities.
3. **Return type**: Requests specify the type of data they return, such as a list of objects.

### Define the data model

As an example, we'll configure a `listSets` request with support for filtering and sorting a list of LEGO sets. Start by 
defining the data model that represents the LEGO sets:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);
```

This model allows us to filter and sort on `Name`, `Price`, and `LaunchTime`.

### Define the request

Next, define the `listSets` request in the schema:

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

At this point, the `listSets` request is defined and supports the `filter()` and `sort()` parameters. However, it 
doesn't expose any fields to use for filtering or sorting yet. Let's configure the fields next.

### Configure fields

Fields define which properties of the data model can be queried, filtered, and sorted. In the `Set` data model, we want 
to expose `Name`, `Price`, and `LaunchTime` to allow advanced querying.

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        // Add the 'listSets' request 
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
                        // Enable specific String filter functions like 'has()'
                        it.SupportsStringFilterFunctions()
                    )
                    // Enable support for sorting on this field
                    .SupportsSort(it => 
                        // Enable specific String field functions like 'lower'
                        it.SupportsStringFieldFunctions()
                    );

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