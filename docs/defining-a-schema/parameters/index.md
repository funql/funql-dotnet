# Parameters

Parameters add query capabilities to a request (e.g., filtering, sorting, pagination). When configuring a request, you 
explicitly enable the parameters it should support.

This page gives an overview of all parameters.

## Filter

The `filter` parameter specifies one or more predicates to filter the data with. Predicates can be combined using 
`#!funql and()` and `#!funql or()` to create more advanced filters. 

**Examples:**

- `#!funql filter(gte(price, 500))`
- `#!funql filter(and(gte(price, 500), has(upper(name), "STAR")))`

**Enabling the parameter:**

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsFilter();
    }
}
```

[Learn more about the filter parameter →](filter.md)

## Sort

The `sort` parameter defines how to sort the data. Expressions can be combined using a comma (`,`).

**Examples:**

- `#!funql sort(desc(price))`
- `#!funql sort(desc(price), asc(lower(name)))`

**Enabling the parameter:**

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsSort();
    }
}
```

[Learn more about the sort parameter →](sort.md)

## Skip

The `skip` parameter specifies how many items to skip from the result set before returning data. It is commonly used for 
pagination in combination with the [`limit`](#limit) parameter.

**Example:**

- `#!funql skip(20)`

**Enabling the parameter:**

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsSkip();
    }
}
```

[Learn more about the skip parameter →](skip.md)

## Limit

The `limit` parameter restricts the maximum number of items returned in a single response. It is commonly used for 
pagination in combination with the [`skip`](#skip) parameter.

**Example:**

- `#!funql limit(10)`

**Enabling the parameter:**

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsLimit();
    }
}
```

[Learn more about the limit parameter →](limit.md)

## Count

The `count` parameter requests the total number of items matching the current query. It is commonly used in combination 
with pagination to display the total number of available results.

**Example:**

- `#!funql count(true)`

**Enabling the parameter:**

```csharp 
public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("listSets")
            .SupportsCount();
    }
}
```

[Learn more about the count parameter →](count.md)

## Input

The `input` parameter specifies the input for a certain request.

**Example:**

- `#!funql input({"name": "LEGO Star Wars R2-D2"})`

**Enabling the parameter:**

```csharp 
public sealed record SetInput(string Name);

public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("addSet")
            .SupportsInput<SetInput>();
    }
}
```

[Learn more about the input parameter →](input.md)