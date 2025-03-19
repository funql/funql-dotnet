// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Filtering.Nodes;
using FunQL.Core.Filtering.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Filtering.Visitors;

/// <summary>Visitor interface for transforming <see cref="Filter"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface IFilterLinqVisitor<in TState> : IFilterVisitor<TState>, ILinqVisitor<TState>
    where TState : ILinqVisitorState;