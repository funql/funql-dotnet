// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Visitors;

/// <summary>Visitor to print the <see cref="Skip"/> nodes.</summary>
/// <inheritdoc cref="ISkipVisitor{TState}"/>
public interface ISkipPrintVisitor<in TState> : ISkipVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;