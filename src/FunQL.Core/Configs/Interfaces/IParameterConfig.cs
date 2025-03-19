// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for a parameter (for a request).</summary>
public interface IParameterConfig : IExtensibleConfig
{
    /// <summary>Name of the parameter.</summary>
    public string Name { get; }

    /// <summary>Whether the parameter is supported.</summary>
    public bool IsSupported { get; }

    /// <summary>Whether the parameter is required.</summary>
    public bool IsRequired { get; }

    /// <summary>Default value of the parameter if not given.</summary>
    public Parameter? DefaultValue { get; }
}