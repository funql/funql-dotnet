// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Interfaces;

/// <summary>Builder interface for building the filter <see cref="IFunctionSupportConfigExtension"/>.</summary>
public interface IFilterFunctionSupportConfigBuilder : IFieldFunctionSupportConfigBuilder
{
    /// <summary>Configures whether the <see cref="Filter"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsFilter(bool supported = true);

    /// <summary>Configures whether the <see cref="Equal"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsEqual(bool supported = true);

    /// <summary>Configures whether the <see cref="NotEqual"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsNotEqual(bool supported = true);

    /// <summary>Configures whether the <see cref="GreaterThan"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsGreaterThan(bool supported = true);

    /// <summary>Configures whether the <see cref="GreaterThanOrEqual"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsGreaterThanOrEqual(bool supported = true);

    /// <summary>Configures whether the <see cref="LessThan"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsLessThan(bool supported = true);

    /// <summary>Configures whether the <see cref="LessThanOrEqual"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsLessThanOrEqual(bool supported = true);

    /// <summary>Configures whether the <see cref="Has"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsHas(bool supported = true);

    /// <summary>Configures whether the <see cref="StartsWith"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsStartsWith(bool supported = true);

    /// <summary>Configures whether the <see cref="EndsWith"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsEndsWith(bool supported = true);

    /// <summary>Configures whether the <see cref="RegexMatch"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsRegexMatch(bool supported = true);

    /// <summary>Configures whether the <see cref="Any"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsAny(bool supported = true);

    /// <summary>Configures whether the <see cref="All"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFilterFunctionSupportConfigBuilder SupportsAll(bool supported = true);

    #region IFieldFunctionSupportConfigBuilder

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsYear"/>
    public new IFilterFunctionSupportConfigBuilder SupportsYear(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMonth"/>
    public new IFilterFunctionSupportConfigBuilder SupportsMonth(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsDay"/>
    public new IFilterFunctionSupportConfigBuilder SupportsDay(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsHour"/>
    public new IFilterFunctionSupportConfigBuilder SupportsHour(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMinute"/>
    public new IFilterFunctionSupportConfigBuilder SupportsMinute(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsSecond"/>
    public new IFilterFunctionSupportConfigBuilder SupportsSecond(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMillisecond"/>
    public new IFilterFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsFloor"/>
    public new IFilterFunctionSupportConfigBuilder SupportsFloor(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsCeiling"/>
    public new IFilterFunctionSupportConfigBuilder SupportsCeiling(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsRound"/>
    public new IFilterFunctionSupportConfigBuilder SupportsRound(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsLower"/>
    public new IFilterFunctionSupportConfigBuilder SupportsLower(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsUpper"/>
    public new IFilterFunctionSupportConfigBuilder SupportsUpper(bool supported = true);

    #endregion
}