// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Fields.Configs.FunctionSupport.Interfaces;
using FunQL.Core.Fields.Nodes.Functions;

namespace FunQL.Core.Fields.Configs.FunctionSupport.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IFunctionSupportConfigExtension"/>.</summary>
public interface IFieldFunctionSupportConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableFunctionSupportConfigExtension MutableConfig { get; }

    /// <summary>Configures whether the <see cref="Year"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsYear(bool supported = true);

    /// <summary>Configures whether the <see cref="Month"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsMonth(bool supported = true);

    /// <summary>Configures whether the <see cref="Day"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsDay(bool supported = true);

    /// <summary>Configures whether the <see cref="Hour"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsHour(bool supported = true);

    /// <summary>Configures whether the <see cref="Minute"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsMinute(bool supported = true);

    /// <summary>Configures whether the <see cref="Second"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsSecond(bool supported = true);

    /// <summary>Configures whether the <see cref="Millisecond"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsMillisecond(bool supported = true);

    /// <summary>Configures whether the <see cref="Floor"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsFloor(bool supported = true);

    /// <summary>Configures whether the <see cref="Ceiling"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsCeiling(bool supported = true);

    /// <summary>Configures whether the <see cref="Round"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsRound(bool supported = true);

    /// <summary>Configures whether the <see cref="Lower"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsLower(bool supported = true);

    /// <summary>Configures whether the <see cref="Upper"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsUpper(bool supported = true);

    /// <summary>Configures whether the <see cref="IsNull"/> function is supported.</summary>
    /// <param name="supported">Whether function is supported. Default <c>true</c>.</param>
    /// <returns>The builder to continue building.</returns>
    public IFieldFunctionSupportConfigBuilder SupportsIsNull(bool supported = true);

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IFunctionSupportConfigExtension Build();
}