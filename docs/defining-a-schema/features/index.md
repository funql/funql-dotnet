# Features

FunQL provides a modular architecture where each capability (like parse, validate, execute) is its own configurable
feature. By only including the features you need, you ensure that your schema remains lightweight and optimized for your
use case.

This section introduces FunQL's features, explains their role, and shows you how to add them to your schema.

## Parse

The parse feature enables FunQL to transform raw queries into structured query nodes by generating an Abstract Syntax 
Tree (AST). This is the first step in handling FunQL queries before validation or execution.

**Adding the feature:**

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

The validate feature allows for validating that FunQL queries comply with the rules defined in the schema. This prevents 
invalid queries from being processed by catching errors early.

**Adding the feature:**

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

The execute feature simplifies the entire query-processing pipeline by combining parsing, validation, and execution into
a single method, leveraging FunQL's [execution pipeline](../../executing-queries/pipeline.md).

**Adding the feature:**

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

**Adding the feature:**

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

The visit feature provides functionality to traverse and inspect the FunQL AST. It is commonly used for the [validate
feature](#validate), but it can be extended for custom operations like query rewriting or auditing.

**Adding the feature:**

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

The print feature converts the FunQL AST into a readable FunQL query string. This is useful for logging or debugging
complex queries.

**Adding the feature:**

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

To quickly enable all core features (Parse, Validate, Execute, Print, and Visit), use:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddCoreFeatures();
    }
}
```

For a lightweight schema, selectively add only the features you need.
