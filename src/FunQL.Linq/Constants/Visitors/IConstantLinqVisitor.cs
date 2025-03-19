// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Constants.Visitors;

/// <summary>Visitor interface for transforming <see cref="Constant"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface IConstantLinqVisitor<in TState> : IConstantVisitor<TState>, ILinqVisitor<TState>
    where TState : ILinqVisitorState;