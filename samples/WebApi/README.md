# FunQL .NET WebApi Sample

This sample demonstrates how to integrate **FunQL .NET** into a **RESTful ASP.NET Core Web API**. It showcases the use
of **Entity Framework Core (EF Core)** for database access, **Noda Time** for advanced date handling,
**System.Text.Json** for serialization, and **minimal APIs** for a lightweight endpoint structure.

## Prerequisites

- .NET SDK 9.0 or later

## Getting started

### 1. Clone the repository

```shell
git clone https://github.com/funql/funql-dotnet.git
cd funql-dotnet/samples/WebApi
```

### 2. Build and run the API

```shell
dotnet run
```

### 3. Test the API

The API exposes endpoints that accept FunQL query parameters. You can test it using tools like **Postman** or **cURL**,
or use the **WebApi.http** file included with the sample project.

#### Example request

```shell
curl "http://localhost:5107/sets?filter=and(has(upper(name),\"STAR%20WARS\"),gte(price,500),gt(year(launchTime),2010))&sort=desc(price)"
```

#### Example response

The response includes all LEGO sets where:

- The name contains "STAR WARS".
- The price is at least 500.
- The launchTime is later than 2010.

```json
[
  {
    "id": 1,
    "name": "LEGO Star Wars Millennium Falcon",
    "setNumber": 75192,
    "pieces": 7541,
    "price": 849.99,
    "launchTime": "2017-10-01T00:00:00Z"
  },
  {
    "id": 2,
    "name": "LEGO Star Wars The Razor Crest",
    "setNumber": 75331,
    "pieces": 6187,
    "price": 599.99,
    "launchTime": "2022-10-03T00:00:00Z"
  }
]
```

## Project structure

```
WebApi/
├── CustomResults/    # Defines custom IResult for FunQL data
├── EFCore/           # Configures Entity Framework Core (EF Core) with a sample in-memory database
├── Endpoints/        # Implements handler for '/sets' endpoint
├── Exceptions/       # Defines exception handler for FunQL exceptions
├── FunQL/            # Configures FunQL schema
│   ├── EFCore/       # Defines custom FunQL execution handler specific to EF Core
│   └── NodaTime/     # Adds FunQL support for NodaTime.Instant, to support DateTime functions (e.g. year(), month(), day())
├── Json/             # Configures System.Text.Json
├── Models/           # Defines the Set data model
├── Properties/       # Default launch settings
├── appsettings.json  # Default application configuration
├── Program.cs        # Entry point, setting up services, middleware, and API routes
└── README.md         # Documentation for the WebApi sample
```

## Next steps

- Extend the API with additional FunQL-compatible endpoints.
- Connect EF Core to an actual SQL server.
- Implement authentication and authorization.

For more details, check out the [FunQL .NET documentation](https://dotnet.funql.io/).