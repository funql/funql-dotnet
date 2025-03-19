// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Fields.Visitors;

/// <summary>Visitor to print the <see cref="FieldArgument"/> nodes.</summary>
/// <inheritdoc cref="IFieldArgumentVisitor{TState}"/>
public interface IFieldArgumentPrintVisitor<in TState> : IFieldArgumentVisitor<TState>,
    IPrintVisitor<TState> where TState : IPrintVisitorState;