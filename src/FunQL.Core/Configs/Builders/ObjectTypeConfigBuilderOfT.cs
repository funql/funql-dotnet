// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IObjectTypeConfigBuilder{T}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="TypeConfigBuilder{T}"/>
public sealed class ObjectTypeConfigBuilder<T>(
    IObjectTypeConfigBuilder builder
) : TypeConfigBuilder<T>, IObjectTypeConfigBuilder<T>
{
    /// <inheritdoc/>
    protected override IObjectTypeConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="IObjectTypeConfigBuilder.MutableConfig"/>
    public override IMutableObjectTypeConfig MutableConfig => Builder.MutableConfig;

    /// <inheritdoc cref="IObjectTypeConfigBuilder{T}.HasType"/>
    public override IObjectTypeConfigBuilder<T> HasType(Type type) => (IObjectTypeConfigBuilder<T>)base.HasType(type);

    /// <inheritdoc cref="IObjectTypeConfigBuilder{T}.IsNullable"/>
    public override IObjectTypeConfigBuilder<T> IsNullable(bool nullable = true) =>
        (IObjectTypeConfigBuilder<T>)base.IsNullable(nullable);

    /// <inheritdoc/>
    public ISimpleFieldConfigBuilder<TField> SimpleField<TField>(Expression<Func<T, TField>> expression)
    {
        var memberInfo = expression.GetMemberInfo();
        var name = memberInfo.Name;
        var type = memberInfo.GetMemberType();

        var innerBuilder = Builder.SimpleField(name, type);
        innerBuilder.MutableConfig.MemberInfo = memberInfo;
        return new SimpleFieldConfigBuilder<TField>(innerBuilder);
    }

    /// <inheritdoc/>
    public IObjectFieldConfigBuilder<TField> ObjectField<TField>(Expression<Func<T, TField>> expression)
    {
        var memberInfo = expression.GetMemberInfo();
        var name = memberInfo.Name;
        var type = memberInfo.GetMemberType();

        var innerBuilder = Builder.ObjectField(name, type);
        innerBuilder.MutableConfig.MemberInfo = memberInfo;
        return new ObjectFieldConfigBuilder<TField>(innerBuilder);
    }

    /// <inheritdoc/>
    public IListFieldConfigBuilder<TElement> ListField<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression)
    {
        var memberInfo = expression.GetMemberInfo();
        var name = memberInfo.Name;
        var type = memberInfo.GetMemberType();

        var innerBuilder = Builder.ListField(name, type);
        innerBuilder.MutableConfig.MemberInfo = memberInfo;
        return new ListFieldConfigBuilder<TElement>(innerBuilder);
    }

    /// <inheritdoc cref="IObjectTypeConfigBuilder.Build"/>
    public override IObjectTypeConfig Build() => (IObjectTypeConfig)base.Build();

    #region IObjectTypeConfigBuilder

    /// <inheritdoc/>
    IObjectTypeConfigBuilder IObjectTypeConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    IObjectTypeConfigBuilder IObjectTypeConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    /// <inheritdoc/>
    ISimpleFieldConfigBuilder IObjectTypeConfigBuilder.SimpleField(string name, Type type) =>
        Builder.SimpleField(name, type);

    /// <inheritdoc/>
    IObjectFieldConfigBuilder IObjectTypeConfigBuilder.ObjectField(string name, Type type) =>
        Builder.ObjectField(name, type);

    /// <inheritdoc/>
    IListFieldConfigBuilder IObjectTypeConfigBuilder.ListField(string name, Type type) =>
        Builder.ListField(name, type);

    #endregion
}