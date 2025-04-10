# FunQL .NET Basic Sample

This sample demonstrates how to use FunQL .NET in a minimal, self-contained console application. It shows how to define
a FunQL schema configuration, enable support for filtering and sorting, execute a FunQL request, and output the result
using System.Text.Json.

## Prerequisites

- .NET SDK 9.0 or later

## Getting started

### 1. Clone the repository

```shell
git clone https://github.com/funql/funql-dotnet.git
cd funql-dotnet/samples/Basic
```

### 2. Run the sample

```shell
dotnet run
```

### 3. Check the output

The sample executes a FunQL query using a `listSets()` request with both a `filter` and a `sort` query parameter. The
result is printed to the console as formatted JSON.

#### Example request

```csharp
const string filter = "and(has(upper(name), \"STAR WARS\"), gte(price, 500), gt(year(launchTime), 2010))";
const string sort = "desc(price)";

var result = await sets
    .ExecuteRequestForParameters(schema, requestName: "listSets", filter: filter, sort: sort);
```

#### Example response

The response includes all LEGO sets where:

- The uppercased `name` contains `"STAR WARS"`
- The `price` is greater than or equal to `500`
- The `launchTime`'s year is greater than `2010`

```json
[
  {
    "name": "LEGO Star Wars Millennium Falcon",
    "price": 849.99,
    "launchTime": "2017-10-01T00:00:00"
  },
  {
    "name": "LEGO Star Wars The Razor Crest",
    "price": 599.99,
    "launchTime": "2022-10-03T00:00:00"
  }
]
```

## Project structure

```
Basic/
├── Program.cs        # Main entry point of the console app
└── README.md         # Documentation for the Basic sample
```

## Next steps

- Try modifying the schema to support additional fields and functions.
- Integrate FunQL into your own data access layer or service.
- Explore the [WebApi sample](../WebApi)  to see FunQL in a REST API context.

For more details, check out the [FunQL .NET documentation](https://dotnet.funql.io/).