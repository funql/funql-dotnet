# Entity Framework Core

[Entity Framework Core (EF Core)](https://learn.microsoft.com/en-us/ef/core/) is a powerful Object-Relational Mapper 
(ORM) for .NET that allows developers to interact with databases using .NET objects and LINQ. FunQL seamlessly 
integrates with EF Core by translating FunQL queries into LINQ expressions, which EF Core further translates to database 
queries.

This section will explain more about integrating FunQL with EF Core and using EF Core-specific optimizations (e.g., 
`CountAsync` for asynchronous count operations).

You can also refer to the [WebApi sample](https://github.com/funql/funql-dotnet/tree/main/samples/WebApi) for a
practical example of the EF Core integration.

## Getting started

FunQL supports EF Core out of the box by working directly with `IQueryable` objects. Create a `DbContext` and then 
directly execute FunQL queries using `DbSet<T>`, which implements `IQueryable`.

### Create DbContext

As an example, we'll configure a `DbContext` for querying LEGO sets (as configured in [Defining a Schema](
../defining-a-schema/index.md)):

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

public class ApiContext : DbContext 
{
    public DbSet<Set> Sets { get; set; }
}
```

### Execute FunQL query

With the `DbContext` configured, you can use FunQL to build and execute queries on it. Here's how you can configure 
FunQL to work with your `DbContext`:

```csharp
// Create the configured DbContext (or use dependency injection)
var context = new ApiContext();
// Create the configured schema (or use dependency injection)
var schema = new ApiSchema();

// Execute the listSets() FunQL request on ApiContext.Sets
var result = await context
    .Sets
    .ExecuteRequestForParameters(
        schema, 
        requestName: "listSets", 
        filter: "gte(price, 500)", 
        sort: "desc(price)"
    );
```

That's it! FunQL will execute a filtered and sorted query directly on your database with full async support.

## Optimize async support

While FunQL integrates with EF Core out of the box, EF Core's asynchronous methods like `CountAsync()` can further 
optimize database interactions. 

!!! note

    FunQL executes LINQ queries asynchronously out of the box if `IQueryable` implements `IAsyncEnumerable`. The 
    implementation is equivalant to calling EF Core's `ToListAsync()` method. While not necessary to use EF Core's 
    `ToListAsync()`, it is necessary to use EF Core's `CountAsync()` directly as there's no generic interface for async 
    counting.

### 1. Create EF Core execution handler

Implement a custom `IExecutionHandler` that executes the LINQ queries using EF Core:

```csharp
/// <summary>
/// Execution handler that executes the <see cref="ExecuteLinqExecuteContext.Queryable"/> to get the data for
/// <see cref="IExecutorState.Request"/>.
/// <para>
/// This handler replaces the <see cref="ExecuteLinqExecutionHandler"/> so it can use the async EFCore methods instead:
/// <list type="bullet">
/// <item>
/// <description>
///   <see cref="EntityFrameworkQueryableExtensions.ToListAsync{T}(IQueryable{T},CancellationToken)"/> to get the data
/// </description>
/// </item>
/// <item>
/// <description>
///   <see cref="EntityFrameworkQueryableExtensions.CountAsync{T}(IQueryable{T},CancellationToken)"/> to count the items
/// </description>
/// </item>
/// </list>
/// </para>
/// </summary>
/// <remarks>
/// Requires <see cref="ExecuteLinqExecuteContext"/> context, just like <see cref="ExecuteLinqExecutionHandler"/>.
///
/// <para>
/// Note that <see cref="ExecuteLinqExecutionHandler"/> already handles <see cref="IAsyncEnumerable{T}"/> the same or
/// similar to how <see cref="EntityFrameworkQueryableExtensions.ToListAsync{T}(IQueryable{T},CancellationToken)"/>
/// handles it. Only the <see cref="EntityFrameworkQueryableExtensions.CountAsync{T}(IQueryable{T},CancellationToken)"/>
/// is different as there's no abstract way to count asynchronously; this is a specific implementation in EFCore.
/// </para>
/// </remarks>
public class EntityFrameworkCoreExecuteLinqExecutionHandler : IExecutionHandler
{
    /// <summary>Default name of this handler.</summary>
    public const string DefaultName = "WebApi.EntityFrameworkCoreExecuteLinqExecutionHandler";
    
    // Handler should be called late in the pipeline as LINQ does the data fetching, which is pretty much at the end
    /// <summary>Default order of this handler.</summary>
    /// <remarks>
    /// Should be called before <see cref="ExecuteLinqExecutionHandler"/> so this handler can take over the execution.
    /// </remarks>
    public const int DefaultOrder = ExecuteLinqExecutionHandler.DefaultOrder - 100;

    /// <inheritdoc/>
    public async Task Execute(IExecutorState state, ExecutorDelegate next, CancellationToken cancellationToken)
    {
        var context = state.FindContext<ExecuteLinqExecuteContext>();
        // Early return if no ExecuteLinqExecuteContext set
        if (context == null)
        {
            await next(state, cancellationToken);
            return;
        }
        
        var queryable = context.Queryable;
        var countQueryable = context.CountQueryable;

        // Query the data
        state.Data = await EntityFrameworkQueryableExtensions.ToListAsync((dynamic)queryable, cancellationToken);

        // Count the data if necessary
        if (countQueryable != null)
        {
            int totalCount = await EntityFrameworkQueryableExtensions.CountAsync(
                (dynamic)countQueryable,
                cancellationToken
            );
            state.SetTotalCount(totalCount);
        }
        
        // This is the last step that executes the request, so don't call next
    }
}
```

### 2. Add extension method

To simplify adding EF Core support to FunQL, create an extension method for the configuration:

```csharp
/// <summary>
/// Extensions related to <see cref="IExecuteConfigBuilder"/> and <see cref="Microsoft.EntityFrameworkCore"/>.
/// </summary>
public static class ExecuteConfigBuilderEntityFrameworkCoreExtensions
{
    /// <summary>
    /// Adds the <see cref="EntityFrameworkCoreExecuteLinqExecutionHandler"/> to given <paramref name="builder"/> if not
    /// yet added.
    /// </summary>
    /// <param name="builder">Builder to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithEntityFrameworkCoreExecuteLinqExecutionHandler(
        this IExecuteConfigBuilder builder
    )
    {
        // Lazy provider so handler is only created when executing
        IExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            EntityFrameworkCoreExecuteLinqExecutionHandler.DefaultName,
            _ => handler ??= new EntityFrameworkCoreExecuteLinqExecutionHandler(),
            EntityFrameworkCoreExecuteLinqExecutionHandler.DefaultOrder
        );
    }
}
```

### 3. Configure Schema

Finally, update your schema to include the custom execution handler for EF Core:

```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {                 
        schema.AddExecuteFeature(it =>
        {
            // Add the EntityFrameworkCoreExecuteLinqExecutionHandler so specific EF Core methods are used 
            it.WithEntityFrameworkCoreExecuteLinqExecutionHandler();
        });
    }
}
```

That's it, FunQL will now call specific async EF Core methods when executing queries.