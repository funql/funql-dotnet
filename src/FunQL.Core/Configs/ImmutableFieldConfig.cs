// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using System.Reflection;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Immutable implementation of <see cref="IFieldConfig"/>.</summary>
/// <param name="Name">Name of this field.</param>
/// <param name="TypeConfig">Config of the type of this field.</param>
/// <param name="MemberInfo">
/// Optional <see cref="MemberInfo"/> in case this field represents a member (property) of an existing CLR type.
/// </param>
/// <param name="Extensions"><inheritdoc cref="ImmutableExtensibleConfig"/></param>
public sealed record ImmutableFieldConfig(
    string Name,
    ITypeConfig TypeConfig,
    MemberInfo? MemberInfo,
    IImmutableDictionary<string, IConfigExtension> Extensions
) : ImmutableExtensibleConfig(Extensions), IFieldConfig;