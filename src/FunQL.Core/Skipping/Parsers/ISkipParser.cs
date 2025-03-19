// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Skipping.Nodes;

namespace FunQL.Core.Skipping.Parsers;

/// <summary>Parser for <see cref="Skip"/> nodes.</summary>
public interface ISkipParser : IConstantParser
{
    /// <summary>Tries to parse <see cref="Skip"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Skip"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Skip ParseSkip(IParserState state);
}