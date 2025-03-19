// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFieldConfig"/> for simple types.</summary>
public interface ISimpleFieldConfigBuilder : IFieldConfigBuilder
{
    /// <inheritdoc cref="IFieldConfigBuilder.HasName"/>
    public new ISimpleFieldConfigBuilder HasName(string name);

    /// <inheritdoc cref="IFieldConfigBuilder.HasType"/>
    public new ISimpleFieldConfigBuilder HasType(Type type);

    /// <inheritdoc cref="IFieldConfigBuilder.IsNullable"/>
    public new ISimpleFieldConfigBuilder IsNullable(bool nullable = true);
}