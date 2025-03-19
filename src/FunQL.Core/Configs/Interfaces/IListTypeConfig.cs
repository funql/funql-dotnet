// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Configs.Interfaces;

/// <summary>Config for a list type.</summary>
public interface IListTypeConfig : ITypeConfig
{
    /// <summary>Config of the elements in the list.</summary>
    public ITypeConfig ElementTypeConfig { get; }
}