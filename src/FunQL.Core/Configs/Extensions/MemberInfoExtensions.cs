// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Core.Configs.Extensions;

/// <summary>Extensions related to <see cref="MemberInfo"/>.</summary>
public static class MemberInfoExtensions
{
    /// <summary>Gets the type of the member for given <paramref name="memberInfo"/>.</summary>
    public static Type GetMemberType(this MemberInfo memberInfo) =>
        (memberInfo as PropertyInfo)?.PropertyType ?? ((FieldInfo)memberInfo).FieldType;
}