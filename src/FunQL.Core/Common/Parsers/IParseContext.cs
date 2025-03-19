// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Processors;

namespace FunQL.Core.Common.Parsers;

/// <summary>
/// Marker interface for context that can be added to the <see cref="IParserState"/> for a parse operation.
/// </summary>
/// <seealso cref="IProcessorState{T}.EnterContext"/>
public interface IParseContext : IProcessContext;