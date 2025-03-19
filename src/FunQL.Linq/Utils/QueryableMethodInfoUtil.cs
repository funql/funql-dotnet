// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="Queryable"/> methods.</summary>
public static class QueryableMethodInfoUtil
{
    /// <summary>
    /// <see cref="MethodInfo"/> of <see cref="Queryable.OrderBy{T1,T2}(IQueryable{T1},Expression{Func{T1,T2}})"/>.
    /// </summary>
    public static readonly MethodInfo OrderBy =
        MethodInfoUtil.MethodOf<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
            Queryable.OrderBy
        ).GetGenericMethodDefinition();

    /// <summary>
    /// <see cref="MethodInfo"/> of
    /// <see cref="Queryable.OrderByDescending{T1,T2}(IQueryable{T1},Expression{Func{T1,T2}})"/>.
    /// </summary>
    public static readonly MethodInfo OrderByDescending =
        MethodInfoUtil.MethodOf<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
            Queryable.OrderByDescending
        ).GetGenericMethodDefinition();

    /// <summary>
    /// <see cref="MethodInfo"/> of
    /// <see cref="Queryable.ThenBy{T1,T2}(IOrderedQueryable{T1},Expression{Func{T1,T2}})"/>.
    /// </summary>
    public static readonly MethodInfo ThenBy =
        MethodInfoUtil.MethodOf<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
            Queryable.ThenBy
        ).GetGenericMethodDefinition();

    /// <summary>
    /// <see cref="MethodInfo"/> of
    /// <see cref="Queryable.ThenByDescending{T1,T2}(IOrderedQueryable{T1},Expression{Func{T1,T2}})"/>.
    /// </summary>
    public static readonly MethodInfo ThenByDescending =
        MethodInfoUtil.MethodOf<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(
            Queryable.ThenByDescending
        ).GetGenericMethodDefinition();

    /// <summary>
    /// Invokes <see cref="OrderBy"/> on <paramref name="queryable"/> for <paramref name="keySelector"/>.
    /// </summary>
    public static IOrderedQueryable InvokeOrderBy(IQueryable queryable, LambdaExpression keySelector)
    {
        var typedMethod = OrderBy.MakeGenericMethod(queryable.ElementType, keySelector.Body.Type);
        return (IOrderedQueryable)typedMethod.Invoke(null, [queryable, keySelector])!;
    }

    /// <summary>
    /// Invokes <see cref="OrderByDescending"/> on <paramref name="queryable"/> for <paramref name="keySelector"/>.
    /// </summary>
    public static IOrderedQueryable InvokeOrderByDescending(IQueryable queryable, LambdaExpression keySelector)
    {
        var typedMethod = OrderByDescending.MakeGenericMethod(queryable.ElementType, keySelector.Body.Type);
        return (IOrderedQueryable)typedMethod.Invoke(null, [queryable, keySelector])!;
    }

    /// <summary>
    /// Invokes <see cref="ThenBy"/> on <paramref name="queryable"/> for <paramref name="keySelector"/>.
    /// </summary>
    public static IOrderedQueryable InvokeThenBy(IOrderedQueryable queryable, LambdaExpression keySelector)
    {
        var typedMethod = ThenBy.MakeGenericMethod(queryable.ElementType, keySelector.Body.Type);
        return (IOrderedQueryable)typedMethod.Invoke(null, [queryable, keySelector])!;
    }

    /// <summary>
    /// Invokes <see cref="ThenByDescending"/> on <paramref name="queryable"/> for <paramref name="keySelector"/>.
    /// </summary>
    public static IOrderedQueryable InvokeThenByDescending(IOrderedQueryable queryable, LambdaExpression keySelector)
    {
        var typedMethod = ThenByDescending.MakeGenericMethod(queryable.ElementType, keySelector.Body.Type);
        return (IOrderedQueryable)typedMethod.Invoke(null, [queryable, keySelector])!;
    }
}