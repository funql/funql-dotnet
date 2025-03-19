// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="decimal"/> methods.</summary>
public static class DecimalMethodUtil
{
    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Floor(decimal)"/>.</summary>
    public static readonly MethodInfo Floor =
        MethodInfoUtil.MethodOf<decimal, decimal>(Math.Floor);

    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Ceiling(decimal)"/>.</summary>
    public static readonly MethodInfo Ceiling =
        MethodInfoUtil.MethodOf<decimal, decimal>(Math.Ceiling);

    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Round(decimal)"/>.</summary>
    public static readonly MethodInfo Round =
        MethodInfoUtil.MethodOf<decimal, decimal>(Math.Round);
}