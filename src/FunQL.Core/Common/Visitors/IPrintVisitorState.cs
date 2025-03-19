// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Visitors;

/// <summary>State of a print visitor.</summary>
public interface IPrintVisitorState : IVisitorState
{
    /// <summary>Writer to print the node tree.</summary>
    public TextWriter TextWriter { get; }
}