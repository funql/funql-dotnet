// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Interfaces;

/// <summary>Builder interface for building the sort <see cref="IFunctionSupportConfigExtension"/>.</summary>
public interface ISortFunctionSupportConfigBuilder : IFieldFunctionSupportConfigBuilder
{
    /// <summary>Configures whether the <see cref="Sort"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public ISortFunctionSupportConfigBuilder SupportsSort(bool supported = true);

    #region IFieldFunctionSupportConfigBuilder

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsYear"/>
    public new ISortFunctionSupportConfigBuilder SupportsYear(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMonth"/>
    public new ISortFunctionSupportConfigBuilder SupportsMonth(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsDay"/>
    public new ISortFunctionSupportConfigBuilder SupportsDay(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsHour"/>
    public new ISortFunctionSupportConfigBuilder SupportsHour(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMinute"/>
    public new ISortFunctionSupportConfigBuilder SupportsMinute(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsSecond"/>
    public new ISortFunctionSupportConfigBuilder SupportsSecond(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsMillisecond"/>
    public new ISortFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsFloor"/>
    public new ISortFunctionSupportConfigBuilder SupportsFloor(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsCeiling"/>
    public new ISortFunctionSupportConfigBuilder SupportsCeiling(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsRound"/>
    public new ISortFunctionSupportConfigBuilder SupportsRound(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsLower"/>
    public new ISortFunctionSupportConfigBuilder SupportsLower(bool supported = true);

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.SupportsUpper"/>
    public new ISortFunctionSupportConfigBuilder SupportsUpper(bool supported = true);

    #endregion
}