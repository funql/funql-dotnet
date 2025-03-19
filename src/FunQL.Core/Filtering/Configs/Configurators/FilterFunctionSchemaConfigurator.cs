// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Schemas;

namespace FunQL.Core.Filtering.Configs.Configurators;

/// <summary>
/// Configurator that configures the default <see cref="Filter"/> functions for the <see cref="ISchemaConfig"/>.
/// </summary>
public sealed class FilterFunctionSchemaConfigurator : ISchemaConfigurator
{
    /// <inheritdoc/>
    public void Configure(ISchemaConfigBuilder builder)
    {
        var config = builder.MutableConfig;
        config.AddFunctionConfig(Equal.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.FindFunctionConfig(Equal.FunctionName)!.ArgumentTypeConfigs[1].IsNullable = true;
        config.AddFunctionConfig(NotEqual.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.FindFunctionConfig(NotEqual.FunctionName)!.ArgumentTypeConfigs[1].IsNullable = true;
        config.AddFunctionConfig(GreaterThan.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.AddFunctionConfig(GreaterThanOrEqual.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.AddFunctionConfig(LessThan.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.AddFunctionConfig(LessThanOrEqual.FunctionName, typeof(bool), typeof(object), typeof(object));
        config.AddFunctionConfig(Has.FunctionName, typeof(bool), typeof(string), typeof(string));
        config.AddFunctionConfig(StartsWith.FunctionName, typeof(bool), typeof(string), typeof(string));
        config.AddFunctionConfig(EndsWith.FunctionName, typeof(bool), typeof(string), typeof(string));
        config.AddFunctionConfig(RegexMatch.FunctionName, typeof(bool), typeof(string), typeof(string));
    }
}