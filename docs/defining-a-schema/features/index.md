# Features

FunQL provides a modular architecture where each capability (like parse, validate, execute) is its own configurable
feature. By adding only the features you need, you ensure that your schema remains lightweight and optimized for your
use case.

This section gives an overview of each feature, its purpose, and how to add it to your schema.

## Parse

The parse feature enables the schema to parse raw FunQL queries into query nodes. Parsing is the first step in
processing a FunQL query, converting it into an Abstract Syntax Tree (AST) for further processing.

- **Purpose**: Converts raw FunQL queries into a structured AST for further processing.
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

[Learn more about the parse feature →](parse.md)

## Validate

The validate feature allows for validating that FunQL queries comply with the rules defined in the schema. It uses the 
[visit feature](#visit) internally to traverse the AST for node validation.

- **Purpose**: Ensures query structure and parameters follow schema rules, detecting and preventing invalid queries.
- **When to use**: Include this feature to prevent invalid or malformed queries being executed.
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
  
[Learn more about the validate feature →](validate.md)

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

[Learn more about the execute feature →](execute.md)

## LINQ

The LINQ feature translates FunQL queries into LINQ expressions, enabling seamless integration with LINQ-based 
frameworks such as Entity Framework Core.

- **Purpose**: Translates FunQL operations like filtering, sorting, and pagination to LINQ expressions.
- **When to use**: Use this feature when querying LINQ-compatible frameworks or in-memory collections.
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

[Learn more about the LINQ feature →](linq.md)

## Visit

The visit feature provides functionality to traverse and inspect the FunQL AST. While this feature is primarily used by 
the [validate feature](#validate), it can also be extended for custom operations.

- **Purpose**: Facilitates AST traversal for operations like validation or custom modifications.
- **When to use**: Usually included as a dependency for validation but can also be used explicitly for custom AST 
  processing.
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

[Learn more about the visit feature →](visit.md)

## Print

The print feature translates the FunQL AST back into a FunQL query string.

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

[Learn more about the print feature →](print.md)

---

## Adding core features

To quickly enable most of the commonly used features, FunQL provides a method to add all core features at once:

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