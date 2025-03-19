// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

namespace FunQL.Core.Common.Nodes;

/// <summary>Metadata of a <see cref="QueryNode"/>.</summary>
/// <param name="Position">Position of the <see cref="QueryNode"/> within original text in case node was parsed.</param>
public sealed record Metadata(int Position);