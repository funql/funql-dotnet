# Visit

- Explain `.AddVisitFeature()` to add the visit feature
    - Adds all services used to visit FunQL AST
- Explain `.AddVisitFeature(config => { })`
    - Same as `.AddVisitFeature()` but with optional action to configure the feature
- Visit feature is mostly used by validate feature to allow for visiting each node of the FunQL AST, so a FunQL request 
can be validated