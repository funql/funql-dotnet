// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Fields.Visitors;
using FunQL.Core.Fields.Visitors.Functions;
using FunQL.Linq.Common.Visitors;

namespace FunQL.Linq.Fields.Visitors;

/// <summary>Default implementation of <see cref="IFieldArgumentLinqVisitor{ILinqVisitorState}"/>.</summary>
/// <inheritdoc cref="FieldArgumentVisitor{ILinqVisitorState}"/>
public class FieldArgumentLinqVisitor(
    IFieldFunctionVisitor<ILinqVisitorState> fieldFunctionVisitor
) : FieldArgumentVisitor<ILinqVisitorState>(fieldFunctionVisitor), IFieldArgumentLinqVisitor<ILinqVisitorState>;