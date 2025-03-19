# FunQL .NET design philosophy

Before you start using FunQL .NET, it's helpful to understand the design principles behind it. Our goal is to provide
developers with the right tools to build powerful, flexible APIs in a way that's both intuitive and easy to use.

To learn more about FunQL, visit [funql.io](https://funql.io/).

## Everything you would expect

FunQL .NET is designed with simplicity and efficiency in mind. We focus on providing the core functionalities that are
essential to most API workflows while ensuring a smooth and seamless developer experience.

<div class="grid cards" markdown>

-   :material-check-decagram:{ .lg .middle .primary-fg } &nbsp; **It just works**

    ---

    FunQL .NET processes raw text into functional queries, handling lexical analysis, tokenization, syntax parsing, and
    Abstract Syntax Tree (AST) generation **so you don't have to**.

-   :material-database:{ .lg .middle .primary-fg } &nbsp; **Supports LINQ and EF Core**

    ---

    Translates FunQL queries into LINQ expressions for seamless database interaction with EF Core.

-   :material-feather:{ .lg .middle .primary-fg } &nbsp; **Zero dependencies**

    ---

    Requires only .NET Core; no additional frameworks or external libraries needed.

-   :material-clipboard-check:{ .lg .middle .primary-fg } &nbsp; **Built-in validation**

    ---

    Enforces query parameter constraints, ensuring valid API queries.

</div>

## Built for developers, by developers

FunQL .NET is built with developers in mind. We believe in keeping things transparent, customizable, and flexible.
It's designed to be simple, yet adaptable to meet the unique needs of any project.

<div class="grid cards" markdown>

-   :material-cog:{ .lg .middle .primary-fg } &nbsp; **Explicit by default**

    ---

    No hidden behaviors or 'automagic' configurations. FunQL .NET enforces explicit schema definitions, **giving
    developers full control** and clarity over API behavior.

-   :material-toy-brick:{ .lg .middle .primary-fg } &nbsp; **Highly extensible**

    ---

    Every component is customizable: override, extend, or replace parts to fit your project's needs.

-   :material-code-json:{ .lg .middle .primary-fg } &nbsp; **Configurable JSON serialization**

    ---

    Works out of the box with `System.Text.Json` and supports external JSON libraries like `NewtonSoft.Json` for greater
    flexibility.

-   :material-code-tags:{ .lg .middle .primary-fg } &nbsp; **Easy integration**

    ---

    Install via NuGet for a seamless setup.

</div>