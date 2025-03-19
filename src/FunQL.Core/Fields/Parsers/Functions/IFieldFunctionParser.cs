// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Fields.Nodes.Functions;
using FunQL.Core.Fields.Parsers.Fields;

namespace FunQL.Core.Fields.Parsers.Functions;

/// <summary>Parser for <see cref="FieldFunction"/> nodes.</summary>
public interface IFieldFunctionParser : IFieldPathParser
{
    /// <summary>Tries to parse <see cref="FieldFunction"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="FieldFunction"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public FieldFunction ParseFieldFunction(IParserState state);

    /// <summary>Tries to parse <see cref="Year"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Year"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Year ParseYear(IParserState state);

    /// <summary>Tries to parse <see cref="Month"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Month"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Month ParseMonth(IParserState state);

    /// <summary>Tries to parse <see cref="Day"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Day"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Day ParseDay(IParserState state);

    /// <summary>Tries to parse <see cref="Hour"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Hour"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Hour ParseHour(IParserState state);

    /// <summary>Tries to parse <see cref="Minute"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Minute"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Minute ParseMinute(IParserState state);

    /// <summary>Tries to parse <see cref="Second"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Second"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Second ParseSecond(IParserState state);

    /// <summary>Tries to parse <see cref="Millisecond"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Millisecond"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Millisecond ParseMillisecond(IParserState state);

    /// <summary>Tries to parse <see cref="Floor"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Floor"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Floor ParseFloor(IParserState state);

    /// <summary>Tries to parse <see cref="Ceiling"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Ceiling"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Ceiling ParseCeiling(IParserState state);

    /// <summary>Tries to parse <see cref="Round"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Round"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Round ParseRound(IParserState state);

    /// <summary>Tries to parse <see cref="Lower"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Lower"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Lower ParseLower(IParserState state);

    /// <summary>Tries to parse <see cref="Upper"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Upper"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Upper ParseUpper(IParserState state);

    /// <summary>Tries to parse <see cref="IsNull"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="IsNull"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public IsNull ParseIsNull(IParserState state);
}