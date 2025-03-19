// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;

namespace FunQL.Core.Schemas.Executors.Parse;

/// <summary>Context used by <see cref="ParseRequestExecutionHandler"/> in order to parse the request.</summary>
/// <param name="Request">Text representing the request to parse.</param>
public sealed record ParseRequestExecuteContext(string Request) : IExecuteContext;