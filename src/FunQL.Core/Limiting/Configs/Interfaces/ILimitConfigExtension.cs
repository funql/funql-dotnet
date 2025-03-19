// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Configs.Interfaces;

/// <summary>Extension for specifying extra config values for the <see cref="Limit"/> parameter.</summary>
public interface ILimitConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.LimitConfigExtension";

    /// <summary>Default maximum value of limit.</summary>
    public const int DefaultMaxLimit = 1000;

    /// <summary>Maximum value of limit.</summary>
    public int MaxLimit { get; }
}