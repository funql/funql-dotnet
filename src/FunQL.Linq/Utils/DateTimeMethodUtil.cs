// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="DateTime"/> methods.</summary>
public static class DateTimeMethodUtil
{
    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Year"/>.</summary>
    public static readonly MemberInfo Year =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Year);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Month"/>.</summary>
    public static readonly MemberInfo Month =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Month);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Day"/>.</summary>
    public static readonly MemberInfo Day =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Day);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Hour"/>.</summary>
    public static readonly MemberInfo Hour =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Hour);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Minute"/>.</summary>
    public static readonly MemberInfo Minute =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Minute);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Second"/>.</summary>
    public static readonly MemberInfo Second =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Second);

    /// <summary><see cref="MemberInfo"/> of <see cref="DateTime.Millisecond"/>.</summary>
    public static readonly MemberInfo Millisecond =
        MethodInfoUtil.MemberOf<DateTime, int>(it => it.Millisecond);
}