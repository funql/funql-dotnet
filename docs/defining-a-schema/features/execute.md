# Execute

TODO: 

- Explain `.AddExecuteFeature(bool withCoreExecutionHandlers = true)` to add the execute feature
    - Adds all services used to execute FunQL requests
    - Also adds core execution handlers by default (see `withCoreExecutionHandlers` boolean)
    - Explain core execution handlers, see `FunQL.Core.Schemas.Configs.Execute.Builders.Extensions.WithCoreExecutionHandlers`
- Explain `.AddExecuteFeature(config => { }, bool withCoreExecutionHandlers = true)`
    - Same as `.AddExecuteFeature()` but with optional action to configure the feature
- Once execute feature added, you can use:
  - `IQueryable.ExecuteRequest(Schema schema, string request)` extension. This is explained in `executing-queries/index.md`
  - `IQueryable.ExecuteRequestForParameters(Schema schema, string requestName, string? filter = null, string? sort = null)` 
  extension. This is explained in `executing-queries/index.md`
  - Should it be shown here as well? It's also explained in `executing-queries/index.md` as executing is one of the key 
  features, so has a dedicated page
- Explain about the execution pipeline? Again, there's also `executing-queries/pipeline.md`, so not sure if it should be explained here
- Explain how to add steps to the pipeline?