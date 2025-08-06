# Parse

The parse feature is crucial for transforming raw FunQL queries into structured query nodes by creating an Abstract 
Syntax Tree (AST). Parsing is the first step in processing a FunQL query before it can be validated and executed. 

This page explains how to add the parse feature to your schema and demonstrates how it works, including examples for
both the QL and REST approaches to parsing.

## Adding feature

The `AddParseFeature()` method registers all the necessary services used to parse FunQL requests within your schema.

### Basic configuration

To enable the parse feature:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddParseFeature();
    }
}
```

This method sets up the parse feature with default configurations, making your schema capable of parsing raw FunQL 
requests.

### Advanced configuration

You can optionally pass an action to configure the feature with the `AddParseFeature(config => { })` overload. For 
example:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddParseFeature(config =>
        {
            // Customize your parse feature here
        });
    }
}
```

Use this approach when you need to adjust or extend the behavior of the parsing services.

## Parsing requests

Once the parse feature is added, the schema exposes two methods to parse FunQL requests depending on your use case:

1. **`ParseRequestForParameters()`**: Designed for _REST_ APIs where FunQL parameters like `filter` and `sort` are
   passed as URL query parameters.
2. **`ParseRequest()`**: Designed for full FunQL queries, treating FunQL as a query language (_QL_). This approach
   combines all parameters into a single query.

=== "REST"

    ```funql title="Example request"
    GET http://localhost:5000/sets
      ?filter=gte(price, 500)
      &sort=desc(price)
    ```

    <div class="result" markdown>

    **Parsing request**:

    ```csharp
    // Get query parameters
    const string filter = "gte(price, 500)";
    const string sort = "desc(price)";

    // Create the configured schema
    var schema = new ApiSchema();

    // Parse the request
    var request = schema.ParseRequestForParameters(
        requestName: "listSets", 
        filter: filter, 
        sort: sort
    );
    ```

    The `ParseRequestForParameters()` method is ideal for REST APIs. It will parse the `filter` and `sort` parameters 
    using the configuration for the `listSets` request. The result will be the parsed `Request`, which is the full AST, 
    ready for further processing like validation and execution.
    
    </div>

=== "QL"

    ```funql title="Example request"
    listSets(
      filter(
        gte(price, 500)
      ),
      sort(
        desc(price)
      )
    )
    ```

    <div class="result" markdown>

    **Parsing request**:

    ```csharp
    // Get raw FunQL query
    const string rawRequest = @"listSets(
      filter(
        gte(price, 500)
      ),
      sort(
        desc(price)
      )
    )";

    // Create the configured schema
    var schema = new ApiSchema();

    // Parse the request
    var request = schema.ParseRequest(rawRequest);
    ```

    The `ParseRequest()` method is designed for use cases where FunQL acts as a fully integrated query language. There's 
    no need to pass the `requestName`, as this is automatically resolved when parsing the request. The result will be 
    the parsed `Request`, which is the full AST, ready for further processing like validation and execution.
    
    </div>

## What's next

Now that you have a fully parsed FunQL `Request`, ensure the query is valid:

- [Learn more about the validate feature →](validate.md)