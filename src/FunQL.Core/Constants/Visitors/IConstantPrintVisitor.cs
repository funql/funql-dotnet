// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Visitors;

/// <summary>Visitor to print the <see cref="Constant"/> nodes.</summary>
/// <inheritdoc cref="IConstantVisitor{TState}"/>
public interface IConstantPrintVisitor<in TState> : IConstantVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;