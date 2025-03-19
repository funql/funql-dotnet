// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Parsers;

/// <summary>Parser for <see cref="Constant"/> nodes used for <see cref="Filter"/> nodes.</summary>
/// <remarks>
/// Only functions as a marker interface so a specific <see cref="IConstantParser"/> can be defined for
/// <see cref="FilterParser"/>.
/// </remarks>
public interface IFilterConstantParser : IConstantParser;