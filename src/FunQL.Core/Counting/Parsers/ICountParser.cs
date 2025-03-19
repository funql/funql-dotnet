// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Counting.Nodes;

namespace FunQL.Core.Counting.Parsers;

/// <summary>Parser for <see cref="Count"/> nodes.</summary>
public interface ICountParser : IConstantParser
{
    /// <summary>Tries to parse <see cref="Count"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Count"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Count ParseCount(IParserState state);
}