// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Constants.Nodes;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Parsers;

/// <summary>Parser for <see cref="Constant"/> nodes used for <see cref="Input"/> nodes.</summary>
/// <remarks>
/// Only functions as a marker interface so a specific <see cref="IConstantParser"/> can be defined for
/// <see cref="InputParser"/>.
/// </remarks>
public interface IInputConstantParser : IConstantParser;