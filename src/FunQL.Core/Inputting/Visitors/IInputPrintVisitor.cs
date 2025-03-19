// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Visitors;

/// <summary>Visitor to print the <see cref="Input"/> nodes.</summary>
/// <inheritdoc cref="IInputVisitor{TState}"/>
public interface IInputPrintVisitor<in TState> : IInputVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;