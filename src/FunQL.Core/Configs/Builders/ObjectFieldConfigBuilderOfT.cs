// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;

namespace FunQL.Core.Configs.Builders;

/// <summary>Default implementation of the <see cref="IObjectFieldConfigBuilder{T}"/>.</summary>
/// <param name="builder">Builder to delegate build logic to.</param>
/// <inheritdoc cref="FieldConfigBuilder{T}"/>
public sealed class ObjectFieldConfigBuilder<T>(
    IObjectFieldConfigBuilder builder
) : FieldConfigBuilder<T>, IObjectFieldConfigBuilder<T>
{
    /// <inheritdoc/>
    protected override IObjectFieldConfigBuilder Builder { get; } = builder;

    /// <inheritdoc cref="IObjectFieldConfigBuilder{T}.HasName"/>
    public override IObjectFieldConfigBuilder<T> HasName(string name) =>
        (IObjectFieldConfigBuilder<T>)base.HasName(name);

    /// <inheritdoc cref="IObjectFieldConfigBuilder{T}.HasType"/>
    public override IObjectFieldConfigBuilder<T> HasType(Type type) =>
        (IObjectFieldConfigBuilder<T>)base.HasType(type);

    /// <inheritdoc cref="IObjectFieldConfigBuilder{T}.IsNullable"/>
    public override IObjectFieldConfigBuilder<T> IsNullable(bool nullable = true) =>
        (IObjectFieldConfigBuilder<T>)base.IsNullable(nullable);

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

    #region IObjectFieldConfigBuilder

    /// <inheritdoc/>
    IObjectFieldConfigBuilder IObjectFieldConfigBuilder.HasName(string name) => HasName(name);

    /// <inheritdoc/>
    IObjectFieldConfigBuilder IObjectFieldConfigBuilder.HasType(Type type) => HasType(type);

    /// <inheritdoc/>
    IObjectFieldConfigBuilder IObjectFieldConfigBuilder.IsNullable(bool nullable) => IsNullable(nullable);

    /// <inheritdoc/>
    ISimpleFieldConfigBuilder IObjectFieldConfigBuilder.SimpleField(string name, Type type) =>
        Builder.SimpleField(name, type);

    /// <inheritdoc/>
    IObjectFieldConfigBuilder IObjectFieldConfigBuilder.ObjectField(string name, Type type) =>
        Builder.ObjectField(name, type);

    /// <inheritdoc/>
    IListFieldConfigBuilder IObjectFieldConfigBuilder.ListField(string name, Type type) =>
        Builder.ListField(name, type);

    #endregion
}