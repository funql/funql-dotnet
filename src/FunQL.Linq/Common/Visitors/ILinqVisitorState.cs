// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors;

namespace FunQL.Linq.Common.Visitors;

/// <summary>State of a LINQ visitor.</summary>
public interface ILinqVisitorState : IVisitorState
{
    /// <summary>
    /// Whether to handle null propagation. If <c>true</c>, null-checks are included to ensure safe handling of null
    /// values within expressions.
    /// </summary>
    public bool HandleNullPropagation { get; }

    /// <summary>
    /// LINQ <see cref="Expression"/> that serves as the source to which LINQ expressions are applied.
    /// </summary>
    public Expression Source { get; }

    /// <summary>
    /// Current <see cref="Expression"/> result after visiting nodes. This can be modified during visiting.
    /// </summary>
    public Expression? Result { get; set; }
}