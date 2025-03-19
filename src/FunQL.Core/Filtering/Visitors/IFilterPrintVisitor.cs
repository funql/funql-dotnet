// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Visitors;

/// <summary>Visitor to print the <see cref="Filter"/> nodes.</summary>
/// <inheritdoc cref="IFilterVisitor{TState}"/>
public interface IFilterPrintVisitor<in TState> : IFilterVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;