// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;
using FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;

namespace WebApi.FunQL.EFCore;

/// <summary>
/// Extensions related to <see cref="IExecuteConfigBuilder"/> and <see cref="Microsoft.EntityFrameworkCore"/>.
/// </summary>
public static class ExecuteConfigBuilderEntityFrameworkCoreExtensions
{
    /// <summary>
    /// Adds the <see cref="EntityFrameworkCoreExecuteLinqExecutionHandler"/> to given <paramref name="builder"/> if not
    /// yet added.
    /// </summary>
    /// <param name="builder">Builder to configure.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithEntityFrameworkCoreExecuteLinqExecutionHandler(
        this IExecuteConfigBuilder builder
    )
    {
        // Lazy provider so handler is only created when executing
        IExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            EntityFrameworkCoreExecuteLinqExecutionHandler.DefaultName,
            _ => handler ??= new EntityFrameworkCoreExecuteLinqExecutionHandler(),
            EntityFrameworkCoreExecuteLinqExecutionHandler.DefaultOrder
        );
    }
}