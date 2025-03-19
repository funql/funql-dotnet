// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="ISimpleTypeConfig"/> for type <typeparamref name="T"/>.
/// </summary>
/// <inheritdoc cref="ITypeConfigBuilder{T}"/>
public interface ISimpleTypeConfigBuilder<T> : ITypeConfigBuilder<T>, ISimpleTypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder{T}.HasType"/>
    public new ISimpleTypeConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder{T}.IsNullable"/>
    public new ISimpleTypeConfigBuilder<T> IsNullable(bool nullable = true);
}