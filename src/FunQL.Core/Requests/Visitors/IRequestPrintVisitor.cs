// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Visitors;

/// <summary>Visitor to print the <see cref="Request"/> nodes.</summary>
/// <inheritdoc cref="IRequestVisitor{TState}"/>
public interface IRequestPrintVisitor<in TState> : IRequestVisitor<TState>, IPrintVisitor<TState>
    where TState : IPrintVisitorState;