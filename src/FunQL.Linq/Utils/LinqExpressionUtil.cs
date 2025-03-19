// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using System.Reflection;
using FunQL.Core.Common.Extensions;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to LINQ <see cref="Expression"/>.</summary>
public static class LinqExpressionUtil
{
    /// <summary><see cref="Expression.Constant(object?)"/> of <c>0</c>.</summary>
    public static readonly Expression ZeroConstant = Expression.Constant(0);

    /// <summary>Checks if the expression is a <c>null</c> <see cref="ConstantExpression"/>.</summary>
    /// <param name="expression">The expression to check.</param>
    /// <returns>True if the expression is a null constant; otherwise, false.</returns>
    private static bool IsNullConstant(this Expression expression) =>
        expression is ConstantExpression constant && constant.Value == null;

    /// <summary>Converts given <paramref name="expression"/> to given <paramref name="destinationType"/>.</summary>
    /// <param name="expression">The expression to convert.</param>
    /// <param name="destinationType">The target type to convert expression to.</param>
    /// <param name="convertToNullable">
    /// Whether to convert to <see cref="Nullable{T}"/> if source and destination types differ in nullability. Default
    /// true.
    /// </param>
    /// <returns>
    /// A new <see cref="Expression"/> of the given <paramref name="destinationType"/>, or the original
    /// <paramref name="expression"/> if no conversion necessary.
    /// </returns>
    public static Expression ConvertToType(
        this Expression expression,
        Type destinationType,
        bool convertToNullable = true
    )
    {
        // If the types already match, no conversion is needed
        if (expression.Type == destinationType)
            return expression;

        // If the expression represents a null constant, create a new null constant of the target type
        if (expression.IsNullConstant())
            return Expression.Constant(null, destinationType.ToNullableType());

        // If types differ only by nullability, convert to the nullable type
        if (convertToNullable && expression.Type.UnwrapNullableType() == destinationType.UnwrapNullableType())
            return Expression.Convert(expression, destinationType.ToNullableType());

        // Convert directly to the destination type
        return Expression.Convert(expression, destinationType);
    }

    /// <summary>
    /// Ensures that a <c>null</c> <see cref="ConstantExpression"/> is of the given <paramref name="type"/>. If the
    /// expression is not a <c>null</c> <see cref="ConstantExpression"/>, the original expression is returned.
    /// </summary>
    /// <param name="expression">The expression to check and convert if necessary.</param>
    /// <param name="type">The type to which the null constant should be converted.</param>
    /// <returns>
    /// A new <c>null</c> <see cref="ConstantExpression"/> of given <paramref name="type"/> if necessary; otherwise, the
    /// original <paramref name="expression"/>.
    /// </returns>
    /// <remarks>Value types will be converted to <see cref="Nullable{T}"/>.</remarks>
    public static Expression EnsureTypedNullConstant(this Expression expression, Type type) =>
        expression.IsNullConstant() && expression.Type != type
            ? Expression.Constant(null, type.ToNullableType())
            : expression;

    /// <summary>
    /// Ensures that given <paramref name="expression"/> has a <see cref="Nullable{T}"/> type if necessary. If the type
    /// is already nullable, the original <paramref name="expression"/> is returned.
    /// </summary>
    /// <param name="expression">The expression to convert.</param>
    /// <returns>
    /// A new <see cref="Expression"/> converting the value to <see cref="Nullable{T}"/> if necessary; otherwise, the
    /// original <paramref name="expression"/>.
    /// </returns>
    public static Expression EnsureNullableType(this Expression expression) =>
        !expression.Type.IsNullableType()
            ? Expression.Convert(expression, expression.Type.ToNullableType())
            : expression;

    #region NullPropagation

    /// <summary><see cref="Expression.Constant(object?)"/> of <c>null</c>.</summary>
    private static readonly Expression NullConstant = Expression.Constant(null);

