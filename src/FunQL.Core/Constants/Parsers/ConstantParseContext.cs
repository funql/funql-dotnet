// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Parsers;

/// <summary>Context for <see cref="IConstantParser"/> used for parsing <see cref="Constant"/>.</summary>
/// <param name="ExpectedType">Expected type of the <see cref="Constant.Value"/>.</param>
public sealed record ConstantParseContext(Type ExpectedType) : IParseContext;