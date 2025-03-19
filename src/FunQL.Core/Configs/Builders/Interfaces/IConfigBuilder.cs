// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;

namespace FunQL.Core.Configs.Builders.Interfaces;

/// <summary>Builder interface for building the <see cref="IConfig"/>.</summary>
public interface IConfigBuilder
{
    /// <summary>The mutable config being configured.</summary>
    public IMutableConfig MutableConfig { get; }

    /// <summary>Builds the <see cref="IConfig"/> for current <see cref="MutableConfig"/>.</summary>
    /// <returns>The config.</returns>
    /// <remarks>Ideally the returned <see cref="IConfig"/> is immutable so it can't accidentally be changed.</remarks>
    public IConfig Build();
}