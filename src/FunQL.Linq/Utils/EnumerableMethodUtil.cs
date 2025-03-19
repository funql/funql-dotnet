// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="Enumerable"/> methods.</summary>
public static class EnumerableMethodUtil
{
    /// <summary><see cref="MethodInfo"/> of <see cref="Enumerable.All{T}"/>.</summary>
    public static readonly MethodInfo All =
        MethodInfoUtil.MethodOf<IEnumerable<object>, Func<object, bool>, bool>(Enumerable.All)
            .GetGenericMethodDefinition();

    /// <summary><see cref="MethodInfo"/> of <see cref="Enumerable.Any{T}(IEnumerable{T},Func{T,bool})"/>.</summary>
    public static readonly MethodInfo Any =
        MethodInfoUtil.MethodOf<IEnumerable<object>, Func<object, bool>, bool>(Enumerable.Any)
            .GetGenericMethodDefinition();

    /// <summary><see cref="MethodInfo"/> of <see cref="Enumerable.Select{T1,T2}(IEnumerable{T1},Func{T1,T2})"/>.</summary>
    public static readonly MethodInfo Select =
        MethodInfoUtil.MethodOf<IEnumerable<object>, Func<object, object>, IEnumerable<object>>(Enumerable.Select)
            .GetGenericMethodDefinition();
}