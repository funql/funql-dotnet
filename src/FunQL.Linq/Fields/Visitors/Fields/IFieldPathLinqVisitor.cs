// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Nodes.Fields;
using FunQL.Core.Fields.Visitors.Fields;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors.Fields;

/// <summary>Visitor interface for transforming <see cref="FieldPath"/> nodes to LINQ.</summary>
/// <inheritdoc cref="ILinqVisitor{TState}"/>
public interface IFieldPathLinqVisitor<in TState> : IFieldPathVisitor<TState>, ILinqVisitor<TState>
    where TState : ILinqVisitorState;