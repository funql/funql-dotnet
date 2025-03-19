// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Builders.Interfaces;
using FunQL.Core.Schemas.Configs.Print.Interfaces;

namespace FunQL.Core.Schemas.Configs.Print.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IPrintConfigExtension"/>.</summary>
public interface IPrintConfigBuilder : IConfigExtensionBuilder
{
    /// <inheritdoc cref="IConfigExtensionBuilder.MutableConfig"/>
    public new IMutablePrintConfigExtension MutableConfig { get; }

    /// <inheritdoc cref="IConfigExtensionBuilder.Build"/>
    public new IPrintConfigExtension Build();
}