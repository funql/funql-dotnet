// Copyright 2025 Xtracked
// SPDX-License-Identifier: GPL-2.0-only OR Commercial

using FunQL.Core.Common.Parsers;
using FunQL.Core.Configs;
using FunQL.Core.Configs.Interfaces;
using FunQL.Core.Constants.Parsers;
using FunQL.Core.Counting.Parsers;
using FunQL.Core.Fields.Parsers;
using FunQL.Core.Fields.Parsers.Fields;
using FunQL.Core.Fields.Parsers.Functions;
using FunQL.Core.Filtering.Parsers;
using FunQL.Core.Inputting.Parsers;
using FunQL.Core.Lexers;
using FunQL.Core.Limiting.Parsers;
using FunQL.Core.Requests.Parsers;
using FunQL.Core.Schemas.Configs.Parse.Interfaces;
using FunQL.Core.Skipping.Parsers;
using FunQL.Core.Sorting.Parsers;

namespace FunQL.Core.Schemas.Configs.Parse;

/// <summary>Immutable implementation of <see cref="IParseConfigExtension"/>.</summary>
/// <param name="Name"><inheritdoc cref="ImmutableConfigExtension"/></param>
/// <param name="FieldPathParserProvider">The <see cref="IParseConfigExtension.FieldPathParserProvider"/>.</param>
/// <param name="FieldFunctionParserProvider">
/// The <see cref="IParseConfigExtension.FieldFunctionParserProvider"/>.
/// </param>
/// <param name="FieldArgumentParserProvider">
/// The <see cref="IParseConfigExtension.FieldArgumentParserProvider"/>.
/// </param>
/// <param name="ConstantParserProvider">The <see cref="IParseConfigExtension.ConstantParserProvider"/>.</param>
/// <param name="InputConstantParserProvider">
/// The <see cref="IParseConfigExtension.InputConstantParserProvider"/>.
/// </param>
/// <param name="InputParserProvider">The <see cref="IParseConfigExtension.InputParserProvider"/>.</param>
/// <param name="FilterConstantParserProvider">
/// The <see cref="IParseConfigExtension.FilterConstantParserProvider"/>.
/// </param>
/// <param name="FilterParserProvider">The <see cref="IParseConfigExtension.FilterParserProvider"/>.</param>
/// <param name="SortParserProvider">The <see cref="IParseConfigExtension.SortParserProvider"/>.</param>
/// <param name="SkipParserProvider">The <see cref="IParseConfigExtension.SkipParserProvider"/>.</param>
/// <param name="LimitParserProvider">The <see cref="IParseConfigExtension.LimitParserProvider"/>.</param>
/// <param name="CountParserProvider">The <see cref="IParseConfigExtension.CountParserProvider"/>.</param>
/// <param name="ParameterParserProvider">The <see cref="IParseConfigExtension.ParameterParserProvider"/>.</param>
/// <param name="RequestParserProvider">The <see cref="IParseConfigExtension.RequestParserProvider"/>.</param>
/// <param name="ParserStateFactory">The <see cref="IParseConfigExtension.ParserStateFactory"/>.</param>
public sealed record ImmutableParseConfigExtension(
    string Name,
    Func<ISchemaConfig, IFieldPathParser> FieldPathParserProvider,
    Func<ISchemaConfig, IFieldFunctionParser> FieldFunctionParserProvider,
    Func<ISchemaConfig, IFieldArgumentParser> FieldArgumentParserProvider,
    Func<ISchemaConfig, IConstantParser> ConstantParserProvider,
    Func<ISchemaConfig, IInputConstantParser> InputConstantParserProvider,
    Func<ISchemaConfig, IInputParser> InputParserProvider,
    Func<ISchemaConfig, IFilterConstantParser> FilterConstantParserProvider,
    Func<ISchemaConfig, IFilterParser> FilterParserProvider,
    Func<ISchemaConfig, ISortParser> SortParserProvider,
    Func<ISchemaConfig, ISkipParser> SkipParserProvider,
    Func<ISchemaConfig, ILimitParser> LimitParserProvider,
    Func<ISchemaConfig, ICountParser> CountParserProvider,
    Func<ISchemaConfig, IParameterParser> ParameterParserProvider,
    Func<ISchemaConfig, IRequestParser> RequestParserProvider,
    Func<ISchemaConfig, ILexer, IParserState> ParserStateFactory
) : ImmutableConfigExtension(Name), IParseConfigExtension;