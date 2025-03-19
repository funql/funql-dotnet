// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Common.Parsers.Exceptions;
using FunQL.Core.Fields.Nodes;
using FunQL.Core.Fields.Parsers.Functions;

namespace FunQL.Core.Fields.Parsers;

/// <summary>Parser for <see cref="FieldArgument"/> nodes.</summary>
public interface IFieldArgumentParser : IFieldFunctionParser
{
    /// <summary>Tries to parse <see cref="FieldArgument"/> node for given <paramref name="state"/>.</summary>
    /// <param name="state">State of the parser.</param>
    /// <returns>The parsed <see cref="FieldArgument"/> node.</returns>
    /// <exception cref="ParseException">If parsing fails.</exception>
    public FieldArgument ParseFieldArgument(IParserState state);
}