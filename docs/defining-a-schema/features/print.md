# Print

The print feature converts a FunQL Abstract Syntax Tree (AST) back into a readable FunQL query string. It's especially
useful for logging, debugging, or auditing.

This page explains how to enable the print feature, how to print requests, and how to customize the feature.

## Adding the feature

Use `AddPrintFeature()` to register the services required to print FunQL ASTs:

```csharp 
public sealed class ApiSchema : Schema 
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {
        schema.AddPrintFeature();
    }
}
```

This sets up the print feature with default configurations.

## Printing requests

Once the print feature is added, you can turn a parsed `Request` into a FunQL string.

```csharp 
var schema = new ApiSchema();
var request = schema.ParseRequest("listSets()");

// Get the request print visitor from the Schema
var requestPrintVisitor = schema.SchemaConfig.FindPrintConfigExtension()
    .RequestPrintVisitorProvider(schema.SchemaConfig);

// Create a string writer to which the AST will be printed
var stringWriter = new StringWriter();

// Visit the request and each of its child nodes to print them
await requestPrintVisitor.Visit(
    request,
    new PrintVisitorState(stringWriter),
    CancellationToken.None
);

// With all the nodes printed, get the text from the string writer
var text = stringWriter.ToString();
Console.WriteLine(text);
// Example output: listSets()
```

!!! note

    There currently is no extension method to easily print FunQL queries. The print API is still evolving, and we hope 
    to improve this in the future.

## Advanced configuration

You can customize how printing works by passing an action to `AddPrintFeature()`. Common use cases include customizing
how constants (JSON values) are serialized or swapping specific visitors for custom behavior.

```csharp 
public sealed class ApiSchema : Schema { protected override void OnInitializeSchema(ISchemaConfigBuilder schema) { 
        schema.AddPrintFeature(config => { 
            // Customize the print feature here            
            // Example: Provide a custom constant printer (cache instance to 
            // avoid repeated allocations)
            IConstantPrintVisitor<IPrintVisitorState>? visitor = null;
            config.MutableConfig.ConstantPrintVisitorProvider = _ =>
                visitor ??= new MyCustomConstantPrintVisitor();
        });
    }
}
```

Use this approach when you need to adjust or extend the behavior of the printing services.

!!! tip

    Using Newtonsoft.Json in your application? No problem! Simply replace the default JSON printer with your own 
    Newtonsoft.Json printer.

    [Learn how to integrate FunQL with Newtonsoft.Json →](../../integrations/newtonsoftjson.md).

## What's next

With the features now all explained, check out the following pages:

- [Learn more about requests →](../requests.md)