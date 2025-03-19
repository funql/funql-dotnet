// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Executors;
using FunQL.Core.Counting.Nodes;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Linq.Schemas.Executors;

/// <summary>
/// Context used by <see cref="ExecuteLinqExecutionHandler"/> in order to execute the <see cref="Request"/> for given
/// <paramref name="Queryable"/> and <paramref name="CountQueryable"/>. LINQ should already be applied on given
/// <see cref="IQueryable"/>.
/// </summary>
/// <param name="Queryable">Queryable to get the data for <see cref="Request"/>.</param>
/// <param name="CountQueryable">Queryable to get the count for <see cref="Count"/> when given.</param>
public sealed record ExecuteLinqExecuteContext(IQueryable Queryable, IQueryable? CountQueryable) : IExecuteContext;