# NodaTime

When you use [Noda Time](https://nodatime.org/) as your date and time API, FunQL's DateTime functions (like `year()`, 
`month()`, `day()`) will not work out of the box. Fortunately, FunQL is highly extendable and allows for adding this 
functionality yourself.

## Configuration

### 1. Create Instant Translator

Implement a custom `FieldFunctionLinqTranslator` that converts NodaTime's `Instant` to .NET's `DateTime` for FunQL 
operations:

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

### 2. Add Extension Method

Add an extension method to easily configure FunQL with NodaTime support:

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

When configuring your FunQL schema, add NodaTime support:

```csharp
public sealed class ApiSchema : Schema { 
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema) {         
        schema.AddLinqFeature(it =>
        {
            // Add DateTime function support to NodaTime Instant
            it.WithInstantFunctionLinqTranslator();
        });
    }
}
```

You can now use FunQL's DateTime functions on `Instant` types.