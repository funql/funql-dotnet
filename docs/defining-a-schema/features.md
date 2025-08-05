# Features

FunQL provides a modular architecture where each capability (like parse, validate, execute) is its own configurable 
feature. By adding only the features you need, you ensure that your schema remains lightweight and optimized for your 
use case. 

This section gives an overview of each feature, its purpose and how to add it to your schema.

## Parse

The parse feature enables the schema to parse raw FunQL queries into query nodes. Parsing is the first step in 
processing a FunQL query, converting it into an Abstract Syntax Tree (AST) for further processing.

- **Purpose**: Converts raw FunQL queries into an internal data structure for further processing.
- **When to use**: Add this feature when you need to parse incoming FunQL queries, e.g., in a REST API.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddParseFeature();
        }
    }
    ```

## Validate

The validate feature allows for validating that FunQL queries comply with the rules defined in the schema.

- **Purpose**: Validates the structure and parameters of a query.
- **When to use**: Include this feature to prevent invalid or malformed queries from reaching the execution stage.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddValidateFeature();
        }
    }
    ```

## Execute

The execute feature allows you to use the [execution pipeline](../executing-queries/pipeline.md) for parsing, 
validating, and executing FunQL queries, simplifying the entire execution process to a single method call.

- **Purpose**: Executes each step in the execution process.
- **When to use**: Use this feature when connecting FunQL to queryable data sources like LINQ collections or databases.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddExecuteFeature();
        }
    }
    ```

## LINQ

The LINQ feature translates FunQL queries into LINQ expressions, making it compatible with LINQ-based frameworks like 
Entity Framework Core.

- **Purpose**: Translates FunQL operations like filtering, sorting, or pagination into LINQ expressions.
- **When to use**: Include this feature when working with LINQ-compatible frameworks or in-memory collections.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddLinqFeature();
        }
    }
    ```

## Visit

The visit feature allows traversal and manipulation of the FunQL AST.

- **Purpose**: Visits each FunQL node for additional processing, like validating nodes or translating them to LINQ.
- **When to use**: Add this feature for processing FunQL nodes.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddVisitFeature();
        }
    }
    ```

## Print

The print feature converts a FunQL AST back into a human-readable query string.

- **Purpose**: Translates a FunQL AST to a FunQL query string, ready to query a FunQL API.
- **When to use**: Use this feature to serialize FunQL queries.
- **Configuration**:
    ```csharp
    public sealed class ApiSchema : Schema
    {
        protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
        {
            schema.AddPrintFeature();
        }
    }
    ```
  
---

## Adding core features

To quickly enable most of the commonly-used features, FunQL provides a method to add all core features at once:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddCoreFeatures();
    }
}
```

This includes:

- Parse
- Validate
- Execute
- Print
- Visit

For a lightweight schema, you can selectively add only the features you need.