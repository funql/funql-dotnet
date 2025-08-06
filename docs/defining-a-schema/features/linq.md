# LINQ

TODO:

- Explain `.AddLinqFeature(bool withLinqExecutionHandlers = true)` to add the LINQ feature
    - Adds all services used to translate FunQL requests to LINQ expressions
    - Also adds LINQ execution handlers to execute feature (if execute feature added) by default (see `withLinqExecutionHandlers` boolean)
    - Explain LINQ execution handlers, see `FunQL.Linq.Schemas.Configs.Execute.Builders.Extensions.ExecuteConfigBuilderExtensions.WithLinqExecutionHandlers()`
- Explain `.AddLinqFeature(config => { }, bool withLinqExecutionHandlers = true)`
    - Same as `.AddLinqFeature()` but with optional action to configure the feature
- Once LINQ feature added, you can use:
    - `IQueryable.ApplyFilter()` to apply FunQL `Filter` to given `IQueryable`
    - `IQueryable.ApplySort()` to apply FunQL `Sort` to given `IQueryable`
    - `IQueryable.ApplySkip()` to apply FunQL `Skip` to given `IQueryable`
    - `IQueryable.ApplyLimit()` to apply FunQL `Limit` to given `IQueryable`
    - `IQueryable.ApplyRequest()` to apply FunQL `Filter`, `Sort`, `Skip`, and `Limit` to given `IQueryable` in correct 
    order, as order is important when applying these parameters
    - `IQueryable.ExecuteRequest(Schema schema, string request)` extension. This is explained in `executing-queries/index.md`
    - `IQueryable.ExecuteRequestForParameters(Schema schema, string requestName, string? filter = null, string? sort = null)`
      extension. This is explained in `executing-queries/index.md`
    - Should it be shown here as well? It's also explained in `executing-queries/index.md` as executing is one of the key
      features, so has a dedicated page
    - Note that the `.ApplyXyz()` methods should probably not be used directly, it's recommended to use the execution pipeline instead
- Explain how to add translators, like the translator for NodaTime's Instant (see `integrations/nodatime.md`)?