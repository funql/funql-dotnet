# Parse

The parse feature is crucial for transforming raw FunQL queries into structured query nodes by generating an Abstract 
Syntax Tree (AST). Parsing is the first step in processing a FunQL query before it can be validated and executed. 

This page explains how to configure the parse feature and demonstrates how to parse queries.

## Adding the feature

The `AddParseFeature()` method registers all services required for parsing FunQL requests. Use this to set up the parse 
feature in your schema:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddParseFeature();
    }
}
```

This sets up the parse feature with default configurations, making your schema capable of parsing raw FunQL requests.

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

    **Parsing code:**

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

    // Output (parsed AST):
    // new Request(
    //     Name: "listSets",
    //     Parameters: [
    //         new Filter(
    //             new GreaterThanOrEqual(
    //                 new FieldPath([new NamedField("price")]),
    //                 new Constant(500)
    //             )
    //         ),
    //         new Sort([
    //             new Descending(new FieldPath([new NamedField("price")]))
    //         ])
    //     ]
    // );
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

    **Parsing code**:

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

    // Output (parsed AST):
    // new Request(
    //     Name: "listSets",
    //     Parameters: [
    //         new Filter(
    //             new GreaterThanOrEqual(
    //                 new FieldPath([new NamedField("price")]),
    //                 new Constant(500)
    //             )
    //         ),
    //         new Sort([
    //             new Descending(new FieldPath([new NamedField("price")]))
    //         ])
    //     ]
    // );
    ```

    The `ParseRequest()` method is designed for use cases where FunQL acts as a fully integrated query language. There's 
    no need to pass the `requestName`, as this is automatically resolved when parsing the request. The result will be 
    the parsed `Request`, which is the full AST, ready for further processing like validation and execution.
    
    </div>

!!! tip

    Use the [execute feature](execute.md) to simplify query processing. It automates essential steps like parsing, 
    validation, LINQ translation, and data fetching, allowing you to handle complex FunQL queries with just a single 
    method call.

## Advanced configuration

You can customize the parse feature by passing an action to the `AddParseFeature()` method. For example, replace the 
default constant parser with your own:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddParseFeature(config =>
        {
            // Customize the parse feature here
            config.WithConstantParserProvider(_ => new MyCustomConstantParser());
        });
    }
}
```

Use this approach when you need to adjust or extend the behavior of the parsing services. 

!!! tip

    Using Newtonsoft.Json in your application? No problem! Simply replace the default JSON parser with your own 
    Newtonsoft.Json parser.

    [Learn how to integrate FunQL with Newtonsoft.Json →](../../integrations/newtonsoftjson.md).

## What's next

Now that you have a fully parsed FunQL `Request`, ensure the query is valid:

- [Learn more about the validate feature →](validate.md)