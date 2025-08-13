# NodaTime

[Noda Time](https://nodatime.org/) is a great alternative date and time API for .NET. However, integrating NodaTime with
FunQL requires additional configuration to handle JSON serialization and support FunQL's DateTime functions, such as 
`year()`, `month()`, and `day()`.

You can also refer to the [WebApi sample](https://github.com/funql/funql-dotnet/tree/main/samples/WebApi) for a 
practical example of the NodaTime integration.

## Configuring JSON serialization

To handle NodaTime types (`Instant`, `LocalDate`, `LocalDateTime`), you must configure JSON serialization. FunQL uses 
`System.Text.Json` by default, and the easiest way to add support for NodaTime types is through the 
[NodaTime.Serialization.SystemTextJson](https://nodatime.org/serialization/systemtextjson) library.

### 1. Install the package

Run the following command to add the [NodaTime.Serialization.SystemTextJson](
https://nodatime.org/serialization/systemtextjson) library:

```shell
dotnet add package NodaTime.Serialization.SystemTextJson
```

### 2. Configure JSON for NodaTime

Update your `Schema` to include a custom `JsonSerializerOptions` configuration with NodaTime support:

```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {     
        // Create custom JsonSerializerOptions for FunQL
        var jsonSerializerOptions = new JsonSerializerOptions() 
            // Add support for NodaTime types
            .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        
        // Apply the configured JsonSerializerOptions to FunQL
        schema.JsonConfig()
            .WithJsonSerializerOptions(jsonSerializerOptions);
    }
}
```

This ensures that NodaTime types are correctly serialized and deserialized when interacting with FunQL.

## Supporting DateTime functions

FunQL's built-in DateTime functions (`year()`, `month()`, `day()`) only work with .NET's `DateTime` type by default. To 
enable these functions for NodaTime's `Instant` type, we need to add a custom translator.

### 1. Create Instant translator

Implement a custom `FieldFunctionLinqTranslator` that converts NodaTime's `Instant` to .NET's `DateTime`. This makes the 
FunQL DateTime functions compatible with NodaTime types:

```csharp
/// <summary>Translator for <see cref="Instant"/> functions.</summary>
/// <remarks>
/// Translates <see cref="Instant"/> to <see cref="DateTime"/> and then delegates the translation logic to
/// <see cref="DateTimeFunctionLinqTranslator"/>, so e.g. <see cref="Year"/> and <see cref="Month"/> field functions can
/// be used on <see cref="Instant"/> types.
/// </remarks>
public class InstantFunctionLinqTranslator : FieldFunctionLinqTranslator
{
    /// <summary>Empty <see cref="Instant"/> we can use to get <see cref="MethodInfo"/>.</summary>
    // ReSharper disable once RedundantDefaultMemberInitializer
    private static readonly Instant DefaultInstant = default;
    /// <summary>The <see cref="MethodInfo"/> for <see cref="Instant.ToDateTimeUtc"/>.</summary>
    private static readonly MethodInfo InstantToDateTimeUtcMethod =
        MethodInfoUtil.MethodOf(DefaultInstant.ToDateTimeUtc);

    /// <summary>The <see cref="DateTime"/> translator to delegate translation logic to.</summary>
    private static readonly DateTimeFunctionLinqTranslator DateTimeFunctionLinqTranslator = new();
    
    /// <inheritdoc/>
    public override Expression? Translate(FieldFunction node, Expression source, ILinqVisitorState state)
    {
        if (source.Type.UnwrapNullableType() != typeof(Instant))
            return null;

        // Translate Instant to DateTime so we can use DateTime methods instead
        source = LinqExpressionUtil.CreateFunctionCall(
            InstantToDateTimeUtcMethod,
            state.HandleNullPropagation,
            source
        );

        return DateTimeFunctionLinqTranslator.Translate(node, source, state);
    }
}
```

### 2. Add extension method

To simplify adding NodaTime support to FunQL, create an extension method for the LINQ configuration:

```csharp
/// <summary>Extensions related to <see cref="ILinqConfigBuilder"/> and <see cref="NodaTime"/>.</summary>
public static class LinqConfigBuilderNodaTimeExtensions
{
    /// <summary>
    /// Adds the <see cref="InstantFunctionLinqTranslator"/> to given <paramref name="builder"/> if not yet added.
    /// </summary>
    /// <param name="builder">Builder to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public static ILinqConfigBuilder WithInstantFunctionLinqTranslator(
        this ILinqConfigBuilder builder
    )
    {
        // Early return if translator already added
        if (builder.MutableConfig.GetFieldFunctionLinqTranslators().Any(it => it is InstantFunctionLinqTranslator))
            return builder;
        
        builder.MutableConfig.AddFieldFunctionLinqTranslator(new InstantFunctionLinqTranslator());

        return builder;
    }
}
```

### 3. Configure Schema

Finally, update your schema to include the custom translator for NodaTime's `Instant`:


```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {         
        schema.AddLinqFeature(it =>
        {
            // Add DateTime function support for NodaTime Instant
            it.WithInstantFunctionLinqTranslator();
        });
    }
}
```

That's it, you can now use FunQL's DateTime functions on `Instant` types.