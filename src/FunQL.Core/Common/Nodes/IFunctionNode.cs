// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Nodes;

/// <summary>A FunQL node that represents a function.</summary>
public interface IFunctionNode
{
    /// <summary>Name of the function.</summary>
    public string Name { get; }
}