// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="MethodInfo"/>.</summary>
public static class MethodInfoUtil
{
    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="expression"/>.</summary>
    public static MethodInfo MethodOf<TReturn>(Expression<Func<object, TReturn>> expression) =>
        MethodOf(expression as Expression);

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="expression"/>.</summary>
    public static MethodInfo MethodOf(Expression expression)
    {
        var lambdaExpression = expression as LambdaExpression;
        Contract.Assert(lambdaExpression != null);
        Contract.Assert(expression.NodeType == ExpressionType.Lambda);
        Contract.Assert(lambdaExpression.Body.NodeType == ExpressionType.Call);
        return (lambdaExpression.Body as MethodCallExpression)!.Method;
    }

    /// <summary>Returns the <see cref="MemberInfo"/> for <paramref name="expression"/>.</summary>
    public static MemberInfo MemberOf<TSource, TProp>(Expression<Func<TSource, TProp>> expression) =>
        (expression.Body as MemberExpression)?.Member
        ?? throw new ArgumentException("Expression is not for a member", nameof(expression));

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T>(Action<T> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2>(Action<T1, T2> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2, T3>(Action<T1, T2, T3> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2, T3, T4>(Action<T1, T2, T3, T4> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T>(Func<T> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T, TResult>(Func<T, TResult> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2, TResult>(Func<T1, T2, TResult> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> fun) => fun.Method;

    /// <summary>Returns the <see cref="MethodInfo"/> for <paramref name="fun"/>.</summary>
    public static MethodInfo MethodOf<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> fun) => fun.Method;
}