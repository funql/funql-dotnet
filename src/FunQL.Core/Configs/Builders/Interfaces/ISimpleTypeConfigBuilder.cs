// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="ISimpleTypeConfig"/>.</summary>
public interface ISimpleTypeConfigBuilder : ITypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder.MutableConfig"/>
    public new IMutableSimpleTypeConfig MutableConfig { get; }

    /// <inheritdoc cref="ITypeConfigBuilder.HasType"/>
    public new ISimpleTypeConfigBuilder HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.IsNullable"/>
    public new ISimpleTypeConfigBuilder IsNullable(bool nullable = true);

    /// <inheritdoc cref="ITypeConfigBuilder.Build"/>
    public new ISimpleTypeConfig Build();
}