// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Visitors;

/// <summary>Visitor to print the <see cref="Limit"/> nodes.</summary>
/// <inheritdoc cref="ILimitVisitor{TState}"/>
public interface ILimitPrintVisitor<in TState> : ILimitVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;