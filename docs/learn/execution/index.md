# Executing queries

Once the `Schema` is set up, it can be used to execute FunQL queries. All you need for this is an `IQueryable` data 
source that FunQL uses to query the data. 

This section will explain more about how to query data and the underlying execution pipeline.

## Example request

For this example we will query an in-memory list of LEGO sets, which we configured in [Defining a schema](
../schemas/index.md):

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
const string filter = "and(has(upper(name), \"STAR WARS\"), gte(price, 500), gt(year(launchTime), 2010))";
const string sort = "desc(price)";

// Create the configured schema
var schema = new ApiSchema();

// Execute the listSets() FunQL request for the 'filter' and 'sort' parameters
var result = await sets.AsQueryable()
    .ExecuteRequestForParameters(schema, requestName: "listSets", filter: filter, sort: sort);
```

The `ExecuteRequestForParameters()` method will process the `filter` and `sort` parameters using the FunQL execution
pipeline, which runs the following steps:

1. Parse the `filter` and `sort` parameters using the `listSets` request configuration
2. Validate the parameters based on the `listSets` request configuration
3. Translate the parameters to LINQ expressions
4. Apply the LINQ expressions to given `IQueryable<Set>`
5. Execute the query, fetching the filtered and sorted result

This example only shows how to query an in-memory data source, but most of the time you'll want to query a database:

[Learn how to integrate FunQL with Entity Framework Core →](../integrations/efcore.md)

Instead of using static query parameters like in the example, one of the biggest use cases for FunQL is building a REST 
API to support dynamic queries:

[Learn how to use FunQL in a REST API →](../examples/webapi.md)

## Execution pipeline

Executing a FunQL request requires multiple steps, like parsing, validating, LINQ translation, and the actual execution. 
While you can use the `Schema` to run all these steps manually, FunQL comes with an execution pipeline that runs all 
these steps for you.

[Learn more about the execution pipeline →](pipeline.md)