    /// <summary>
    /// Creates an <see cref="Expression"/> to invoke the function for given <paramref name="member"/>: Either calls a
    /// method or accesses a member (field or property).
    /// </summary>
    /// <param name="member">The member to call or access.</param>
    /// <param name="handleNullPropagation">If true, handles null propagation for nullable arguments.</param>
    /// <param name="arguments">The arguments for the method or member access.</param>
    /// <returns>The <see cref="Expression"/> representing the method call or member access.</returns>
    /// <exception cref="ArgumentException">Thrown if an unsupported member type is provided.</exception>
    public static Expression CreateFunctionCall(
        MemberInfo member,
        bool handleNullPropagation,
        params Expression[] arguments
    )
    {
        // Convert Nullable<T> to Nullable<T>.Value as none of the functions (e.g. Math.Round(double), DateTime.Year)
        // take a Nullable<T> argument
        var functionArguments = UnwrapNullableArguments(arguments).ToList();

        Expression functionCall = member switch
        {
            MethodInfo methodInfo => CreateMethodCall(methodInfo, functionArguments),
            FieldInfo fieldInfo => CreateMemberAccess(fieldInfo, functionArguments),
            PropertyInfo propertyInfo => CreateMemberAccess(propertyInfo, functionArguments),
            _ => throw new ArgumentException($"Unsupported member type: {member.GetType().Name}.", nameof(member))
        };

        return handleNullPropagation
            ? CreateNullPropagatingFunctionCall(functionCall, arguments)
            : functionCall;
    }

    /// <summary>
    /// Creates a <see cref="MethodCallExpression"/> that calls given <paramref name="methodInfo"/> for given
    /// <paramref name="arguments"/>.
    /// </summary>
    /// <remarks>
    /// Creates an expression similar to:
    /// 
    /// <code>
    /// arg0.SomeFunction(arg1)
    /// StaticClass.SomeFunction(arg0, arg1)
    /// </code>
    /// </remarks>
    private static MethodCallExpression CreateMethodCall(
        MethodInfo methodInfo,
        IReadOnlyList<Expression> arguments
    ) => methodInfo.IsStatic
        // Static methods have to be called with null instance
        ? Expression.Call(null, methodInfo, arguments)
        : Expression.Call(arguments[0], methodInfo, arguments.Skip(1));

    /// <summary>
    /// Creates a <see cref="MemberExpression"/> that accesses the <paramref name="memberInfo"/>, either a field or a
    /// property from given object instance argument.
    /// </summary>
    /// <remarks>
    /// Creates an expression similar to:
    /// 
    /// <code>
    /// arg0.SomeProperty
    /// </code>
    /// </remarks>
    private static MemberExpression CreateMemberAccess(
        MemberInfo memberInfo,
        IReadOnlyList<Expression> arguments
    )
    {
        if (arguments.Count != 1)
            throw new ArgumentException("Member access requires exactly one argument for the object instance.");

        return Expression.MakeMemberAccess(arguments[0], memberInfo);
    }

    /// <summary>
    /// Converts each <see cref="Nullable{T}"/> argument to access its <see cref="Nullable{T}.Value"/> if applicable.
    /// </summary>
    /// <remarks>
    /// Creates an expression similar to:
    /// 
    /// <code>
    /// arg.Value
    /// </code>
    /// </remarks>
    private static IEnumerable<Expression> UnwrapNullableArguments(IEnumerable<Expression> arguments) =>
        arguments.Select(it =>
            Nullable.GetUnderlyingType(it.Type) != null
                ? Expression.Property(it, "Value")
                : it
        );

    /// <summary>
    /// Wraps given <paramref name="functionCall"/> with null propagation for nullable <paramref name="arguments"/>, or
    /// the original <paramref name="functionCall"/> if null checks are not necessary.
    /// </summary>
    /// <remarks>
    /// Creates an expression similar to:
    /// 
    /// <code>
    /// arg0 == null || arg1 == null
    ///   ? null
    ///   : function(arg0, arg1)
    /// </code>
    /// </remarks>
    private static Expression CreateNullPropagatingFunctionCall(
        Expression functionCall,
        Expression[] arguments
    )
    {
        // If any of the arguments is always null, just return null constant
        if (arguments.Any(arg => arg.IsNullConstant()))
            return Expression.Constant(null, functionCall.Type.ToNullableType());

        var nullChecks = arguments
            .Where(arg => arg.Type.IsNullableType())
            .Select(arg => Expression.Equal(arg, NullConstant))
            .ToList();

        // If there are no null checks necessary, no need to do null propagation
        if (nullChecks.Count == 0)
            return functionCall;

        // Execute the null check and return null if any of the arguments is null
        return Expression.Condition(
            // Test arguments for null: 'arg0 == null || arg1 == null'
            test: nullChecks.Aggregate(Expression.OrElse),
            ifTrue: Expression.Constant(null, functionCall.Type.ToNullableType()),
            ifFalse: EnsureNullableType(functionCall)
        );
    }

    #endregion
}