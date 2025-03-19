// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IObjectFieldConfigBuilder"/>.</summary>
/// <inheritdoc cref="FieldConfigBuilder"/>
public sealed class ObjectFieldConfigBuilder(
    IMutableFieldConfig mutableConfig
) : FieldConfigBuilder(mutableConfig), IObjectFieldConfigBuilder
{
    /// <summary>
    /// Cached builder for the field's <see cref="IMutableFieldConfig.TypeConfig"/> to avoid recreating that builder.
    /// </summary>
    private IObjectTypeConfigBuilder? _currentTypeConfigBuilder;

    /// <inheritdoc cref="IObjectFieldConfigBuilder.HasName"/>
    public override IObjectFieldConfigBuilder HasName(string name) => (IObjectFieldConfigBuilder)base.HasName(name);

    /// <inheritdoc cref="IObjectFieldConfigBuilder.HasType"/>
    public override IObjectFieldConfigBuilder HasType(Type type) => (IObjectFieldConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="IObjectTypeConfigBuilder.IsNullable"/>
    public override IObjectFieldConfigBuilder IsNullable(bool nullable = true) =>
        (IObjectFieldConfigBuilder)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleFieldConfigBuilder SimpleField(string name, Type type) =>
        GetTypeConfigBuilder().SimpleField(name, type);

    /// <inheritdoc/>
    public IObjectFieldConfigBuilder ObjectField(string name, Type type) =>
        GetTypeConfigBuilder().ObjectField(name, type);

    /// <inheritdoc/>
    public IListFieldConfigBuilder ListField(string name, Type type) =>
        GetTypeConfigBuilder().ListField(name, type);

    /// <summary>
    /// Returns the cached <see cref="IObjectTypeConfigBuilder"/>, creating it if it does not exist yet.
    /// </summary>
    private IObjectTypeConfigBuilder GetTypeConfigBuilder()
    {
        if (MutableConfig.TypeConfig is not IMutableObjectTypeConfig objectTypeConfig)
            throw new InvalidOperationException("TypeConfig must be of type IMutableObjectTypeConfig");

        if (_currentTypeConfigBuilder?.MutableConfig != MutableConfig.TypeConfig)
            _currentTypeConfigBuilder = new ObjectTypeConfigBuilder(objectTypeConfig);

        return _currentTypeConfigBuilder;
    }
}