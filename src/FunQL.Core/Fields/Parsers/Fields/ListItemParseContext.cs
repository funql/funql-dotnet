// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Parsers.Fields;

/// <summary>Context for <see cref="IFieldPathParser"/> required for parsing <see cref="ListItemField"/>.</summary>
/// <param name="Parent">Field path of parent being referenced by the <see cref="ListItemField"/>.</param>
public sealed record ListItemParseContext(FieldPath Parent) : IParseContext;