// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;

namespace FunQL.Linq.Common.Visitors;

/// <summary>Visitor interface for transforming a node tree to LINQ.</summary>
/// <inheritdoc/>
/// <seealso cref="IVisitor{TState}"/>
public interface ILinqVisitor<in TState> : IVisitor<TState> where TState : ILinqVisitorState;