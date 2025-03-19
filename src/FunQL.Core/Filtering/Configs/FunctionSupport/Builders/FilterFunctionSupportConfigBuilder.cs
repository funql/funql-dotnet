// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Configs.FunctionSupport.Builders;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Filtering.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Configs.FunctionSupport.Builders;

/// <summary>Default implementation of the <see cref="IFilterFunctionSupportConfigBuilder"/>.</summary>
/// <inheritdoc cref="FieldFunctionSupportConfigBuilder"/>
public sealed class FilterFunctionSupportConfigBuilder(
    IMutableFunctionSupportConfigExtension mutableConfig
) : FieldFunctionSupportConfigBuilder(mutableConfig), IFilterFunctionSupportConfigBuilder
{
    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsFilter(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Filter.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsEqual(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Equal.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsNotEqual(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(NotEqual.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsGreaterThan(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(GreaterThan.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsGreaterThanOrEqual(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(GreaterThanOrEqual.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsLessThan(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(LessThan.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsLessThanOrEqual(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(LessThanOrEqual.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsHas(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Has.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsStartsWith(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(StartsWith.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsEndsWith(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(EndsWith.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsRegexMatch(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(RegexMatch.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsAny(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Any.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public IFilterFunctionSupportConfigBuilder SupportsAll(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(All.FunctionName, supported);
        return this;
    }

    #region IFieldFunctionSupportConfigBuilder

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsYear"/>
    public override IFilterFunctionSupportConfigBuilder SupportsYear(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsYear(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsMonth"/>
    public override IFilterFunctionSupportConfigBuilder SupportsMonth(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsMonth(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsDay"/>
    public override IFilterFunctionSupportConfigBuilder SupportsDay(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsDay(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsHour"/>
    public override IFilterFunctionSupportConfigBuilder SupportsHour(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsHour(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsMinute"/>
    public override IFilterFunctionSupportConfigBuilder SupportsMinute(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsMinute(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsSecond"/>
    public override IFilterFunctionSupportConfigBuilder SupportsSecond(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsSecond(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsMillisecond"/>
    public override IFilterFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsMillisecond(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsFloor"/>
    public override IFilterFunctionSupportConfigBuilder SupportsFloor(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsFloor(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsCeiling"/>
    public override IFilterFunctionSupportConfigBuilder SupportsCeiling(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsCeiling(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsRound"/>
    public override IFilterFunctionSupportConfigBuilder SupportsRound(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsRound(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsLower"/>
    public override IFilterFunctionSupportConfigBuilder SupportsLower(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsLower(supported);

    /// <inheritdoc cref="IFilterFunctionSupportConfigBuilder.SupportsUpper"/>
    public override IFilterFunctionSupportConfigBuilder SupportsUpper(bool supported = true) =>
        (IFilterFunctionSupportConfigBuilder)base.SupportsUpper(supported);

    #endregion
}