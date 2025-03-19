// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using System.Linq.Expressions;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Linq.Sorting.Visitors;

/// <summary>
/// Info of a LINQ sort expression when translating a <see cref="Sort"/> to an <see cref="Expression"/>.
/// </summary>
/// <param name="KeySelectorExpression">The key selector expression to sort on.</param>
/// <param name="Direction">Direction to sort in.</param>
/// <remarks>
/// Sorting in LINQ is done by calling <c>.OrderBy()</c> or <c>.OrderByDescending()</c> and then <c>.ThenBy()</c> or
/// <c>.ThenByDescending()</c>. This is why we need to know the direction to determine which method to call.
/// </remarks>
public sealed record SortLinqExpression(LambdaExpression KeySelectorExpression, SortDirection Direction);