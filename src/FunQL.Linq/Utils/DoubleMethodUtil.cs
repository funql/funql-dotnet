// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="double"/> methods.</summary>
public static class DoubleMethodUtil
{
    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Floor(double)"/>.</summary>
    public static readonly MethodInfo Floor =
        MethodInfoUtil.MethodOf<double, double>(Math.Floor);

    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Ceiling(double)"/>.</summary>
    public static readonly MethodInfo Ceiling =
        MethodInfoUtil.MethodOf<double, double>(Math.Ceiling);

    /// <summary><see cref="MethodInfo"/> of <see cref="Math.Round(double)"/>.</summary>
    public static readonly MethodInfo Round =
        MethodInfoUtil.MethodOf<double, double>(Math.Round);
}