// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Sorting.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Configs.FunctionSupport.Builders;

/// <summary>Default implementation of the <see cref="ISortFunctionSupportConfigBuilder"/>.</summary>
/// <inheritdoc cref="FieldFunctionSupportConfigBuilder"/>
public sealed class SortFunctionSupportConfigBuilder(
    IMutableFunctionSupportConfigExtension mutableConfig
) : FieldFunctionSupportConfigBuilder(mutableConfig), ISortFunctionSupportConfigBuilder
{
    /// <inheritdoc/>
    public ISortFunctionSupportConfigBuilder SupportsSort(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Sort.FunctionName, supported);
        return this;
    }

    #region IFieldFunctionSupportConfigBuilder

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsYear"/>
    public override ISortFunctionSupportConfigBuilder SupportsYear(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsYear(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsMonth"/>
    public override ISortFunctionSupportConfigBuilder SupportsMonth(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsMonth(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsDay"/>
    public override ISortFunctionSupportConfigBuilder SupportsDay(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsDay(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsHour"/>
    public override ISortFunctionSupportConfigBuilder SupportsHour(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsHour(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsMinute"/>
    public override ISortFunctionSupportConfigBuilder SupportsMinute(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsMinute(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsSecond"/>
    public override ISortFunctionSupportConfigBuilder SupportsSecond(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsSecond(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsMillisecond"/>
    public override ISortFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsMillisecond(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsFloor"/>
    public override ISortFunctionSupportConfigBuilder SupportsFloor(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsFloor(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsCeiling"/>
    public override ISortFunctionSupportConfigBuilder SupportsCeiling(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsCeiling(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsRound"/>
    public override ISortFunctionSupportConfigBuilder SupportsRound(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsRound(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsLower"/>
    public override ISortFunctionSupportConfigBuilder SupportsLower(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsLower(supported);

    /// <inheritdoc cref="ISortFunctionSupportConfigBuilder.SupportsUpper"/>
    public override ISortFunctionSupportConfigBuilder SupportsUpper(bool supported = true) =>
        (ISortFunctionSupportConfigBuilder)base.SupportsUpper(supported);

    #endregion
}