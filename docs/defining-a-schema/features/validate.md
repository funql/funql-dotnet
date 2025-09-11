# Validate

The validate feature ensures that FunQL requests comply with your schema's rules. It catches invalid queries early, 
preventing them from being executed.

This page explains how to enable validation, how to validate requests, and how to customize validation.

## Adding the feature

The `AddValidateFeature()` method registers all services required for validating FunQL requests. Use this to set up the
validate feature in your schema:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddValidateFeature();
    }
}
```

This sets up the validate feature with default configurations and the core validation rules. See [Advanced
configuration](#advanced-configuration) on how to customize the feature.

## Validating requests

Once the validate feature is added, you can validate a FunQL `Request` using:

- **`schema.ValidateRequest(Request request)`**: Traverses the AST and runs the configured validation rules, throwing 
  `ValidationException` on validation errors.

```csharp
var schema = new ApiSchema();
var request = schema.ParseRequest("listSets()");

try
{
    schema.ValidateRequest(request);
    // Valid request, continue to execution
}
catch (ValidationException e)
{
    // Handle validation errors
    // ValidationException.Errors contains the collected validation 
    // errors (message, location, etc.)
    Console.WriteLine("Validation failed:");
    foreach (var error in e.Errors)
    {
        Console.WriteLine($"- {error.Message}");
    }
}
```

!!! tip

    Use the [execute feature](execute.md) to simplify query processing. It automates essential steps like parsing, 
    validation, LINQ translation, and data fetching, allowing you to handle complex FunQL queries with just a single 
    method call.

## Advanced configuration

The `AddValidateFeature()` method has three optional arguments:

- `action`: An action to customize the feature.
- `addVisitFeature`: Whether to add the [visit feature](visit.md). Default `true`. _This is required by the default 
  `IValidator` implementation, so only disable if you are implementing your own validator._
- `withCoreRules`: Whether to add the core validation rules. Default `true`.

For example, disable the core validation rules and add your own:

```csharp
public sealed class ApiSchema : Schema
{
    protected override void OnInitializeSchema(ISchemaConfigBuilder schema)
    {
        schema.AddValidateFeature(config =>
        {
            // Customize the feature here
            config.WithValidationRule(new MyCustomValidationRule());
        }, withCoreRules: false);
    }
}
```

Use this to tailor the feature to your needs.

### Custom validation rules

You can extend the validation logic by adding your own rules. To get started:

- Inherit from `AbstractValidationRule<T>` for simple validators.
- Use `CompositeValidationRule` to group multiple rules.
- Implement `IValidationRule` for full control.

The following example shows how to validate that `Skip` nodes have an integer constant:

```csharp
public sealed class SkipHasIntConstant : AbstractValidationRule<Skip>
{
    public override Task ValidateOnEnter(Skip node, IValidatorState state, CancellationToken cancellationToken)
    {
        if (node.Constant.Value is int)
            return Task.CompletedTask;

        // Not an int, so error
        state.AddError(new ValidationError($"'{Skip.FunctionName}' value must be an integer.", node.Constant));

        // Can't validate invalid value, so stop validation
        return Task.FromException(new ValidationException(state.Errors));
    }
}
```

Register it by calling `#!csharp config.WithValidationRule(new SkipHasIntConstant())` inside the configuration action.

**Complex rules:**

For more complex scenarios (e.g., rules that depend on parent nodes), combine rules via `CompositeValidationRule` and 
use a shared context to communicate between them. 

As an example, the following rule validates that there are no `FieldFunction` nodes used when inside a `Sort` node:

```csharp
public sealed class SortHasNoFieldFunctions() : CompositeValidationRule(
    new SortValidationRule(),
    new FieldFunctionValidationRule()
) {
    // Context shared by rules
    private sealed class Context : IValidateContext;
    
    // Sort rule enters the Context for nested rules to use
    private sealed class SortValidationRule : AbstractValidationRule<Sort>
    {
        public override Task ValidateOnEnter(Sort node, IValidatorState state, CancellationToken cancellationToken)
        {
            state.EnterContext(new Context());
            return Task.CompletedTask;
        }

        public override Task ValidateOnExit(Sort node, IValidatorState state, CancellationToken cancellationToken)
        {
            // Make sure to exit context when done
            state.ExitContext();
            return Task.CompletedTask;
        }
    }
    
    // FieldFunction rule adds error when Context is found
    private sealed class FieldFunctionValidationRule : AbstractValidationRule<FieldFunction>
    {
        public override Task ValidateOnEnter(FieldFunction node, IValidatorState state, CancellationToken cancellationToken)
        {
            var context = state.FindContext<Context>();
            // If context is found, it means we're inside a Sort node, so 
            // add the error
            if (context != null) 
            {
                state.AddError(new ValidationError(
                    $"Field functions are not supported when sorting.",
                    node
                ));
            }

            return Task.CompletedTask;
        }
    }
}
```

## What's next

With the `Request` fully validated, it's time to fetch the data:

- [Learn more about the execute feature →](execute.md)