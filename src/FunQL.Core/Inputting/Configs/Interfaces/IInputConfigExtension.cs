// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Configs.Interfaces;

/// <summary>Extension for specifying extra config values for the <see cref="Input"/> parameter.</summary>
public interface IInputConfigExtension : IConfigExtension
{
    /// <summary>Default name of the extension.</summary>
    public const string DefaultName = "FunQL.Core.InputConfigExtension";

    /// <summary>Config of the type of the input.</summary>
    public ITypeConfig TypeConfig { get; }
}