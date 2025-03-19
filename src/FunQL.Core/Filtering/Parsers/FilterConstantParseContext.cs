// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Constants.Nodes;
using FunQL.Core.Fields.Nodes;

namespace FunQL.Core.Filtering.Parsers;

/// <summary>Context for <see cref="IFilterConstantParser"/> used for parsing <see cref="Constant"/>.</summary>
/// <param name="ExpressionName">Name of expression the <see cref="Constant"/> is in.</param>
/// <param name="FieldArgument">Field the <see cref="Constant"/> is compared with.</param>
public sealed record FilterConstantParseContext(string ExpressionName, FieldArgument FieldArgument) : IParseContext;