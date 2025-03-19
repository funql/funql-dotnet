// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Common.Visitors;

namespace FunQL.Linq.Common.Visitors;

/// <summary>Implementation of <see cref="ILinqVisitorState"/>.</summary>
/// <param name="source">Initial LINQ <see cref="Expression"/>.</param>
/// <param name="handleNullPropagation">Whether to handle null propagation.</param>
/// <param name="onEnter">Callback to call when a node is entered.</param>
/// <param name="onExit">Callback to call when a node is exited.</param>
public class LinqVisitorState(
    Expression source,
    bool handleNullPropagation = false,
    VisitorState.VisitDelegate? onEnter = null,
    VisitorState.VisitDelegate? onExit = null
) : VisitorState(onEnter, onExit), ILinqVisitorState
{
    /// <inheritdoc/>
    public bool HandleNullPropagation { get; } = handleNullPropagation;

    /// <inheritdoc/>
    public Expression Source { get; } = source;

    /// <inheritdoc/>
    public Expression? Result { get; set; }
}