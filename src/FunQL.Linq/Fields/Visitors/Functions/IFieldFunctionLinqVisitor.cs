// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors.Functions;

/// <summary>Visitor interface for transforming <see cref="FieldFunction"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface IFieldFunctionLinqVisitor<in TState> : IFieldFunctionVisitor<TState>, ILinqVisitor<TState>
    where TState : ILinqVisitorState;