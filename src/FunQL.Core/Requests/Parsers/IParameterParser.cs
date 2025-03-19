// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Parsers;

/// <summary>Parser for <see cref="Parameter"/> nodes.</summary>
public interface IParameterParser
{
    /// <summary>Tries to parse <see cref="Parameter"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Parameter"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Parameter ParseParameter(IParserState state);
}