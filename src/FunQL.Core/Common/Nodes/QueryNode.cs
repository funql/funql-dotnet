// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Nodes;

/// <summary>Base class for a FunQL node.</summary>
/// <param name="Metadata">Metadata of this node or <c>null</c> if there's no metadata.</param>
public abstract record QueryNode(Metadata? Metadata);