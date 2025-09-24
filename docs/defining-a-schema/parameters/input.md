# Input

The `input` parameter specifies structured input data for a request. It's typically used for create and update 
operations, but can be used by any request that requires extra data.

## Syntax

- `#!funql input(value: Any)`

Examples:

- `#!funql input("533d3fe3-bccc-405a-9904-4f516e892856")`
- `#!funql input({"name": "LEGO Star Wars Millennium Falcon"})`

## C# representation

The `input` parameter is represented by the `Input` node in C#. For example:

```funql
input({"name": "LEGO Star Wars Millennium Falcon"})
```

corresponds to:

```csharp
public sealed record SetInput(string Name);

new Input(new Constant(new SetInput("LEGO Star Wars Millennium Falcon"));
```

The `Input` node has a single child, a `Constant` node containing the input value.

## Configuration

Enable the `input` parameter for a request by calling the `SupportsInput<TInput>()` method, specifying the input type:

```csharp 
public sealed record SetInput(string Name);

public sealed class ApiSchema : Schema
{ 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) 
    {        
        schema.Request("addSet")
            .SupportsInput<SetInput>(config => 
            {
                // Configure the parameter
            });
    }
}
```

!!! note

    The configured .NET type is used to parse and validate the `input` value.

## Execution

The `input` parameter can be used to e.g., add an item to your data set:

```csharp
public sealed record Set(string Name, double Price, DateTime LaunchTime);

var sets = new List<Set>
{
    new("LEGO Star Wars Millennium Falcon", 849.99, new DateTime(2017, 10, 01)),
};

// Input node (e.g., from parsing)
// input({
//   "name": "LEGO Star Wars The Razor Crest", 
//   "price": 599.99, 
//   "launchTime": "2022-10-03T00:00:00"
// })
var input = new Input(new Constant(
    new SetInput(
        "LEGO Star Wars The Razor Crest", 
        599.99,
        new DateTime(2022, 10, 03)
    )
));

// Add the item to the data set
sets.Add(input.Constant.Value);

// Print the result
Console.WriteLine(string.Join("\n", sets));
// Output:
// Set { Name = LEGO Star Wars Millennium Falcon, Price = 849.99, LaunchTime = 1-10-2017 00:00:00 }
// Set { Name = LEGO Star Wars The Razor Crest, Price = 599.99, LaunchTime = 3-10-2022 00:00:00 }
```