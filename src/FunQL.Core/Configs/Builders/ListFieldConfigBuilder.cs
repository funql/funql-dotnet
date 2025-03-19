// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IListFieldConfigBuilder"/>.</summary>
/// <inheritdoc cref="FieldConfigBuilder"/>
public sealed class ListFieldConfigBuilder(
    IMutableFieldConfig mutableConfig
) : FieldConfigBuilder(mutableConfig), IListFieldConfigBuilder
{
    /// <summary>
    /// Cached builder for the field's <see cref="IMutableFieldConfig.TypeConfig"/> to avoid recreating that builder.
    /// </summary>
    private IListTypeConfigBuilder? _currentTypeConfigBuilder;

    /// <inheritdoc cref="IListFieldConfigBuilder.HasName"/>
    public override IListFieldConfigBuilder HasName(string name) => (IListFieldConfigBuilder)base.HasName(name);

    /// <inheritdoc cref="IListFieldConfigBuilder.HasType"/>
    public override IListFieldConfigBuilder HasType(Type type) => (IListFieldConfigBuilder)base.HasType(type);

    /// <inheritdoc cref="IListFieldConfigBuilder.IsNullable"/>
    public override IListFieldConfigBuilder IsNullable(bool nullable = true) =>
        (IListFieldConfigBuilder)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleTypeConfigBuilder SimpleItem(Type type) => GetTypeConfigBuilder().SimpleItem(type);

    /// <inheritdoc/>
    public IObjectTypeConfigBuilder ObjectItem(Type type) => GetTypeConfigBuilder().ObjectItem(type);

    /// <inheritdoc/>
    public IListTypeConfigBuilder ListItem(Type type) => GetTypeConfigBuilder().ListItem(type);

    /// <summary>
    /// Returns the cached <see cref="IListTypeConfigBuilder"/>, creating it if it does not exist yet.
    /// </summary>
    private IListTypeConfigBuilder GetTypeConfigBuilder()
    {
        if (MutableConfig.TypeConfig is not IMutableListTypeConfig listTypeConfig)
            throw new InvalidOperationException("TypeConfig must be of type IMutableListTypeConfig");

        if (_currentTypeConfigBuilder?.MutableConfig != MutableConfig.TypeConfig)
            _currentTypeConfigBuilder = new ListTypeConfigBuilder(listTypeConfig);

        return _currentTypeConfigBuilder;
    }
}