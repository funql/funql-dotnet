// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Processors;
using FunQL.Core.Lexers;

namespace FunQL.Core.Common.Parsers;

/// <summary>State of a parser.</summary>
public interface IParserState : IProcessorState<IParseContext>
{
    /// <summary>The lexer to parse data with.</summary>
    public ILexer Lexer { get; }

    /// <summary>Increases current depth and checks if it doesn't exceed the maximum allowed depth.</summary>
    /// <exception cref="ParseException">If maximum depth exceeded.</exception>
    /// <remarks>
    /// Depth is calculated in terms of AST nodes, for example <c>request(sort(asc(lower(field))))</c> has a depth of 6:
    /// Nodes Request, Sort, Ascending, Lower, FieldPath and NamedField.
    /// </remarks>
    public void IncreaseDepth();

    /// <summary>Decreases current depth.</summary>
    public void DecreaseDepth();
}