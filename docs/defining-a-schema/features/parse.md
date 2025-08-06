# Parse

TODO:

- Explain `.AddParseFeature()` to add the parse feature
  - Adds all services used to parse FunQL requests
- Explain `.AddParseFeature(config => { })`
  - Same as `.AddParseFeature()` but with optional action to configure the feature 
- Once parse feature added, you can use:
  - `schema.ParseRequest(string request)` extension, which parses a FunQL request using the Schema and configured 
  feature. This would be the QL version, treating FunQL as a query language (_QL_). Perhaps show an example of what it 
  does
  ```funql
  listSets(
    filter(
      gte(price, 500)
    )
  )
  ```
  Becomes
  ```
  var request = new Request(
    "listSets",
    [
        new Filter(
            new GreaterThanOrEqual(
                new FieldPath([new NamedField("price")]),
                new Constant(500)
            )
        )
    ]
  );
  ```
  - `schema.ParseRequestForParameters(string requestName, string? filter = null, string? sort = null)` extension, which 
  parses a FunQL request for given `requestName` using the Schema and configured feature. This would be the REST 
  version where FunQL parameters like `filter` and `sort` are passed as URL query parameters. Perhaps show an example of 
  what it does
  - If feature was not added, calling these methods will throw `InvalidOperationException`