// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;
using System.Text.RegularExpressions;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="string"/> methods.</summary>
public static class StringMethodUtil
{
    /// <summary>String we can use to get its method's <see cref="MethodInfo"/> for.</summary>
    private const string DefaultString = "";

    /// <summary><see cref="MethodInfo"/> of <see cref="string.Contains(string)"/>.</summary>
    public static readonly MethodInfo Contains =
        MethodInfoUtil.MethodOf<string, bool>(DefaultString.Contains);

    /// <summary><see cref="MethodInfo"/> of <see cref="string.StartsWith(string)"/>.</summary>
    public static readonly MethodInfo StartsWith =
        MethodInfoUtil.MethodOf<string, bool>(DefaultString.StartsWith);

    /// <summary><see cref="MethodInfo"/> of <see cref="string.EndsWith(string)"/>.</summary>
    public static readonly MethodInfo EndsWith =
        MethodInfoUtil.MethodOf<string, bool>(DefaultString.EndsWith);

    /// <summary><see cref="MethodInfo"/> of <see cref="string.ToLower()"/>.</summary>
    public static readonly MethodInfo ToLower =
        MethodInfoUtil.MethodOf(DefaultString.ToLower);

    /// <summary><see cref="MethodInfo"/> of <see cref="string.ToUpper()"/>.</summary>
    public static readonly MethodInfo ToUpper =
        MethodInfoUtil.MethodOf(DefaultString.ToUpper);

    /// <summary><see cref="MethodInfo"/> of <see cref="Regex.IsMatch(string)"/>.</summary>
    public static readonly MethodInfo RegexIsMatch =
        MethodInfoUtil.MethodOf<string, string, bool>(Regex.IsMatch);

    /// <summary><see cref="MethodInfo"/> of <see cref="string.Compare(string?,string?)"/>.</summary>
    public static readonly MethodInfo StringCompare =
        MethodInfoUtil.MethodOf<string?, string?, int>(string.Compare);
}