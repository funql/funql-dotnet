// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Configs.Builders.Interfaces;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Configs.Builders;

/// <summary>Default implementation of the <see cref="IInputParameterConfigBuilder"/>.</summary>
/// <inheritdoc cref="ParameterConfigBuilder"/>
public sealed class InputParameterConfigBuilder(
    IMutableParameterConfig mutableConfig
) : ParameterConfigBuilder(mutableConfig), IInputParameterConfigBuilder
{
    /// <inheritdoc/>
    public new IInputParameterConfigBuilder HasName(string name) => (IInputParameterConfigBuilder)base.HasName(name);

    /// <inheritdoc/>
    public IInputParameterConfigBuilder HasDefaultValue(Input? defaultValue)
    {
        MutableConfig.DefaultValue = defaultValue;
        return this;
    }
}