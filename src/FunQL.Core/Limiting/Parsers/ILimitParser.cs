// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Limiting.Nodes;

namespace FunQL.Core.Limiting.Parsers;

/// <summary>Parser for <see cref="Limit"/> nodes.</summary>
public interface ILimitParser : IConstantParser
{
    /// <summary>Tries to parse <see cref="Limit"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Limit"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Limit ParseLimit(IParserState state);
}