# Integrations

FunQL .NET is designed with extensibility at its core, making it highly adaptable to different technologies and 
libraries in the .NET ecosystem. This section covers how popular .NET libraries can be integrated with FunQL.

## Entity Framework Core

FunQL seamlessly integrates with [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/), using `IQueryable`
LINQ expressions that EF Core efficiently translates into database queries. While this integration works out of the box,
you can enhance it further by implementing EF Core-specific optimizations, such as using `CountAsync()` for improved
async performance with FunQL's `count()` parameter.

[Learn more about integrating Entity Framework Core →](efcore.md)

## NodaTime

[Noda Time](https://nodatime.org/) is a great alternative date and time API for .NET. When using NodaTime in your 
project, however, the FunQL DateTime functions (like `year()`, `month()`, `day()`) will not work out of the box. 
Fortunately, you can extend FunQL to add support for DateTime functions on NodaTime's `Instant` type.

[Learn more about integrating NodaTime →](nodatime.md)

## Newtonsoft.Json

FunQL uses [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) 
by default for JSON serialization, as it's built into .NET. For projects requiring [Newtonsoft.Json](
https://www.newtonsoft.com/json) (JSON.NET), FunQL allows for fully customizing the serialization process.

[Learn more about integrating Newtonsoft.Json →](newtonsoftjson.md)
