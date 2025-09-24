# Requests

In FunQL, requests define the entry points to your data. They represent the operations that users can query, specifying
what data can be fetched and how it can be filtered or sorted.

This page explains how to define and configure requests.

## Key concepts of requests

1. **Request name**: Each request has a unique identifier, like `listSets`, used in queries to specify the operation to
   execute.
2. **Parameters**: Requests specify which parameters they support, like the `filter()` and `sort()` parameters to
   provide advanced querying capabilities.
3. **Return type**: Requests define the type of data returned, such as a list of objects, along with the fields that can
   be queried, filtered, and sorted.

## Adding a request

Add a request by calling `schema.Request(string name)` in your `Schema` configuration.

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        // Add the 'listSets' request 
        schema.Request("listSets");
    }
}
```

With the `listSets` request defined, the schema can now be used to handle `listSets()` requests. However, the request
does not yet support any parameters or defines the fields to query, so let's configure that next.

## Supporting parameters

By default, a request does not support any parameters. You must explicitly enable parameters that are relevant to the
request. For example, the `listSets` request should support common list operations like filtering, sorting, and 
pagination:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.Request("listSets")
            // Enable support for the 'filter()' parameter 
            .SupportsFilter()
            // Enable support for the 'sort()' parameter 
            .SupportsSort()     
            // Enable support for the 'skip()' parameter 
            .SupportsSkip()  
            // Enable support for the 'limit()' parameter 
            .SupportsLimit();         
    }
}
```

You may pass an action to configure each parameter, for example, to define a default and maximum value for the `limit()` 
parameter:

```csharp
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.Request("listSets")
            .SupportsFilter()
            .SupportsSort()     
            .SupportsSkip()  
            .SupportsLimit(config => 
            {
                // Set the default limit to 10
                config.HasDefaultValue(new Limit(new Constant(10))) 
                    // Set the maximum limit to 100
                    .HasMaxLimit(100);                                
            });         
    }
}
```

When executing a request, it will now ensure that the `limit()` parameter is not greater than `100` and it will use the 
default value of `10` if no limit was given.

[Learn more about parameters →](parameters/index.md)

## Defining fields

With the parameters configured, the request can now be used to query data. However, it does not yet define which fields 
can be queried, filtered, and sorted. For this, you must specify the return type of the request:

```csharp 
public sealed class ApiSchema : Schema 
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    { 
        schema.Request("listSets")
            .SupportsFilter()
            .SupportsSort()     
            .SupportsSkip()  
            .SupportsLimit()
            .ReturnsListOfObjects<Set>(set =>
            {
                // Field configurations go here, for example:
                set.SimpleField(it => it.Price)
                    .HasName("price")
                    .SupportsFilter(it => it.SupportsDoubleFilterFunctions())
                    .SupportsSort(it => it.SupportsDoubleFieldFunctions());
            });
    }
}
```

This configures `listSets` to return a list of `Set` objects, with a `price` field that supports filtering and sorting. 
With all this configured, the request is now properly configured to handle queries like:

=== "REST"

    ```funql
    GET http://localhost:5000/sets
      ?filter=gte(price, 500)
      &sort=desc(price)
    ```

=== "QL"

    ```funql
    listSets(
      filter(
        gte(price, 500)
      ),
      sort(
        desc(price)
      )
    )
    ```

[Learn more about configuring fields →](fields/index.md)

## What's next

With the request configured, you can now execute queries with it. Or continue to configure the request to support 
additional parameters and fields.

- [Learn more about executing queries →](../executing-queries/index.md)
- [Learn more about parameters →](parameters/index.md)
- [Learn more about fields →](fields/index.md)