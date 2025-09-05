# Newtonsoft.Json

FunQL uses [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) 
by default for JSON serialization. However, if your project requires [Newtonsoft.Json](https://www.newtonsoft.com/json) 
(JSON.NET), FunQL can seamlessly integrate it for both serialization and deserialization.

This section explains how to integrate Newtonsoft.Json with FunQL for both parsing (deserializing) and printing 
(serializing) constants.

## Configuring deserialization

To parse FunQL constants (e.g., `true`, `"name"`, `123`, or objects/arrays) using Newtonsoft.Json, we will need to 
override the default `IConstantParser`. First, we create a parser that uses Newtonsoft.Json, and then we configure our 
schema to use it.

### 1. Create the parser

Implement a custom `IConstantParser` that uses `JsonConvert.DeserializeObject()` to deserialize JSON strings into FunQL 
constants.

```csharp
/// <summary>
/// Implementation of <see cref="IConstantParser"/> that uses <see cref="JsonConvert"/> to parse the
/// <see cref="Constant"/> node.
/// </summary>
/// <param name="jsonSerializerSettings">Settings for <see cref="Newtonsoft.Json"/>.</param>
/// <inheritdoc/>
public class NewtonsoftJsonConstantParser(
    JsonSerializerSettings jsonSerializerSettings
) : IConstantParser
{
    /// <summary>Options for <see cref="Newtonsoft.Json"/>.</summary>
    private readonly JsonSerializerSettings _jsonSerializerSettings = jsonSerializerSettings;

    /// <inheritdoc/>
    public Constant ParseConstant(IParserState state)
    {
        state.IncreaseDepth();

        var expectedType = state.RequireContext<ConstantParseContext>().ExpectedType;
        // If Type is a primitive/struct (int, double, DateTime, etc.), we should make it Nullable as 'null' is a valid
        // constant, but JsonConvert can only read 'null' if Type can be null
        expectedType = expectedType.ToNullableType();

        var token = state.CurrentToken();
        switch (token.Type)
        {
            case TokenType.String:
            case TokenType.Number:
            case TokenType.Boolean:
            case TokenType.Null:
            case TokenType.Object:
            case TokenType.Array:
                // Valid token for constants
                break;
            case TokenType.OpenBracket:
                // Handle OpenBracket token as Array 
                token = state.CurrentTokenAsArray();
                break;
            case TokenType.None:
            case TokenType.Eof:
            case TokenType.Identifier:
            case TokenType.OpenParen:
            case TokenType.CloseParen:
            case TokenType.Comma:
            case TokenType.Dot:
            case TokenType.Dollar:
            case TokenType.CloseBracket:
            default:
                // Invalid token for constants
                throw state.Lexer.SyntaxException($"Expected constant at position {token.Position}, but found '{token.Text}'.");
        }

        var metadata = state.CreateMetadata();

        try
        { 
            var value = JsonConvert.DeserializeObject(token.Text, expectedType, _jsonSerializerSettings);
            // Successfully parsed, so go to next token
            state.NextToken();

            state.DecreaseDepth();
            return new Constant(value, metadata);
        }
        catch (Exception e) when (e is JsonException)
        {
            throw new ParseException($"Failed to parse constant '{token.Text}' at position {token.Position}.", e);
        }
    }
}
```

!!! note

    This code is based on the [default implementation](
    https://github.com/funql/funql-dotnet/blob/main/src/FunQL.Core/Constants/Parsers/JsonConstantParser.cs), adapted to 
    use Newtonsoft.Json instead of System.Text.Json.

### 2. Configure Schema

To use the `NewtonsoftJsonConstantParser`, override the default `IConstantParser` in your schema's `OnInitializeSchema` 
method.

```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {
        schema.AddParseFeature(it =>
        {
            IConstantParser? constantParser = null;
            it.MutableConfig.ConstantParserProvider = _ => constantParser ??= new NewtonsoftJsonConstantParser(
                new JsonSerializerSettings()
            );
        });
    }
}
```

Now, when you parse a FunQL query, the `NewtonsoftJsonConstantParser` will be used to parse the constants. You can also 
configure the `JsonSerializerSettings` as needed, adding custom converters, naming strategies, etc.

## Configuring serialization

When printing FunQL queries to a string, the `Constant` nodes are serialized to JSON using the `IConstantPrintVisitor`. 
First, we implement this class to use Newtonsoft.Json, and then we configure the schema to use this implementation.

### 1. Create the print visitor

The `NewtonsoftJsonConstantPrintVisitor` serializes FunQL constants into JSON strings using 
`JsonConvert.SerializeObject()`.

````csharp
/// <summary>Implementation of <see cref="IConstantPrintVisitor{TState}"/> using <see cref="JsonConvert"/>.</summary>
/// <param name="jsonSerializerSettings">Settings for <see cref="Newtonsoft.Json"/>.</param>
/// <inheritdoc cref="IConstantPrintVisitor{TState}"/>
public class NewtonsoftJsonConstantPrintVisitor<TState>(
    JsonSerializerSettings jsonSerializerSettings
) : ConstantVisitor<TState>, IConstantPrintVisitor<TState> where TState : IPrintVisitorState
{
    /// <summary>Settings to use when writing JSON.</summary>
    private readonly JsonSerializerSettings _jsonSerializerSettings = jsonSerializerSettings;

    /// <inheritdoc/>
    public override Task Visit(Constant node, TState state, CancellationToken cancellationToken) =>
        state.OnVisit(node, async ct =>
        {
            var jsonValue = JsonConvert.SerializeObject(node.Value, _jsonSerializerSettings);
            await state.Write(jsonValue, ct);
        }, cancellationToken);
}
````

!!! note

    This code is based on the [default implementation](
    https://github.com/funql/funql-dotnet/blob/main/src/FunQL.Core/Constants/Visitors/JsonConstantPrintVisitor.cs), 
    adapted to use Newtonsoft.Json instead of System.Text.Json.

### 2. Configure Schema

To use the `NewtonsoftJsonConstantPrintVisitor`, override the default `IConstantPrintVisitor` in your schema's 
`OnInitializeSchema` method.

```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {
        schema.AddPrintFeature(it =>
        {
            IConstantPrintVisitor<IPrintVisitorState>? constantPrintVisitor = null;
            it.MutableConfig.ConstantPrintVisitorProvider = _ => 
                constantPrintVisitor ??= new NewtonsoftJsonConstantPrintVisitor<IPrintVisitorState>(
                    new JsonSerializerSettings()
                );
        });
    }
}
```

Now, when you print a FunQL query, the `NewtonsoftJsonConstantPrintVisitor` will be used to serialize the constants.