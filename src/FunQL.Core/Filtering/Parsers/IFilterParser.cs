// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Filtering.Nodes;

namespace FunQL.Core.Filtering.Parsers;

/// <summary>Parser for <see cref="Filter"/> nodes.</summary>
public interface IFilterParser : IFieldArgumentParser, IConstantParser
{
    /// <summary>Tries to parse <see cref="Filter"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Filter"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Filter ParseFilter(IParserState state);

    /// <summary>Tries to parse <see cref="BooleanExpression"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="BooleanExpression"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public BooleanExpression ParseBooleanExpression(IParserState state);

    /// <summary>Tries to parse <see cref="And"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="And"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public And ParseAnd(IParserState state);

    /// <summary>Tries to parse <see cref="Or"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Or"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Or ParseOr(IParserState state);

    /// <summary>Tries to parse <see cref="Not"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Not"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Not ParseNot(IParserState state);

    /// <summary>Tries to parse <see cref="All"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="All"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public All ParseAll(IParserState state);

    /// <summary>Tries to parse <see cref="Any"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Any"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Any ParseAny(IParserState state);

    /// <summary>Tries to parse <see cref="Equal"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Equal"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Equal ParseEqual(IParserState state);

    /// <summary>Tries to parse <see cref="NotEqual"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="NotEqual"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public NotEqual ParseNotEqual(IParserState state);

    /// <summary>Tries to parse <see cref="GreaterThan"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="GreaterThan"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public GreaterThan ParseGreaterThan(IParserState state);

    /// <summary>Tries to parse <see cref="GreaterThanOrEqual"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="GreaterThanOrEqual"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public GreaterThanOrEqual ParseGreaterThanOrEqual(IParserState state);

    /// <summary>Tries to parse <see cref="LessThan"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="LessThan"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public LessThan ParseLessThan(IParserState state);

    /// <summary>Tries to parse <see cref="LessThanOrEqual"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="LessThanOrEqual"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public LessThanOrEqual ParseLessThanOrEqual(IParserState state);

    /// <summary>Tries to parse <see cref="Has"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Has"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Has ParseHas(IParserState state);

    /// <summary>Tries to parse <see cref="StartsWith"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="StartsWith"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public StartsWith ParseStartsWith(IParserState state);

    /// <summary>Tries to parse <see cref="EndsWith"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="EndsWith"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public EndsWith ParseEndsWith(IParserState state);

    /// <summary>Tries to parse <see cref="RegexMatch"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="RegexMatch"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public RegexMatch ParseRegexMatch(IParserState state);
}