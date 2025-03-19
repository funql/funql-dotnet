// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Fields.Configs.FunctionSupport.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Fields.Nodes.Functions;

namespace FunQL.Core.Fields.Configs.FunctionSupport.Builders;

/// <summary>Base class of the <see cref="IFieldFunctionSupportConfigBuilder"/>.</summary>
/// <inheritdoc cref="ConfigExtensionBuilder"/>
public abstract class FieldFunctionSupportConfigBuilder(
    IMutableFunctionSupportConfigExtension mutableConfig
) : ConfigExtensionBuilder(mutableConfig), IFieldFunctionSupportConfigBuilder
{
    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.MutableConfig"/>
    public override IMutableFunctionSupportConfigExtension MutableConfig { get; } = mutableConfig;

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsYear(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Year.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsMonth(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Month.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsDay(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Day.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsHour(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Hour.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsMinute(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Minute.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsSecond(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Second.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Millisecond.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsFloor(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Floor.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsCeiling(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Ceiling.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsRound(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Round.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsLower(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Lower.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsUpper(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(Upper.FunctionName, supported);
        return this;
    }

    /// <inheritdoc/>
    public virtual IFieldFunctionSupportConfigBuilder SupportsIsNull(bool supported = true)
    {
        MutableConfig.SetFunctionSupported(IsNull.FunctionName, supported);
        return this;
    }

    /// <inheritdoc cref="IFieldFunctionSupportConfigBuilder.Build"/>
    public override IFunctionSupportConfigExtension Build() => MutableConfig.ToConfig();
}