# Executing queries

Once the `Schema` is set up, it can be used to execute FunQL queries. All you need for this is an `IQueryable` data 
source that FunQL uses to query the data. 

This section will explain more about how to query data and the underlying execution pipeline.

## Execution pipeline

Executing a FunQL request involves several steps, including parsing, validating, LINQ translation, and the actual query 
execution. While you can use the `Schema` to handle these steps manually, the FunQL execution pipeline simplifies this 
process by ensuring that all steps are performed in the correct order and in the right way, reducing complexity for
developers.

[Learn more about the execution pipeline →](pipeline.md)

## Example request

For this example, we will query an in-memory list of LEGO sets, which we configured in [Defining a schema](
../schemas/index.md). FunQL provides two execution methods:

1. **`ExecuteRequestForParameters()`**: Designed for _REST_ APIs where FunQL parameters like `filter` and `sort` are 
   passed as URL query parameters.
2. **`ExecuteRequest()`**: Designed for full FunQL queries, treating FunQL as a query language (_QL_). This approach 
   combines all parameters into a single query.

=== "REST"

    ```csharp
    // Define the data model
    public sealed record Set(string Name, double Price, DateTime LaunchTime);
    
    // Prepare in-memory data
    var sets = new List<Set>
    {
        new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
        new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
        new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
        new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
    };
    
    // Define query parameters
    const string filter = @"and(
      has(upper(name), ""STAR WARS""),
      gte(price, 500),
      gt(year(launchTime), 2010)
    )";
    const string sort = "desc(price)";
    
    // Create the configured schema
    var schema = new ApiSchema();
    
    // Execute the listSets() FunQL request for the 'filter' and 'sort' parameters
    var result = await sets
        .ExecuteRequestForParameters(
            schema, 
            requestName: "listSets", 
            filter: filter, 
            sort: sort
        );
    ```

    <div class="result" markdown>

    The `ExecuteRequestForParameters()` method is ideal for REST APIs. It will process the `filter` and `sort` 
    parameters using the FunQL execution pipeline, which runs the following steps:
    
    1. Parse the `filter` and `sort` parameters using the `listSets` request configuration
    2. Validate the parameters based on the `listSets` request configuration
    3. Translate the parameters to LINQ expressions
    4. Apply the LINQ expressions to given `IQueryable<Set>`
    5. Execute the query, fetching the filtered and sorted result
    
    </div>

=== "QL"

    ```csharp
    // Define the data model
    public sealed record Set(string Name, double Price, DateTime LaunchTime);
    
    // Prepare in-memory data
    var sets = new List<Set>
    {
        new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
        new("LEGO Star Wars The Razor Crest", 599.99, new DateTime(2022, 10, 03)),
        new("LEGO DC Batman Batmobile Tumbler", 269.99, new DateTime(2021, 11, 01)),
        new("LEGO Harry Potter Hogwarts Castle", 469.99, new DateTime(2018, 09, 01)),
    };
    
    // Define FunQL request 
    const string request = @"listSets(
      filter(
        and(
          has(upper(name), ""STAR WARS""),
          gte(price, 500),
          gt(year(launchTime), 2010)
        )
      ),
      sort(
        desc(price)
      )
    )";
    
    // Create the configured schema
    var schema = new ApiSchema();
    
    // Execute the listSets() FunQL request
    var result = await sets.AsQueryable()
        .ExecuteRequest(schema, request);
    ```

    <div class="result" markdown>
    
    The `ExecuteRequest()` method is designed for use cases where FunQL acts as a fully integrated query language. It 
    will process the `request` using the FunQL execution pipeline, which runs the following steps:
    
    1. Parse the `request` using the `listSets` request configuration
    2. Validate the parameters based on the `listSets` request configuration
    3. Translate the parameters to LINQ expressions
    4. Apply the LINQ expressions to given `IQueryable<Set>`
    5. Execute the query, fetching the filtered and sorted result

    </div>

This example demonstrates how to query an in-memory data source. However, in real-world applications, you will typically 
need to:

- **Query an external database**: FunQL translates queries into LINQ expressions, enabling seamless integration with any
  LINQ-compatible data provider, such as Entity Framework Core, Dapper, or others. This allows you to execute queries 
  directly against your database.

    [Learn how to integrate FunQL with Entity Framework Core →](../integrations/efcore.md)

- **Enhance your REST API with FunQL**: Utilize FunQL as part of your REST API by supporting advanced query capabilities 
  like filtering, sorting, and pagination through URL query parameters.
    
    [Learn how to use FunQL in a REST API →](../examples/webapi.md)
