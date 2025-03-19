// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Visitors;

/// <summary>Visitor interface for printing a node tree.</summary>
/// <inheritdoc/>
/// <seealso cref="IVisitor{TState}"/>
public interface IPrintVisitor<in TState> : IVisitor<TState> where TState : IPrintVisitorState;