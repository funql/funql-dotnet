# Getting started

In this guide, we will walk you through the basics of installing FunQL .NET.

## Installation

FunQL .NET is available as a [NuGet package](#nuget-packages) and can be easily installed using the
[.NET CLI](#with-dotnet-cli). If you are not familiar with the .NET CLI, we recommend using the
[NuGet Package Manager](#with-nuget).

### NuGet packages

| Package      | Latest version                                                                                   | About                                                                                   |
|--------------|--------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| `FunQL`      | [![NuGet](https://img.shields.io/nuget/v/FunQL)](https://www.nuget.org/packages/FunQL)           | Includes both `FunQL.Core` and `FunQL.Linq` for quick setup.                            |
| `FunQL.Core` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Core)](https://www.nuget.org/packages/FunQL.Core) | Provides the core functionality, including parsing, validating, and executing requests. |
| `FunQL.Linq` | [![NuGet](https://img.shields.io/nuget/v/FunQL.Linq)](https://www.nuget.org/packages/FunQL.Linq) | Translates FunQL queries into LINQ expressions.                                         |

### with .NET CLI <small>recommended</small> { #with-dotnet-cli data-toc-label="with .NET CLI" }

To add the [`FunQL`](https://www.nuget.org/packages/FunQL) package to your project, run the following command in your
terminal from your project directory:

```shell
dotnet add package FunQL
```

!!! note

    This command will install both [`FunQL.Core`](https://www.nuget.org/packages/FunQL.Core) and [`FunQL.Linq`](
    https://www.nuget.org/packages/FunQL.Linq), providing all the essential components to get you started quickly.

### with NuGet { #with-nuget }

To install the [`FunQL`](https://www.nuget.org/packages/FunQL) package in Visual Studio, follow these steps:

1. Select **Project** > **Manage NuGet Packages**.
2. In the **NuGet Package Manager** page, choose **nuget.org** as the **Package source**.
3. From the **Browse** tab, search for *FunQL*, select **FunQL** in the list, and then select **Install**.
4. If you are prompted to verify the installation, select **OK**.

After installation, the [`FunQL`](https://www.nuget.org/packages/FunQL) package will be added to your project's
dependencies.