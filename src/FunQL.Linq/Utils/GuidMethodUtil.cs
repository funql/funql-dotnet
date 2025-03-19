// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Linq.Utils;

/// <summary>Utility methods related to <see cref="Guid"/> methods.</summary>
public static class GuidMethodUtil
{
    /// <summary><see cref="MethodInfo"/> of <see cref="Guid.CompareTo(Guid)"/>.</summary>
    public static readonly MethodInfo CompareTo =
        MethodInfoUtil.MethodOf<Guid, int>(Guid.Empty.CompareTo);
}