// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Collections.Immutable;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs;

/// <summary>Default implementation of <see cref="IMutableSimpleTypeConfig"/>.</summary>
/// <inheritdoc cref="MutableTypeConfig"/>
public sealed class MutableSimpleTypeConfig(
    Type type
) : MutableTypeConfig(type), IMutableSimpleTypeConfig
{
    /// <inheritdoc cref="IMutableSimpleTypeConfig.ToConfig"/>
    public override ISimpleTypeConfig ToConfig() => new ImmutableSimpleTypeConfig(
        Type,
        IsNullable,
        Extensions.ToImmutableDictionary(it => it.Name, it => it.ToConfig())
    );
}