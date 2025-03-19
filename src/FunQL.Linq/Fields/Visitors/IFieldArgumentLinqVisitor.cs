// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Visitors;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors;

/// <summary>Visitor interface for transforming <see cref="FieldArgument"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface IFieldArgumentLinqVisitor<in TState> : IFieldArgumentVisitor<TState>, ILinqVisitor<TState>
    where TState : ILinqVisitorState;