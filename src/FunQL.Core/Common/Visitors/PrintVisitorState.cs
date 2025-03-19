// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Visitors;

/// <summary>Implementation of <see cref="IPrintVisitorState"/>.</summary>
/// <param name="textWriter">Writer to print the node tree.</param>
/// <param name="onEnter">Callback to call when a node is entered.</param>
/// <param name="onExit">Callback to call when a node is exited.</param>
public class PrintVisitorState(
    TextWriter textWriter,
    VisitorState.VisitDelegate? onEnter = null,
    VisitorState.VisitDelegate? onExit = null
) : VisitorState(onEnter, onExit), IPrintVisitorState
{
    /// <inheritdoc/>
    public TextWriter TextWriter { get; } = textWriter;
}