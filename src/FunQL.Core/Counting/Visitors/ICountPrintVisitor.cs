// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Counting.Nodes;

namespace FunQL.Core.Counting.Visitors;

/// <summary>Visitor to print the <see cref="Count"/> nodes.</summary>
/// <inheritdoc cref="ICountVisitor{TState}"/>
public interface ICountPrintVisitor<in TState> : ICountVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;