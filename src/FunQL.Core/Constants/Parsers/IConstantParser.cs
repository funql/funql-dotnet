// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Nodes;

namespace FunQL.Core.Constants.Parsers;

/// <summary>Parser for <see cref="Constant"/> nodes.</summary>
public interface IConstantParser
{
    /// <summary>Tries to parse <see cref="Constant"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Constant"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Constant ParseConstant(IParserState state);
}