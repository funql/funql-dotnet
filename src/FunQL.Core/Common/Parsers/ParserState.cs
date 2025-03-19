// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Common.Processors;
using FunQL.Core.Lexers;

namespace FunQL.Core.Common.Parsers;

/// <summary>Implementation of <see cref="IParserState"/>.</summary>
/// <param name="lexer">The <see cref="Lexer"/> to use.</param>
/// <param name="maxDepth">Maximum allowed recursion depth.</param>
public class ParserState(
    ILexer lexer,
    int maxDepth = ParserState.DefaultMaxDepth
) : ProcessorState<IParseContext>, IParserState
{
    /// <summary>Default maximum allowed recursion depth.</summary>
    public const int DefaultMaxDepth = 128;

    /// <summary>Maximum allowed recursion depth.</summary>
    private readonly int _maxDepth = maxDepth;

    /// <summary>Current recursion depth.</summary>
    private int _depth;

    /// <inheritdoc/>
    public ILexer Lexer { get; } = lexer;

    /// <inheritdoc/>
    public void IncreaseDepth()
    {
        if (++_depth > _maxDepth)
            throw new ParseException($"Maximum recursion depth of {_maxDepth} exceeded.");
    }

    /// <inheritdoc/>
    public void DecreaseDepth()
    {
        if (--_depth < 0)
            throw new InvalidOperationException("Depth should not become negative.");
    }
}