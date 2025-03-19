// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;
using FunQL.Linq.Schemas.Executors;

namespace FunQL.Linq.Schemas.Configs.Execute.Builders.Extensions;

/// <summary>Extensions related to <see cref="IExecuteConfigBuilder"/>.</summary>
public static class ExecuteConfigBuilderExtensions
{
    /// <summary>Adds the <see cref="ApplyLinqExecutionHandler"/> as singleton.</summary>
    /// <param name="builder">Builder to configure <see cref="ApplyLinqExecutionHandler"/> for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithApplyLinqExecutionHandler(this IExecuteConfigBuilder builder)
    {
        // Early return if already added
        if (builder.MutableConfig.FindExecutionStep(ApplyLinqExecutionHandler.DefaultName) != null)
            return builder;
        
        // Lazy provider so handler is only created when executing
        IExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            ApplyLinqExecutionHandler.DefaultName,
            _ => handler ??= new ApplyLinqExecutionHandler(),
            ApplyLinqExecutionHandler.DefaultOrder
        );
    }

    /// <summary>Adds the <see cref="ExecuteLinqExecutionHandler"/> as singleton.</summary>
    /// <param name="builder">Builder to configure <see cref="ExecuteLinqExecutionHandler"/> for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithExecuteLinqExecutionHandler(this IExecuteConfigBuilder builder)
    {
        // Early return if already added
        if (builder.MutableConfig.FindExecutionStep(ExecuteLinqExecutionHandler.DefaultName) != null)
            return builder;
        
        // Lazy provider so handler is only created when executing
        IExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            ExecuteLinqExecutionHandler.DefaultName,
            _ => handler ??= new ExecuteLinqExecutionHandler(),
            ExecuteLinqExecutionHandler.DefaultOrder
        );
    }

    /// <summary>Adds the LINQ execution handlers.</summary>
    /// <param name="builder">Builder to configure LINQ execution handlers for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithLinqExecutionHandlers(this IExecuteConfigBuilder builder) => builder
        .WithApplyLinqExecutionHandler()
        .WithExecuteLinqExecutionHandler();
}