// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Inputting.Nodes;

namespace FunQL.Core.Inputting.Parsers;

/// <summary>Parser for <see cref="Input"/> nodes.</summary>
public interface IInputParser : IConstantParser
{
    /// <summary>Tries to parse <see cref="Input"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Input"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Input ParseInput(IParserState state);
}