// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Visitors;

/// <summary>
/// Visitor interface for walking a node tree.
///
/// Visit methods should return a <see cref="Task"/> to allow for async support. Visit methods should call
/// <see cref="IVisitorState.OnEnter"/> and <see cref="IVisitorState.OnExit"/> callbacks for sealed/final classes to
/// allow for extending the functionality of a visit call.
/// </summary>
/// <typeparam name="TState">Type of the <see cref="IVisitorState"/> for this visitor.</typeparam>
/// <seealso cref="IVisitorState"/>
public interface IVisitor<in TState> where TState : IVisitorState;