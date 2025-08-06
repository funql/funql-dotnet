# Validate

TODO:

- Explain `.AddValidateFeature(bool addVisitFeature = true, bool withCoreRules = true)` to add the validate feature
    - Adds all services used to validate FunQL requests
    - Also adds `.AddVisitFeature()` by default (see `addVisitFeature` boolean)
    - Also adds core validation rules by default (see `withCoreRules` boolean)
    - Explain core rules, see `FunQL.Core.Schemas.Configs.Validate.Builders.Extensions.ValidateConfigBuilderExtensions`
- Explain `.AddValidateFeature(config => { }, bool addVisitFeature = true, bool withCoreRules = true)`
    - Same as `.AddValidateFeature()` but with optional action to configure the feature
- Once validate feature added, you can use:
    - `schema.ValidateRequest(Request request)` extension, which validates a FunQL request using the Schema and configured
      feature
      - Throws `ValidationException` on validation errors
- Explain how to add validation rules
  - Implement `IValidationRule` or `AbstractValidationRule<T>` for simple validators
    - Basic example:
      public sealed class SkipHasIntConstant : AbstractValidationRule<Skip>
      {
      /// <inheritdoc/>
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
  - Use `CompositeValidationRule` for more complex validation rules, combining multiple validation rules
  - Add the rule via `.AddValidateFeature(config => { config.WithValidationRule(new Rule()) })`