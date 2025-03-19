// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Visit.Interfaces;

namespace FunQL.Core.Schemas.Configs.Visit.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IVisitConfigExtension"/>.</summary>
public interface IVisitConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutableVisitConfigExtension MutableConfig { get; }

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IVisitConfigExtension Build();
}