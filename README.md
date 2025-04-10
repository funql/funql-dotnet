![FunQL logo](https://raw.githubusercontent.com/funql/funql-dotnet/main/assets/logo.png)

# FunQL .NET - API Query Library

[![License: GPLv2/Commercial](https://img.shields.io/badge/license-GPLv2%20or%20Commercial-orange.svg)](https://github.com/funql/funql-dotnet/blob/main/LICENSE)
[![Downloads](https://img.shields.io/nuget/dt/FunQL.Core)](https://www.nuget.org/packages/FunQL)
[![Latest version](https://img.shields.io/nuget/v/FunQL.Core)](https://www.nuget.org/packages/FunQL)
[![Build status](https://github.com/funql/funql-dotnet/workflows/build/badge.svg)](https://github.com/funql/funql-dotnet/actions/workflows/build.yml)

FunQL .NET is the official .NET implementation of [FunQL](https://funql.io/), the open-source Functional Query Language.
Use FunQL .NET to enhance your existing REST API with ready-to-use components for filtering, sorting, pagination, and
more, or build a new FunQL API using the powerful, well-structured, and easy-to-learn FunQL Query Language. To learn
more about FunQL, visit [funql.io](https://funql.io/).

## Packages

| Package      | Latest version                                                                                   | About                                                                                   |
|--------------|--------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| `FunQL`      | [![NuGet](https://img.shields.io/nuget/v/FunQL)](https://www.nuget.org/packages/FunQL)           | Includes both `FunQL.Core` and `FunQL.Linq` for quick setup.                            |
| `FunQL.Core` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Core)](https://www.nuget.org/packages/FunQL.Core) | Provides the core functionality, including parsing, validating, and executing requests. |
| `FunQL.Linq` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Linq)](https://www.nuget.org/packages/FunQL.Linq) | Translates FunQL queries into LINQ expressions.                                         |

## Installation

FunQL .NET is available on [NuGet](https://www.nuget.org/packages/FunQL). Install it via the NuGet Package Manager, or
use the .NET CLI:

```shell
dotnet add package FunQL
```

## Features

- It just works — FunQL .NET processes raw text into functional queries, handling lexical analysis, tokenization, syntax
  parsing, and Abstract Syntax Tree (AST) generation so you don't have to.
- Supports LINQ and EF Core — Translates FunQL queries into LINQ expressions for seamless database interaction with EF
  Core.
- Zero dependencies — Requires only .NET Core; no additional frameworks or external libraries needed.
- Built-in validation — Enforces query parameter constraints, ensuring valid API queries.
- Explicit by default — No hidden behaviors or 'automagic' configurations. FunQL .NET enforces explicit schema
  definitions, giving developers full control and clarity over API behavior.
- Highly extensible — Every component is customizable: override, extend, or replace parts to fit your project's needs.
- Configurable JSON serialization — Works out of the box with `System.Text.Json` and supports external JSON libraries
  like `NewtonSoft.Json` for greater flexibility.

## Samples

The [samples](https://github.com/funql/funql-dotnet/tree/main/samples) directory contains example projects that
demonstrate how to use FunQL .NET in different scenarios.

- [`Basic`](https://github.com/funql/funql-dotnet/tree/main/samples/Basic) - A minimal, self-contained console
  application that demonstrates how to configure a FunQL schema, use LINQ to apply queries, and filter and sort data
  using FunQL query parameters.
- [`WebApi`](https://github.com/funql/funql-dotnet/tree/main/samples/WebApi) - A REST API example that integrates FunQL
  .NET with Entity Framework Core (EF Core), Noda Time, System.Text.Json, and minimal APIs.

## License

- FunQL software is dual-licensed:
    - Under the [GNU GPLv2](https://github.com/funql/funql-dotnet/blob/main/LICENSE-GPL) for open-source use.
    - Under a [commercial license](https://funql.io/code/licensing/) for proprietary, closed-source use.
- FunQL [documentation](https://github.com/funql/funql-dotnet/tree/main/docs) is licensed under [CC BY-SA 4.0](
  https://github.com/funql/funql-dotnet/blob/main/docs/LICENSE).
    - **Note:** Source code examples in the documentation are covered by the same license as FunQL software.
- For full licensing details, refer to:
    - [LICENSE](https://github.com/funql/funql-dotnet/blob/main/LICENSE)
    - [COMPLIANCE](https://github.com/funql/funql-dotnet/blob/main/COMPLIANCE.md)