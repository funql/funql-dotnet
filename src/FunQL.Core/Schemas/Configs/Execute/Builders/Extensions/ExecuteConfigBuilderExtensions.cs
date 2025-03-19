// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Common.Executors.Extensions;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;
using FunQL.Core.Schemas.Executors;
using FunQL.Core.Schemas.Executors.Parse;
using FunQL.Core.Schemas.Executors.Validate;

namespace FunQL.Core.Schemas.Configs.Execute.Builders.Extensions;

/// <summary>Extensions related to <see cref="IExecuteConfigBuilder"/>.</summary>
public static class ExecuteConfigBuilderExtensions
{
    /// <summary>
    /// Adds <paramref name="handler"/> as <see cref="ExecutionStep"/> for given <paramref name="name"/> and
    /// <paramref name="order"/> to <see cref="IExecuteConfigExtension.GetExecutionSteps"/>.
    /// </summary>
    /// <param name="builder">Builder to configure <paramref name="handler"/> for.</param>
    /// <param name="name">The name of the execution step.</param>
    /// <param name="handler">The handler to add.</param>
    /// <param name="order">Order of the handler.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithExecutionHandler(
        this IExecuteConfigBuilder builder,
        string name,
        IExecutionHandler handler,
        int order
    ) => builder.WithExecutionStep(new ExecutionStep(
        name,
        next => (state, cancellationToken) => handler.Execute(state, next, cancellationToken),
        order
    ));

    /// <summary>
    /// Adds <paramref name="handlerProvider"/> as <see cref="ExecutionStep"/> for given <paramref name="name"/> and
    /// <paramref name="order"/> to <see cref="IExecuteConfigExtension.GetExecutionSteps"/>. Will call
    /// <paramref name="handlerProvider"/> for <see cref="SchemaConfigExecuteContext"/>. This allows for lazily creating
    /// the <see cref="IExecutionHandler"/>.
    /// </summary>
    /// <param name="builder">Builder to configure <paramref name="handlerProvider"/> for.</param>
    /// <param name="name">The name of the execution step.</param>
    /// <param name="handlerProvider">The handler provider to add.</param>
    /// <param name="order">Order of the handler.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithExecutionHandler(
        this IExecuteConfigBuilder builder,
        string name,
        Func<ISchemaConfig, IExecutionHandler> handlerProvider,
        int order
    ) => builder.WithExecutionStep(new ExecutionStep(
        name,
        next => (state, cancellationToken) =>
        {
            var schemaConfig = state.RequireContext<SchemaConfigExecuteContext>().SchemaConfig;
            return handlerProvider(schemaConfig).Execute(state, next, cancellationToken);
        },
        order
    ));

    /// <summary>Adds the <see cref="ParseRequestExecutionHandler"/> as singleton.</summary>
    /// <param name="builder">Builder to configure <see cref="ParseRequestExecutionHandler"/> for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithParseRequestExecutionHandler(this IExecuteConfigBuilder builder)
    {
        // Early return if already added
        if (builder.MutableConfig.FindExecutionStep(ParseRequestExecutionHandler.DefaultName) != null)
            return builder;
        
        // Lazy provider so handler is only created when executing
        ParseRequestExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            ParseRequestExecutionHandler.DefaultName,
            _ => handler ??= new ParseRequestExecutionHandler(),
            ParseRequestExecutionHandler.DefaultOrder
        );
    }

    /// <summary>Adds the <see cref="ParseRequestForParametersExecutionHandler"/> as singleton.</summary>
    /// <param name="builder">Builder to configure <see cref="ParseRequestForParametersExecutionHandler"/> for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithParseRequestForParametersExecutionHandler(
        this IExecuteConfigBuilder builder
    )
    {
        // Early return if already added
        if (builder.MutableConfig.FindExecutionStep(ParseRequestForParametersExecutionHandler.DefaultName) != null)
            return builder;
        
        // Lazy provider so handler is only created when executing
        ParseRequestForParametersExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            ParseRequestForParametersExecutionHandler.DefaultName,
            _ => handler ??= new ParseRequestForParametersExecutionHandler(),
            ParseRequestForParametersExecutionHandler.DefaultOrder
        );
    }

    /// <summary>Adds the <see cref="ValidateRequestExecutionHandler"/> as singleton.</summary>
    /// <param name="builder">Builder to configure <see cref="ValidateRequestExecutionHandler"/> for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithValidateRequestExecutionHandler(this IExecuteConfigBuilder builder)
    {
        // Early return if already added
        if (builder.MutableConfig.FindExecutionStep(ValidateRequestExecutionHandler.DefaultName) != null)
            return builder;
        
        // Lazy provider so handler is only created when executing
        ValidateRequestExecutionHandler? handler = null;
        return builder.WithExecutionHandler(
            ValidateRequestExecutionHandler.DefaultName,
            _ => handler ??= new ValidateRequestExecutionHandler(),
            ValidateRequestExecutionHandler.DefaultOrder
        );
    }

    /// <summary>Adds the core execution handlers.</summary>
    /// <param name="builder">Builder to configure core execution handler for.</param>
    /// <returns>The builder to continue building.</returns>
    public static IExecuteConfigBuilder WithCoreExecutionHandlers(this IExecuteConfigBuilder builder) => builder
        .WithParseRequestExecutionHandler()
        .WithParseRequestForParametersExecutionHandler()
        .WithValidateRequestExecutionHandler();
}