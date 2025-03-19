// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Reflection;

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Mutable version of <see cref="IFieldConfig"/>.</summary>
public interface IMutableFieldConfig : IMutableExtensibleConfig, IFieldConfig
{
    /// <summary>Name of this field.</summary>
    public new string Name { get; set; }

    /// <summary>Config of the type of this field.</summary>
    public new IMutableTypeConfig TypeConfig { get; set; }

    /// <summary>
    /// Optional <see cref="MemberInfo"/> in case this field represents a member (property) of an existing CLR type.
    /// </summary>
    public new MemberInfo? MemberInfo { get; set; }

    /// <inheritdoc cref="IMutableConfig.ToConfig"/>
    public new IFieldConfig ToConfig();
}