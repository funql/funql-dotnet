// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Visitors;

/// <summary>Visitor to print the <see cref="Sort"/> nodes.</summary>
/// <inheritdoc cref="ISortVisitor{TState}"/>
public interface ISortPrintVisitor<in TState> : ISortVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;