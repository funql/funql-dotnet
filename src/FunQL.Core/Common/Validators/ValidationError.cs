// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;

namespace FunQL.Core.Common.Validators;

/// <summary>Represents a validation error.</summary>
/// <param name="Message">Human-readable message describing the validation error.</param>
/// <param name="Node">The <see cref="QueryNode"/> associated with the error, if applicable.</param>
public sealed record ValidationError(string Message, QueryNode? Node = null);