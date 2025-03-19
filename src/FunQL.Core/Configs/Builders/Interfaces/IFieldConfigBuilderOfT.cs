// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFieldConfig"/> for type <typeparamref name="T"/>.</summary>
/// <typeparam name="T">Type of the field to configure.</typeparam>
public interface IFieldConfigBuilder<T> : IFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder.HasName"/>
    public new IFieldConfigBuilder<T> HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder.HasType"/>
    public new IFieldConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder.IsNullable"/>
    public new IFieldConfigBuilder<T> IsNullable(bool nullable = true);
}