// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Execute.Interfaces;

namespace FunQL.Core.Schemas.Configs.Execute.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IExecuteConfigExtension"/>.</summary>
public interface IExecuteConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableExecuteConfigExtension MutableConfig { get; }

    /// <summary>Adds <paramref name="step"/> to <see cref="IExecuteConfigExtension.GetExecutionSteps"/>.</summary>
    /// <param name="step">The step to add.</param>
    /// <returns>The builder to continue building.</returns>
    /// <remarks>
    /// This replaces any existing <see cref="ExecutionStep"/> with the same <see cref="ExecutionStep.Name"/>.
    /// </remarks>
    public IExecuteConfigBuilder WithExecutionStep(ExecutionStep step);

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IExecuteConfigExtension Build();
}