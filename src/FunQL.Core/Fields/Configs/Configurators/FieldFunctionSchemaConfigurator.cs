// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Configs.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Schemas;

namespace FunQL.Core.Fields.Configs.Configurators;

/// <summary>
/// Configurator that configures the default <see cref="FieldFunction"/> functions for the <see cref="ISchemaConfig"/>.
/// </summary>
public sealed class FieldFunctionSchemaConfigurator : ISchemaConfigurator
{
    /// <inheritdoc/>
    public void Configure(ISchemaConfigBuilder builder)
    {
        var config = builder.MutableConfig;
        config.AddFunctionConfig(Year.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Month.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Day.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Hour.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Minute.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Second.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Millisecond.FunctionName, typeof(int), typeof(object));
        config.AddFunctionConfig(Floor.FunctionName, typeof(object), typeof(object));
        config.AddFunctionConfig(Ceiling.FunctionName, typeof(object), typeof(object));
        config.AddFunctionConfig(Round.FunctionName, typeof(object), typeof(object));
        config.AddFunctionConfig(Lower.FunctionName, typeof(string), typeof(string));
        config.AddFunctionConfig(Upper.FunctionName, typeof(string), typeof(string));
        config.AddFunctionConfig(IsNull.FunctionName, typeof(object), typeof(object));
    }
}