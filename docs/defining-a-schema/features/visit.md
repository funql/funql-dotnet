# Visit

The visit feature provides the infrastructure to traverse and inspect the FunQL Abstract Syntax Tree (AST). It powers
other features like validation and can also be extended for custom scenarios such as query analysis, rewriting, or 
auditing.

This page explains how to enable the visit feature, what it's used for, and how to customize it.

## Adding the feature

Use `AddVisitFeature()` to register the services required to traverse FunQL ASTs:

```csharp
public sealed class ApiSchema : Schema 
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {
        schema.AddVisitFeature();
    }
}
```

This sets up the visit feature with default configurations. Other features (like validate) will leverage it to walk the
AST.

## Visiting requests

While the visit feature is primarily used by other features, you can also use it directly to traverse the AST. For 
example, to visit all nodes of a request:

```csharp 
var schema = new ApiSchema(); 
var request = schema.ParseRequest("listSets()");

// Get the request visitor from the Schema
var requestVisitor = schema.SchemaConfig.FindVisitConfigExtension()
    .RequestVisitorProvider(schema.SchemaConfig);

// Visit the request and each of its child nodes
await requestVisitor.Visit(
    request,
    new VisitorState(
        // Callback called upon entering a node
        onEnter: (node, state, token) => Task.CompletedTask,
        // Callback called upon exiting a node
        onExit: (node, state, token) => Task.CompletedTask
    ),
    CancellationToken.None
);
```

The `VisitorState` lets you perform custom logic when visiting nodes via the `onEnter` and `onExit` callbacks. Each 
visitor calls these for every node it visits. The callbacks are fully async so you can perform non-blocking work during 
traversal, like auditing or permission checks.

## Advanced configuration

You can customize the visit feature by passing an action to `AddVisitFeature()`:

```csharp 
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) { 
        schema.AddVisitFeature(config => { 
            // Customize the feature here
            // Example: Provide a custom visitor for field functions (cache 
            // instance to avoid repeated allocations)
            MyCustomFieldFunctionVisitor? visitor = null;
            config.MutableConfig.FieldFunctionVisitorProvider = _ => 
                visitor ??= new MyCustomFieldFunctionVisitor();
        }); 
    }
}
```

!!! note

    If you introduce custom node types, ensure corresponding visitors properly handle them. Otherwise, traversal may 
    result in exceptions. For example, if you introduce a new `FieldFunction` type, ensure the `FieldFunctionVisitor` 
    properly handles the new type.

## What's next

See how the visit feature is used in other parts:

- [Learn more about the print feature →](print.md)