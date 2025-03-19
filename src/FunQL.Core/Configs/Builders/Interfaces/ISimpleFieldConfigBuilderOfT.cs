// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>
/// Builder interface for building the <see cref="IFieldConfig"/> for simple types of type <typeparamref name="T"/>.
/// </summary>
/// <inheritdoc cref="IFieldConfigBuilder{T}"/>
public interface ISimpleFieldConfigBuilder<T> : IFieldConfigBuilder<T>, ISimpleFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasName"/>
    public new ISimpleFieldConfigBuilder<T> HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.HasType"/>
    public new ISimpleFieldConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder{T}.IsNullable"/>
    public new ISimpleFieldConfigBuilder<T> IsNullable(bool nullable = true);
}