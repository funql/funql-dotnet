# Execute

The execute feature wires together parsing, validation, and execution so you can run FunQL requests end-to-end with a
single call. It builds on the execution pipeline, ensuring that all steps are performed in the correct order and in the 
right way, reducing complexity for developers.

This page explains how to enable the execute feature, how to run requests, and how to customize the pipeline.

## Adding the feature

Use `AddExecuteFeature()` to register the services required to execute FunQL requests:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    { 
        schema.AddExecuteFeature();
    }
}
```

This sets up the execute feature with default configurations and the core execution handlers, like handlers for parsing 
and validating. See [Advanced configuration](#advanced-configuration) on how to customize the feature.

## Executing requests

To avoid duplication, the examples for executing FunQL requests (both REST-style parameters and full FunQL queries) are 
documented in the _Executing queries_ guide:

- [Learn how to execute requests →](../../executing-queries/index.md)

## Advanced configuration

The `AddExecuteFeature()` method has two optional arguments:

- `action`: An action to customize the feature.
- `withCoreExecutionHandlers`: Whether to add all core execution handlers. Default `true`.

For example, add your own execution handler:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.AddExecuteFeature(config => { 
            // Customize the feature here            
            config.WithExecutionHandler("MyCustomHandler", new MyCustomHandler(), 0);
        });
    }
}
```

### Core execution handlers

With `withCoreExecutionHandlers` set to `true` (default), the execute feature adds the following core execution 
handlers:

- **`ParseRequestExecutionHandler`**: Parses a full FunQL request string (e.g., `listSets(skip(1))`) into a `Request` 
  AST for further processing. This is designed for full FunQL queries, treating FunQL as a query language (_QL_).
- **`ParseRequestForParametersExecutionHandler`**: Builds a `Request` from given parameters (e.g., `filter`, `sort`), 
  parsing each parameter separately. This is designed for _REST_ APIs where the FunQL parameters are extracted from the 
  URL, like  `../sets?filter=gte(price, 500)&sort=desc(price)`.
- **`ValidateRequestExecutionHandler`**: Validates the parsed `Request` against the schema.

!!! tip

    Add the [LINQ feature](linq.md) **after** the execute feature to automatically register the LINQ execution handlers. 
    These handlers translate the `Request` into LINQ and apply filter/sort/limit/skip to the target `IQueryable`.

### Custom execution handlers

You can also add your own execution handlers to the pipeline. For example, a handler that measures the time it takes to 
execute the request:

```csharp 
public sealed class TimingExecutionHandler : IExecutionHandler 
{     
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await next(state, cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            Console.WriteLine($"Request executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
```

Then register the handler when adding the execute feature:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.AddExecuteFeature(config => { 
            // Register the handler here      
            config.WithExecutionHandler(
                // Name of the handler, used to identify it in the pipeline
                name: "TimingExecutionHandler", 
                new TimingExecutionHandler(), 
                // Lower order means earlier in the pipeline, so pick a very 
                // low value to measure the whole pipeline 
                order: int.MinValue
            );
        });
    }
}
```

Now whenever a request is executed, the `TimingExecutionHandler` will log how long it takes to execute the request.

**Dynamic context:**

For handlers that require dynamic context or need to share data with other handlers, use `IExecutorState` to enter an 
`IExecuteContext`. This stores data that handlers later in the pipeline can then use.

As an example, we update the `TimingExecutionHandler` to enter a `TimingContext` with a shared `Stopwatch` for a 
different handler to use:

```csharp 
public sealed record TimingContext(Stopwatch Stopwatch) : IExecuteContext;

public sealed class TimingExecutionHandler : IExecutionHandler 
{     
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        // Enter the context before calling the next handler
        state.EnterContext(new TimingContext(stopwatch));
        
        await next(state, cancellationToken);
        
        // Exit the context to clean up the data
        state.ExitContext();
    }
}
```

Now create a handler that uses the `TimingContext` to log the elapsed time:

```csharp 
public sealed class LogTimingExecutionHandler : IExecutionHandler 
{     
    public Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        // Find the context and log the elapsed time
        var context = state.FindContext<TimingContext>();
        if (context != null) {
            Console.WriteLine($"Time elapsed is {context.stopwatch.ElapsedMilliseconds} ms");
        } // else: Context was not entered
        
        return next(state, cancellationToken);
    }
}
```

The `LogTimingExecutionHandler` logs the elapsed time only when a `TimingContext` has been entered.

## What's next

With the execution feature added, it's time to use it:

- [Learn more about executing queries →](../../executing-queries/index.md)
- [Learn more about the LINQ feature →](linq.md)
