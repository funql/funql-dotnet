# Print

- Explain `.AddPrintFeature()` to add the print feature
    - Adds all services used to print FunQL AST
- Explain `.AddPrintFeature(config => { })`
    - Same as `.AddPrintFeature()` but with optional action to configure the feature
- Print feature is used to print a FunQL query (AST)
- There's no extension to actually print the query (yet), but the services are there