// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Linq.Schemas.Executors;

/// <summary>
/// Context used by <see cref="ApplyLinqExecutionHandler"/> in order to apply the <see cref="Request"/> using LINQ.
/// </summary>
/// <param name="Queryable">Queryable to apply <see cref="Request"/> to.</param>
public sealed record ApplyLinqExecuteContext(IQueryable Queryable) : IExecuteContext;