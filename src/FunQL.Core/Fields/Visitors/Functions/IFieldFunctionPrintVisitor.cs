// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes.Functions;

namespace FunQL.Core.Fields.Visitors.Functions;

/// <summary>Visitor to print the <see cref="FieldFunction"/> nodes.</summary>
/// <inheritdoc cref="IFieldFunctionVisitor{TState}"/>
public interface IFieldFunctionPrintVisitor<in TState> : IFieldFunctionVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;