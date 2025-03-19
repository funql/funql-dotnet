// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Visitors.Fields;

/// <summary>Visitor to print the <see cref="FieldPath"/> nodes.</summary>
/// <inheritdoc cref="IFieldPathVisitor{TState}"/>
public interface IFieldPathPrintVisitor<in TState> : IFieldPathVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;