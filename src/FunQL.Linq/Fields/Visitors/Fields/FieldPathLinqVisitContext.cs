// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Visitors;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Linq.Fields.Visitors.Fields;

/// <summary>
/// Context for <see cref="IFieldPathLinqVisitor{ILinqVisitorState}"/> required for translating <see cref="FieldPath"/>.
/// </summary>
/// <param name="ObjectTypeConfig">Config of source referred to by <see cref="FieldPath"/>.</param>
public sealed record FieldPathLinqVisitContext(
    IObjectTypeConfig ObjectTypeConfig
) : IVisitContext;