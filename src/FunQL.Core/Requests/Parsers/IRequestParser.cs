// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Requests.Nodes;

namespace FunQL.Core.Requests.Parsers;

/// <summary>Parser for <see cref="Request"/> nodes.</summary>
public interface IRequestParser : IParameterParser
{
    /// <summary>Tries to parse <see cref="Request"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Request"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Request ParseRequest(IParserState state);
}