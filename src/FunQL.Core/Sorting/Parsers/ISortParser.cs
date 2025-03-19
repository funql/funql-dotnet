// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Sorting.Nodes;

namespace FunQL.Core.Sorting.Parsers;

/// <summary>Parser for <see cref="Sort"/> nodes.</summary>
public interface ISortParser : IFieldArgumentParser
{
    /// <summary>Tries to parse <see cref="Sort"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Sort"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Sort ParseSort(IParserState state);

    /// <summary>Tries to parse <see cref="SortExpression"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="SortExpression"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public SortExpression ParseSortExpression(IParserState state);

    /// <summary>Tries to parse <see cref="Ascending"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Ascending"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Ascending ParseAscending(IParserState state);

    /// <summary>Tries to parse <see cref="Descending"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Descending"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Descending ParseDescending(IParserState state);
}