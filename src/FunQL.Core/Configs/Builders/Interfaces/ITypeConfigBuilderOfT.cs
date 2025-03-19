// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="ITypeConfig"/> for type <typeparamref name="T"/>.</summary>
/// <typeparam name="T">Type of the type to configure.</typeparam>
public interface ITypeConfigBuilder<T> : ITypeConfigBuilder
{
    /// <inheritdoc cref="ITypeConfigBuilder.HasType"/>
    public new ITypeConfigBuilder<T> HasType(Type type);

    /// <inheritdoc cref="ITypeConfigBuilder.IsNullable"/>
    public new ITypeConfigBuilder<T> IsNullable(bool nullable = true);
}