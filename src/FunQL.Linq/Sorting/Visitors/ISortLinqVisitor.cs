// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Sorting.Nodes;
using FunQL.Core.Sorting.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Sorting.Visitors;

/// <summary>Visitor interface for transforming <see cref="Sort"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface ISortLinqVisitor<in TState> : ISortVisitor<TState>, ILinqVisitor<TState>
    where TState : ISortLinqVisitorState;