// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for a field: Named value in an object.</summary>
public interface IFieldConfig : IExtensibleConfig
{
    /// <summary>Name of this field.</summary>
    public string Name { get; }

    /// <summary>Config of the type of this field.</summary>
    public ITypeConfig TypeConfig { get; }

    /// <summary>
    /// Optional <see cref="MemberInfo"/> in case this field represents a member (property) of an existing CLR type.
    /// </summary>
    public MemberInfo? MemberInfo { get; }
}