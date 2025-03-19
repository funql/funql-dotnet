// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Fields.Visitors.Functions;

namespace FunQL.Core.Fields.Visitors;

/// <summary>Default implementation of <see cref="IFieldArgumentPrintVisitor{TState}"/>.</summary>
/// <inheritdoc cref="FieldArgumentVisitor{TState}"/>
public class FieldArgumentPrintVisitor<TState>(
    IFieldFunctionVisitor<TState> fieldFunctionVisitor
) : FieldArgumentVisitor<TState>(fieldFunctionVisitor),
    IFieldArgumentPrintVisitor<TState> where TState : IPrintVisitorState;