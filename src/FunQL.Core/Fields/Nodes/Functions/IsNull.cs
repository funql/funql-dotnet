// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Nodes;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Nodes.Functions;

/// <summary>The isNull function returns <c>true</c> if the <paramref name="FieldPath"/> value is <c>null</c>.</summary>
/// <param name="FieldPath">Field path to apply function on.</param>
/// <param name="Metadata"><inheritdoc cref="FieldArgument"/></param>
/// <remarks>
/// This function is the same as <c>eq(field,null)</c>, but as a field function, so it can be used for sorting:
/// <c>asc(isNull(field))</c>. This allows for sorting nulls first or nulls last, regardless of the underlying data
/// source. E.g. PostgreSQL treats NULL values as larger than non-NULL values, so NULL values come last when sorting in
/// ascending order. However, SQL Server treats NULL values as smaller than non-NULL values, so NULL values come first
/// when sorting in ascending order.<br/>
/// To make nulls always come last for a nullable field, you can use: <c>asc(isNull(field)),desc(field)</c>. This first
/// sorts by a boolean (<c>true &gt; false</c>), so items that return <c>true</c> for <c>field == null</c> will come
/// last. 
/// </remarks>
public sealed record IsNull(
    FieldPath FieldPath,
    Metadata? Metadata = null
) : FieldFunction(FunctionName, FieldPath, Metadata)
{
    /// <summary>Function name of this node.</summary>
    public const string FunctionName = "isNull";
}