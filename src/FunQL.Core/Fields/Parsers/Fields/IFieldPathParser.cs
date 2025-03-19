// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Fields.Nodes.Fields;

namespace FunQL.Core.Fields.Parsers.Fields;

/// <summary>Parser for <see cref="FieldPath"/> nodes.</summary>
public interface IFieldPathParser
{
    /// <summary>Tries to parse <see cref="FieldPath"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="FieldPath"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public FieldPath ParseFieldPath(IParserState state);

    /// <summary>Tries to parse <see cref="Field"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="Field"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public Field ParseField(IParserState state);

    /// <summary>Tries to parse <see cref="NamedField"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="NamedField"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public NamedField ParseNamedField(IParserState state);

    /// <summary>Tries to parse <see cref="ListItemField"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="ListItemField"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public ListItemField ParseListItemField(IParserState state);
}